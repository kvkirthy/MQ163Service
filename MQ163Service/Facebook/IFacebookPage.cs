using System;
using System.Collections.Generic;

namespace MQ163.External.Facebook
{
    public interface IFacebookPage : IDisposable
    {
        internal bool AddPost(IFacebookPostData postObject);

        internal IEnumerable<IFacebookPost> GetAllPosts();

        internal IEnumerable<FacebookComment> GetAllCommentsForPost(string postID);

        internal IEnumerable<IFacebookProfile> GetAllLikesForPost(string postID);
    }
}
