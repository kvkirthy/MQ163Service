using System;
using System.Collections.Generic;
using MQ163.Application.External;
using MQ163.External.Facebook;

namespace MQ163.Application.Facade
{
    /// <summary>
    /// Encapsulate complex operations of getting data, parsing, multiple calls (if any)
    /// For the consumer it's just a single call.
    /// </summary>
    public class FacebookFacade : IDisposable
    {
        private IFacebookPage page = null;

        /// <summary>
        /// Initializes with given Facebook Page object
        /// </summary>
        /// <param name="page">Facebook page to be initailized</param>
        public FacebookFacade(IFacebookPage page)
        {
            this.page = page;
        }

        /// <summary>
        /// Initializes Facebook Facade object. Need to set Facebook page later.
        /// </summary>
        public FacebookFacade()
        {
            page = new FacebookPage();
        }
        

        /// <summary>
        /// Gets information of all the posts of type "picture" for a given Facebook page.
        /// </summary>
        /// <returns>IEnumerable of all the Posts</returns>
        public IEnumerable<IFacebookPost> GetAllPosts()
        {
            if (null == page)
            {
                page = new FacebookPage();
            }
            return page.GetAllPosts();
        }

        /// <summary>
        /// Creates a post on Facebook page.
        /// </summary>
        /// <param name="message">Message of the POst</param>
        /// <param name="picUrl">Picture URL</param>
        /// <param name="taggedUserEmail">emial ID of the User tagged in the Post</param>
        /// <returns></returns>
        public bool PostPictureMesssage(string message, string picUrl, string taggedUserEmail)
        {
            if (null == picUrl)
                throw new ArgumentNullException("PicUrl", "Cannot post an empty image to Facebook.");
            IFacebookPostData data = new FacebookPostData();
            data.Message = message;
            data.PictureUrl = picUrl;
            data.TaggedUserEmail = taggedUserEmail;

            return page.AddPost(data);
        }

        /// <summary>
        /// Get comments on a given post.
        /// </summary>
        /// <param name="postID">Post ID</param>
        /// <returns>Returns List of all the comments on the post</returns>
        public IEnumerable<FacebookComment> GetAllCommentsForPost(string postID)
        { 
            if (null == postID)
                throw new ArgumentNullException("PostID", "Cannot get the comments for null post ID.");
            return page.GetAllCommentsForPost(postID);
        }

        /// <summary>
        /// Get the Profiles of users who liked the post
        /// </summary>
        /// <param name="postID">Post ID</param>
        /// <returns>Returns list of all the likes for the post</returns>
        public IEnumerable<IFacebookProfile> GetAllLikesForPost(string postID)
        {
            if (null == postID)
                throw new ArgumentNullException("PostID", "Cannot get like for null post ID.");
            return page.GetAllLikesForPost(postID);
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.page.Dispose();
        }

        #endregion
    }
}