using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MQ163.Application.Facade;
using MQ163Service.Facebook;
using MQ163Service.Tests.Mocked_Classes;

namespace MQ163Service.Tests.Facebook
{
    [TestClass]
    public class FacebookFacadeTest
    {
        private FacebookFacade facade = null;
        private FacebookPageMock FacebookPageMock;

        [TestInitialize]
        public void TestInitialize()
        {
            CommonData.AuthToken = "AAAH7kNqRjzEBABPae2TMwT9xvafZA5g8quwjuiqXd4knAZBiSOZAX1JQF2JIfw9fpkgZAD5NPZCleAlXn6XdfRlq7evk1Yf7921DpRoVcBIkZB8lTL925r";
            this.FacebookPageMock = new FacebookPageMock();
            this.facade = new FacebookFacade(FacebookPageMock);

        }

        [TestMethod]
        [TestCategory("Integration")]
        public void AddPostTest()
        {
            bool result = facade.PostPictureMesssage("This is my Test image1", @"C:\Users\Public\Pictures\Sample Pictures\Tulips.jpg", "venckii@facebook.com");

            Assert.IsTrue(result, "Posting an image failed.");
        }

        [TestMethod]
        [TestCategory("Unit")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetAllCommentsWithNullParameterTest()
        {
            facade.GetAllCommentsForPost(null);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void GetAllCommentsWithValidParameterTest()
        {
            facade.GetAllCommentsForPost("132");

            Assert.IsTrue(FacebookPageMock.IsGetAllCommentsCalled, "Page.GetAllCommentsForPost not called from Facade.");
        }

        [TestMethod]
        [TestCategory("Unit")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetAllLikesWithNullParameterTest()
        {
            facade.GetAllLikesForPost(null);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void GetAllLikesWithValidParameterTest()
        {
            facade.GetAllLikesForPost("132");

            Assert.IsTrue(FacebookPageMock.IsGetAllLikesCalled, "Page.GetAllLikesForPost not called from Facade.");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void GetAllPostsTest()
        {
            facade.GetAllPosts();

            Assert.IsTrue(FacebookPageMock.IsGetAllPostsCalled, "Page.GetAllPosts not called from Facade.");
        }

        [TestMethod]
        [TestCategory("Unit")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PostPictureMesssageWithNullPicUrlTest()
        {
            facade.PostPictureMesssage(null, null, null);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void PostPictureMesssageWithNullMessageTest()
        {
            facade.PostPictureMesssage(null, "C:\\", null);
            Assert.IsTrue(FacebookPageMock.IsAddPostCalled, "Page.AddPost not called from Facade when message of the post is null.");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void PostPictureMesssageWithNullEmailIDTest()
        {
            facade.PostPictureMesssage("Hi", "C:\\", null);
            Assert.IsTrue(FacebookPageMock.IsAddPostCalled, "Page.AddPost not called from Facade when tagged email id of the post is null.");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void PostPictureMesssageWithValidParametersTest()
        {
            facade.PostPictureMesssage("Hi", "C:\\", "seshumiriyala@gmail.com");
            Assert.IsTrue(FacebookPageMock.IsAddPostCalled, "Page.PostPictureMesssage not called from Facade when all the parameters are valid.");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.facade.Dispose();
        }
    }
}
