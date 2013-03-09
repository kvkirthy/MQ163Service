using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MQ163.Application.Facade;
using MQ163Service.Facebook;
using MQ163Service.Tests.Mocked_Classes;
using MQ163.External.Facebook;
using MQ163.External.Facebook.Fakes;
using System.Collections.Generic;

namespace MQ163Service.Tests.Facebook
{
    [TestClass]
    public class FacebookFacadeTest
    {
        private FacebookFacade facade = null;
        private IFacebookPage FacebookPageStub;

        [TestInitialize]
        public void TestInitialize()
        {
            CommonData.AuthToken = "AAAH7kNqRjzEBAErSt1hOYz6EdygxLBU8xFZBomQ09SfwYy7jdgtSLiD3dGPfUZBL6eFqREZB2sfbdDH3x8UFbudQncKqZABR1qWZBgZAxF9ADEmqs0YVDw";
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void AddPostTest()
        {
            this.facade = new FacebookFacade();
            bool result = facade.PostPictureMesssage("I like it.", @"C:\Users\Public\Pictures\Sample Pictures\Tulips.jpg", "venckii@facebook.com");

            Assert.IsTrue(result, "Posting an image failed.");
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void GetAllPostsTest()
        {
            this.facade = new FacebookFacade();
            List<IFacebookPost> result = (List<IFacebookPost>)facade.GetAllPosts();

            Assert.IsNotNull(result, "Posting are not returned by the page.");
            Assert.IsTrue(result.Count > 0, "There are no posts on page.");
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void GetAllCommentsTest()
        {
            this.facade = new FacebookFacade();
            List<FacebookComment> result = (List<FacebookComment>)facade.GetAllCommentsForPost("450370435035504");

            Assert.IsNotNull(result, "GetAllComments not working.");
            Assert.IsTrue(result.Count > 0, "There are no comments for the post.");
        }

        [TestMethod]
        [TestCategory("Unit")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetAllCommentsWithNullParameterTest()
        {
            this.FacebookPageStub = new StubIFacebookPage()
            {
                GetAllCommentsForPostString = (postID) => { return new List<FacebookComment>(); }
            };
            this.facade = new FacebookFacade(FacebookPageStub);
            facade.GetAllCommentsForPost(null);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void GetAllCommentsWithValidParameterTest()
        {
            bool IsPageGetAllCommentsCalled = false;
            this.FacebookPageStub = new StubIFacebookPage()
            {
                GetAllCommentsForPostString = (postID) => { IsPageGetAllCommentsCalled = true; return new List<FacebookComment>(); }
            };
            this.facade = new FacebookFacade(FacebookPageStub);
            facade.GetAllCommentsForPost("132");

            Assert.IsTrue(IsPageGetAllCommentsCalled, "Facade didn't call Page.GetAllComments.");
        }

        [TestMethod]
        [TestCategory("Unit")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetAllLikesWithNullParameterTest()
        {
            this.FacebookPageStub = new StubIFacebookPage()
            {
                GetAllCommentsForPostString = (postID) => { return new List<FacebookComment>(); }
            };
            this.facade = new FacebookFacade(FacebookPageStub);
            facade.GetAllLikesForPost(null);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void GetAllLikesWithValidParameterTest()
        {
            bool IsPageGetAllLikesCalled = false;
            this.FacebookPageStub = new StubIFacebookPage()
            {
                GetAllLikesForPostString = (postID) => { IsPageGetAllLikesCalled = true; return new List<IFacebookProfile>(); }
            };
            this.facade = new FacebookFacade(FacebookPageStub);
            facade.GetAllLikesForPost("132");

            Assert.IsTrue(IsPageGetAllLikesCalled, "Page.GetAllLikesForPost not called from Facade.");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void GetAllPostsUnitTest()
        {
            bool IsPageGetAllPostsCalled = false;
            this.FacebookPageStub = new StubIFacebookPage()
            {
                GetAllPosts = () => { IsPageGetAllPostsCalled = true; return new List<IFacebookPost>(); }
            };
            this.facade = new FacebookFacade(FacebookPageStub);
            facade.GetAllPosts();

            Assert.IsTrue(IsPageGetAllPostsCalled, "Page.GetAllPosts not called from Facade.");
        }

        [TestMethod]
        [TestCategory("Unit")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PostPictureMesssageWithNullPicUrlTest()
        {
            this.FacebookPageStub = new StubIFacebookPage()
            {
                GetAllCommentsForPostString = (postID) => { return new List<FacebookComment>(); }
            };
            this.facade = new FacebookFacade(FacebookPageStub);
            facade.PostPictureMesssage(null, null, null);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void PostPictureMesssageWithNullMessageTest()
        {
            bool IsPagePostPictureMesssageCalled = false;
            this.FacebookPageStub = new StubIFacebookPage()
            {
                AddPostIFacebookPostData = (PostData) => { IsPagePostPictureMesssageCalled = true; return true; }
            };
            this.facade = new FacebookFacade(FacebookPageStub);
            facade.PostPictureMesssage(null, "C:\\", null);
            Assert.IsTrue(IsPagePostPictureMesssageCalled, "Page.AddPost not called from Facade when message of the post is null.");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void PostPictureMesssageWithNullEmailIDTest()
        {
            bool IsPagePostPictureMesssageCalled = false;
            this.FacebookPageStub = new StubIFacebookPage()
            {
                AddPostIFacebookPostData = (PostData) => { IsPagePostPictureMesssageCalled = true; return true; }
            };
            this.facade = new FacebookFacade(FacebookPageStub);
            facade.PostPictureMesssage("Hi", "C:\\", null);
            Assert.IsTrue(IsPagePostPictureMesssageCalled, "Page.AddPost not called from Facade when tagged email id of the post is null.");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void PostPictureMesssageWithValidParametersTest()
        {
            bool IsPagePostPictureMesssageCalled = false;
            this.FacebookPageStub = new StubIFacebookPage()
            {
                AddPostIFacebookPostData = (PostData) => { IsPagePostPictureMesssageCalled = true; return true; }
            };
            this.facade = new FacebookFacade(FacebookPageStub);
            facade.PostPictureMesssage("Hi", "C:\\", "seshumiriyala@gmail.com");
            Assert.IsTrue(IsPagePostPictureMesssageCalled, "Page.PostPictureMesssage not called from Facade when all the parameters are valid.");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.facade.Dispose();
        }
    }
}
