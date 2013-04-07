using System;
namespace MQ163.Application.External
{
    public interface IFacebookAgent : IDisposable
    {
        /// <summary>
        /// Read Only - User Access Token for the session
        /// </summary>
        string AccessToken { get; }

        /// <summary>
        /// Is the given user logged in?
        /// </summary>
        bool IsLoggedIn { get; set; }

        /// <summary>
        /// Add a post on Facebook page
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        bool AddPost(MQ163.External.Facebook.IFacebookPostData postData);

        /// <summary>
        /// Get all comments for the given Facebook post.
        /// </summary>
        /// <param name="postId">Identifier for the Facebook post</param>
        /// <returns>All Facebook comments for the given post</returns>
        System.Collections.Generic.IEnumerable<MQ163.External.Facebook.FacebookComment> GetAllCommentsForPost(string postId);

        /// <summary>
        /// Gets all news feed on given Facebook Page.
        /// </summary>
        /// <returns></returns>
        System.Collections.Generic.IEnumerable<MQ163.External.Facebook.IFacebookPost> GetAllNewsFeed();
        
        /// <summary>
        /// Get all likes information on the given Facebook post
        /// </summary>
        /// <param name="postId">Identifier for the Facebook post</param>
        /// <returns>All likes for the givne Facebook post.</returns>
        System.Collections.Generic.IEnumerable<MQ163.External.Facebook.IFacebookProfile> GetAllLikesForPost(string postId);
        
        /// <summary>
        /// Tag given Photo on a  post
        /// </summary>
        /// <param name="photoId">Identifier of the photo to be tagged</param>
        /// <param name="userId">User being tagged on the photo</param>
        /// <returns>true if tagging is successfull.</returns>
        bool TagPhoto(string photoId, string userId);
    }
}
