using System;
using System.Collections.Generic;

namespace MQ163.External.Facebook
{
    public interface IFacebookPage : IDisposable
    {
        bool AddPost(IFacebookPostData postObject);

        IEnumerable<IFacebookPost> GetAllPosts();
    }
}
