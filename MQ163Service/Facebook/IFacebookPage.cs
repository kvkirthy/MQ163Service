using System;
using System.Collections.Generic;

namespace MQ163.External.Facebook
{
    public interface IFacebookPage : IDisposable
    {
        /// <summary>
        /// Adds a post on the Facebook Page
        /// </summary>
        /// <param name="postObject">object to be posted</param>
        /// <returns>true if object is posted successfully</returns>
        bool AddPost(IFacebookPostData postObject);

        /// <summary>
        /// Gets all posts from Facebook Page
        /// </summary>
        /// <returns>Facebook post data parsed to IFacebookPost object</returns>
        IEnumerable<IFacebookPost> GetAllPosts();

        /// <summary>
        /// Gets all comments on the given post
        /// </summary>
        /// <param name="postID">Post Identifier</param>
        /// <returns>List of Facebook Comments</returns>
        IEnumerable<FacebookComment> GetAllCommentsForPost(string postID);

        /// <summary>
        /// Gets all likes on a given post
        /// </summary>
        /// <param name="postID">Post Identifier</param>
        /// <returns>List of Facebook Likes</returns>
        IEnumerable<IFacebookProfile> GetAllLikesForPost(string postID);
    }
}
