using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Facebook;
using MQ163.External.Facebook;
using MQ163Service;
using System.Runtime.CompilerServices;
using MQ163Service.Facebook;

[assembly: InternalsVisibleTo("MQ163Service.Tests")]
namespace MQ163.Application.External
{
    internal class FacebookAgent : IFacebookAgent
    {
        #region Private Fields
            private FacebookClient _client = new FacebookClient();
            private string _baseUrl = "https://graph.facebook.com";
        #endregion

        public string AccessToken { get; private set; }
        public bool IsLoggedIn { get; set; }

        /// <summary>
        /// Constructor - instantiate the Facebook Agent
        /// </summary>
        public FacebookAgent()
        {
            AccessToken = CommonData.AuthToken;
            IsLoggedIn = true;
        }


        /// <summary>
        /// Gets the News Feeds of the Facebook Page
        /// </summary>
        /// <returns>Returns list of Feeds of type "Picture" from MQ163 page</returns>
        public IEnumerable<IFacebookPost> GetAllNewsFeed()
        {
            #region Initialize objects and URL
                var json = new JavaScriptSerializer();
                List<IFacebookPost> postsList = new List<IFacebookPost>();
            
                string Url = string.Format("{0}/MQ163/posts?method=GET&format=json&access_token={1}", _baseUrl, AccessToken);
            #endregion

            using (var webClient = new WebClient())
            {
                string data = webClient.DownloadString(Url);

                #region parse Facebook feed data
                    var feeds = (Dictionary<string, object>)json.DeserializeObject(data);
                    object[] feedsArray = (object[])feeds.FirstOrDefault(p => p.Key == "data").Value;
                    foreach (object feed in feedsArray)
                    {
                        IFacebookPost post = new FacebookPost();
                        Dictionary<string, object> feed2 = (Dictionary<string, object>)feed;

                        post.Id = feed2["id"].ToString();

                        if (feed2.Keys.Contains("message"))
                        {
                            post.PostText = feed2["message"].ToString();
                        }
                        else
                        {
                            post.PostText = "No message title.";
                        }

                        if (feed2.Keys.Contains("comments"))
                        {
                            int value = 0;
                            Int32.TryParse((feed2["comments"] as Dictionary<string, object>)["count"].ToString(), out value);
                            post.CommentCount = value;
                        }
                        else
                        {
                            post.CommentCount = 0;
                        }

                        if (feed2.Keys.Contains("likes"))
                        {
                            int value = 0;
                            Int32.TryParse((feed2["likes"] as Dictionary<string, object>)["count"].ToString(), out value);
                            post.LikeCount = value;
                        }
                        else
                        {
                            post.LikeCount = 0;
                        }


                        postsList.Add(post);

                    }
                #endregion
            }
            return postsList;
        }

        /// <summary>
        /// Posts on to Facebook Page
        /// </summary>
        /// <param name="postData">Data to be posted</param>
        /// <returns>Returns True: If posted successfully. 
        /// Exception: If post is unsuccessfull</returns>
        public bool AddPost(IFacebookPostData postData)
        {
            #region initialize objects and Url
                string accessToken = GetPageAccessToken();
                postData.AccessToken = accessToken;

                string path = string.Format("{0}/photos?access_token={1}", "MQ163", accessToken);
                dynamic publishResponse;

                FacebookClient fb = new FacebookClient();
                fb.AccessToken = this.AccessToken;
            #endregion

            publishResponse = fb.PostTaskAsync(path, postData.GetPostObject());

            // Wait for activation 
            while (publishResponse.Status == TaskStatus.WaitingForActivation) ;

            // Check if it succeded or failed
            if (publishResponse.Status == TaskStatus.RanToCompletion)
            {
                string photoId = publishResponse.Result["post_id"];//post_id
                if (null != postData.TaggedUserEmail)
                {
                    bool result = TagPhoto(photoId, GetUserID(postData.TaggedUserEmail).Id);
                }
                return true;
            }
            else if (publishResponse.Status == TaskStatus.Faulted)
            {
                //CommonEventsHelper.WriteToEventLog(string.Format("Error posting message - {0}", (publishResponse.Exception as Exception).Message), System.Diagnostics.EventLogEntryType.Error);
                throw (new InvalidOperationException((((Exception)publishResponse.Exception).InnerException).Message, (Exception)publishResponse.Exception));
            }
            return false;
        }

