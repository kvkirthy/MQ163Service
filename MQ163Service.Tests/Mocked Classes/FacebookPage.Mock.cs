
using System.Collections.Generic;
using MQ163.External.Facebook;
namespace MQ163Service.Tests.Mocked_Classes
{
    public class FacebookPageMock : IFacebookPage
    {
        public bool IsGetAllPostsCalled { get; set; }
        public bool IsGetAllCommentsCalled { get; set; }
        public bool IsGetAllLikesCalled { get; set; }
        public bool IsAddPostCalled { get; set; }

        public FacebookPageMock()
        {
            IsGetAllPostsCalled = false;
            IsGetAllCommentsCalled = false;
            IsGetAllLikesCalled = false;
            IsAddPostCalled = false;
        }

        #region IFacebookPage Members

        public bool AddPost(IFacebookPostData postObject)
        {
            IsAddPostCalled = true;
            return true;
        }

        public IEnumerable<IFacebookPost> GetAllPosts()
        {
            IsGetAllPostsCalled = true;
            return new List<IFacebookPost>();
        }

        public IEnumerable<FacebookComment> GetAllCommentsForPost(string postID)
        {
            IsGetAllCommentsCalled = true;
            return new List<FacebookComment>();
        }

        public IEnumerable<IFacebookProfile> GetAllLikesForPost(string postID)
        {
            IsGetAllLikesCalled = true;
            return new List<IFacebookProfile>();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {

        }

        #endregion
    }
}
