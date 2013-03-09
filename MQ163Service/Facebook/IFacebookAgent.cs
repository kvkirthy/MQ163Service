using System;
namespace MQ163.Application.External
{
    public interface IFacebookAgent : IDisposable
    {
        string AccessToken { get; }
        bool IsLogged { get; set; }
        bool AddPost(MQ163.External.Facebook.IFacebookPostData postData);
        System.Collections.Generic.IEnumerable<MQ163.External.Facebook.FacebookComment> GetAllCommentsForPost(string postId);
        System.Collections.Generic.IEnumerable<MQ163.External.Facebook.IFacebookPost> GetAllFeeds();
        System.Collections.Generic.IEnumerable<MQ163.External.Facebook.IFacebookProfile> GetAllLikesForPost(string postId);
        string GetFacebookLoginURL();
        bool TaggingPhoto(string photoId, string userId);
    }
}