        /// <summary>
        /// Tags given photo on a post in Facebook
        /// </summary>
        /// <param name="photoId">Identifier for the photo</param>
        /// <param name="userId">User being tagged</param>
        /// <returns>True if tagging successds</returns>
        public bool TagPhoto(string photoId, string userId)
        {
            dynamic parameters = new ExpandoObject();
            var json = new JavaScriptSerializer();
            parameters.tags = json.Serialize(new[] { new { tag_uid = userId, x = 1, y = 1 } });

            string path = string.Format("{0}/tags?access_token={1}", photoId, AccessToken);
            dynamic publishResponse;
            publishResponse = _client.GetTaskAsync(path, parameters);
            while (publishResponse.Status == TaskStatus.WaitingForActivation) ;
            if (publishResponse.Status == TaskStatus.RanToCompletion)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets Access token for the logged in user
        /// </summary>
        /// <returns>Returns the Page Access Token needed for posting data</returns>
        private string GetPageAccessToken()
        {
            var fb = new FacebookClient();
            fb.AccessToken = this.AccessToken;
            // ToDo: Should this be specifid user name/accounts instead of me/accounts?
            dynamic me = fb.Get("/me/accounts");
            dynamic pages1 = me.data;
            string accessToken = null;
            string pageId1 = null;
            string pageName = ConfigurationManager.AppSettings["Facebook:PageName"].ToString();
            foreach (dynamic page in pages1)
            {
                if (page.name == pageName)
                {
                    pageId1 = page.id;
                    accessToken = page.access_token;
                    break;
                }
            }
            return accessToken;
        }

        /// <summary>
        /// Gets all the Comments written by the Facebook users on a post. 
        /// </summary>
        /// <param name="postId">Facebook post ID</param>
        /// <returns>Returns the list of comments for the post</returns>
        public IEnumerable<FacebookComment> GetAllCommentsForPost(string postId)
        {
            #region Initialize objects and Url
                string Url = string.Format("{0}/{2}/comments?access_token={1}", _baseUrl, AccessToken, postId);
                var json = new JavaScriptSerializer();
                List<FacebookComment> commentsList = new List<FacebookComment>();
            #endregion

            using (var webClient = new WebClient())
            {
                string data = webClient.DownloadString(Url);

                #region parse returned data
                    
                var comments = (Dictionary<string, object>)json.DeserializeObject(data);
                object[] commentsArray = (object[])comments.FirstOrDefault(p => p.Key == "data").Value;
                if (commentsArray.Count() > 0)
                {
                    foreach (object comment in commentsArray)
                    {
                        FacebookComment facebookComment = null;
                        Dictionary<string, object> comment2 = (Dictionary<string, object>)comment;
                        if (comment2.Keys.Contains("message") && null != comment2["message"])
                        {
                            facebookComment = new FacebookComment();
                            facebookComment.CommentText = comment2["message"].ToString();
                            facebookComment.Id = comment2["id"].ToString();
                            facebookComment.CreatedDateAndTime = null != comment2["created_time"] ? Convert.ToDateTime(comment2["created_time"].ToString()) : DateTime.MinValue;
                            Dictionary<string, object> commentedUser = (Dictionary<string, object>)comment2["from"];
                            if (commentedUser.Keys.Contains("name") && null != commentedUser["name"])
                            {
                                facebookComment.User = GetUserProfile(commentedUser["id"].ToString());
                            }
                            facebookComment.IsSupportive = (null != comment2["user_likes"] ? Convert.ToBoolean(comment2["user_likes"].ToString()) : false);
                            commentsList.Add(facebookComment);
                        }
                    }
                }

                #endregion
            }
            return commentsList;
        }

        /// <summary>
        /// Gets all the Likes on a post
        /// </summary>
        /// <param name="postId">Facebook post ID</param>
        /// <returns>Returns list of likes for the post</returns>
        public IEnumerable<IFacebookProfile> GetAllLikesForPost(string postId)
        {
            #region Initialize URL and objects
                string Url = string.Format("{0}/{2}/likes?access_token={1}", _baseUrl, AccessToken, postId);
                var json = new JavaScriptSerializer();
                List<IFacebookProfile> profilesList = new List<IFacebookProfile>();
            #endregion

            using (var webClient = new WebClient())
            {
                string data = webClient.DownloadString(Url);

                #region Parse data
                    var likes = (Dictionary<string, object>)json.DeserializeObject(data);
                    object[] likesArray = (object[])likes.FirstOrDefault(p => p.Key == "data").Value;
                    if (likesArray.Count() > 0)
                    {
                        IFacebookProfile profile = null;
                        foreach (object like in likesArray)
                        {
                            Dictionary<string, object> like2 = (Dictionary<string, object>)like;
                            if (like2.Keys.Contains("name") && null != like2["name"])
                            {
                                profile = GetUserProfile(like2["id"].ToString());
                            }
                            profilesList.Add(profile);
                        }
                    }
                #endregion
            }
            return profilesList;
            
        }

        /// <summary>
        /// Gets the Facebook User Profile
        /// </summary>
        /// <param name="profileId">Profile ID of the User</param>
        /// <returns>Returns the Facebook Profile for the User</returns>
        private IFacebookProfile GetUserProfile(string profileId)
        {
            string Url = string.Format("{0}/{2}?access_token={1}", _baseUrl, AccessToken, profileId);
            var json = new JavaScriptSerializer();
            IFacebookProfile profile = null;

            using (var webClient = new WebClient())
            {
                string data = webClient.DownloadString(Url);
                var userProfile = (Dictionary<string, object>)json.DeserializeObject(data);
                if (userProfile.Count() > 0)
                {
                    profile = new FacebookProfile();
                    profile.Id = (userProfile.ContainsKey("id"))?userProfile["id"].ToString():string.Empty;
                    profile.FirstName = (userProfile.ContainsKey("first_name"))?userProfile["first_name"].ToString():"- ";
                    profile.LastName = (userProfile.ContainsKey("last_name"))?userProfile["last_name"].ToString() : "- ";
                    //profile.ProfilePicture = GetProfilePictureURL(userProfile["id"].ToString());
                    profile.UserName = (userProfile.ContainsKey("username"))?userProfile["username"].ToString():"- ";
                }
            }
            return profile;
        }

        /// <summary>
        /// Gets the Facebook Profile of a User by Email ID
        /// </summary>
        /// <param name="userEmail">Email ID of the User</param>
        /// <returns>Returns the User profile of the User</returns>
        private IFacebookProfile GetUserID(string userEmail)
        {
            string Url = string.Format("{0}/search?q={1}&type=user&access_token={2}", _baseUrl, userEmail, AccessToken);
            var json = new JavaScriptSerializer();
            IFacebookProfile profile = null;

            using (var webClient = new WebClient())
            {
                string data = webClient.DownloadString(Url);
                var userProfile = (Dictionary<string, object>)json.DeserializeObject(data);
                if (userProfile.Count() > 0)
                {
                    profile = new FacebookProfile();
                    object[] userProfileArray = (object[])userProfile.FirstOrDefault(p => p.Key == "data").Value;
                    foreach (object user in userProfileArray)
                    {
                        Dictionary<string, object> userProfileData = (Dictionary<string, object>)user;
                        profile = GetUserProfile(userProfileData["id"].ToString());
                    }
                }
            }
            return profile;
        }

        /// <summary>
        /// Gets the Profile Picture of the user
        /// </summary>
        /// <param name="profileId">Facebook Profile ID of the User</param>
        /// <returns>Returns the Facebook Profile Picture of the user</returns>
        private string GetProfilePictureURL(string profileId)
        {
            string Url = string.Format("{0}/{2}/picture?access_token={1}", _baseUrl, AccessToken, profileId);
            var json = new JavaScriptSerializer();
            string profilePictureUrl = null;

            using (var webClient = new WebClient())
            {
                profilePictureUrl = webClient.DownloadString(Url);
            }
            return profilePictureUrl;
        }

        #region IDisposable Members

        public void Dispose()
        {
            this._client = null;
        }

        #endregion
    }
}