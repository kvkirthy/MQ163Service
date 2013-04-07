using MQ163.External.Facebook;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MQ163Service.Tests")]
namespace MQ163.Application.External
{
    internal class FacebookPage : IFacebookPage
    {
        private IFacebookAgent fbAgent = null;

        public FacebookPage()
        {
            fbAgent = new FacebookAgent();
            //fbAgent.FacebookLogin();
        }

        //TOBE: Used by the tests only
        public FacebookPage(IFacebookAgent agent)
        {
            this.fbAgent = agent;
        }

        public FacebookPage(string accessToken)
        {
            if (null == fbAgent)
                fbAgent = new FacebookAgent();
            //fbAgent.AccessToken = accessToken;
        }

        #region IFacebookPage Members

        /// <summary>
        /// Posts the data on the page
        /// </summary>
        /// <param name="postObject">Data to be posted</param>
        /// <returns>Returns True: If posted successfully. 
        /// Exception: If post is unsuccessfull</returns>
        public bool AddPost(IFacebookPostData postObject)
        {
            try
            {
                if (fbAgent.IsLoggedIn)
                {
                    return fbAgent.AddPost(postObject);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets all the News Feeds of the MQ163 page with type "Picture"
        /// </summary>
        /// <returns>Returns all the posts of the page</returns>
        public IEnumerable<IFacebookPost> GetAllPosts()
        {
            try
            {
                if (fbAgent.IsLoggedIn)
                {
                    return fbAgent.GetAllNewsFeed();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets all the comments for the given post ID
        /// </summary>
        /// <param name="postID">Post ID</param>
        /// <returns></returns>
        public IEnumerable<FacebookComment> GetAllCommentsForPost(string postID)
        {
            try
            {
                if (fbAgent.IsLoggedIn)
                {
                    return fbAgent.GetAllCommentsForPost(postID);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets the Profiles of the users who liked the post
        /// </summary>
        /// <param name="postID">Post ID</param>
        /// <returns></returns>
        public IEnumerable<IFacebookProfile> GetAllLikesForPost(string postID)
        {
            try
            {
                if (fbAgent.IsLoggedIn)
                {
                    return fbAgent.GetAllLikesForPost(postID);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this.fbAgent.Dispose();
        }

        #endregion
    }
}