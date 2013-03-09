using Microsoft.VisualStudio.TestTools.UnitTesting;
using MQ163.Application.External;
using MQ163.Application.External.Fakes;
using MQ163.External.Facebook;
using MQ163Service.Facebook;
using System.Collections.Generic;

namespace MQ163Service.Tests.Facebook
{
    /// <summary>
    /// Summary description for FacebookPage
    /// </summary>
    [TestClass]
    public class FacebookPageTest
    {
        private IFacebookAgent fAgent = null;
        private IFacebookPage target = null;
        public FacebookPageTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestInitialize]
        public void TestInitialize()
        {
            CommonData.AuthToken = "AAAH7kNqRjzEBABPae2TMwT9xvafZA5g8quwjuiqXd4knAZBiSOZAX1JQF2JIfw9fpkgZAD5NPZCleAlXn6XdfRlq7evk1Yf7921DpRoVcBIkZB8lTL925r";
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void GetAllCommentsWithValidParameterTest()
        {
            bool IsFAgentGetAllCommentsCalled = false;
            this.fAgent = new StubIFacebookAgent()
            {
                IsLoggedGet = () => {return true; },
                GetAllCommentsForPostString = (postID) => { IsFAgentGetAllCommentsCalled = true; return new List<FacebookComment>(); }
            };
            this.target = new FacebookPage(fAgent);
            target.GetAllCommentsForPost("132");

            Assert.IsTrue(IsFAgentGetAllCommentsCalled, "Facade didn't call FacebookAgent.GetAllComments.");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void GetAllLikesWithValidParameterTest()
        {
            bool IsFAgentGetAllLikesCalled = false;
            this.fAgent = new StubIFacebookAgent()
            {
                IsLoggedGet = () => { return true; },
                GetAllLikesForPostString = (postID) => { IsFAgentGetAllLikesCalled = true; return new List<IFacebookProfile>(); }
            };
            this.target = new FacebookPage(fAgent);
            target.GetAllLikesForPost("132");

            Assert.IsTrue(IsFAgentGetAllLikesCalled, "Page.GetAllLikesForPost not called from Facade.");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void GetAllPostsTest()
        {
            bool IsFAgentGetAllPostsCalled = false;
            this.fAgent = new StubIFacebookAgent()
            {
                IsLoggedGet = () => { return true; },
                GetAllFeeds = () => { IsFAgentGetAllPostsCalled = true; return new List<IFacebookPost>(); }
            };
            this.target = new FacebookPage(fAgent);
            target.GetAllPosts();

            Assert.IsTrue(IsFAgentGetAllPostsCalled, "Page.GetAllPosts not called from Facade.");
        }

        
        [TestMethod]
        [TestCategory("Unit")]
        public void PostPictureMesssageWithNullMessageTest()
        {
            bool IsPagePostPictureMesssageCalled = false;
            this.fAgent = new StubIFacebookAgent()
            {
                IsLoggedGet = () => { return true; },
                AddPostIFacebookPostData = (PostData) => { IsPagePostPictureMesssageCalled = true; return true; }
            };
            this.target = new FacebookPage(fAgent);
            target.AddPost(new FacebookPostData() { Message = null, PictureUrl = "C:\\", TaggedUserEmail = null });
            Assert.IsTrue(IsPagePostPictureMesssageCalled, "Page.AddPost not called from Facade when message of the post is null.");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void PostPictureMesssageWithNullEmailIDTest()
        {
            bool IsPagePostPictureMesssageCalled = false;
            this.fAgent = new StubIFacebookAgent()
            {
                IsLoggedGet = () => { return true; },
                AddPostIFacebookPostData = (PostData) => { IsPagePostPictureMesssageCalled = true; return true; }
            };
            this.target = new FacebookPage(fAgent);
            target.AddPost(new FacebookPostData() { Message = "Hi", PictureUrl = "C:\\", TaggedUserEmail = null });
            Assert.IsTrue(IsPagePostPictureMesssageCalled, "Page.AddPost not called from Facade when tagged email id of the post is null.");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void PostPictureMesssageWithValidParametersTest()
        {
            bool IsPagePostPictureMesssageCalled = false;
            this.fAgent = new StubIFacebookAgent()
            {
                IsLoggedGet = () => { return true; },
                AddPostIFacebookPostData = (PostData) => { IsPagePostPictureMesssageCalled = true; return true; }
            };
            this.target = new FacebookPage(fAgent);
            target.AddPost(new FacebookPostData() { Message = "Hi", PictureUrl = "C:\\", TaggedUserEmail = "seshumiriyala@gmail.com" });
            Assert.IsTrue(IsPagePostPictureMesssageCalled, "Page.PostPictureMesssage not called from Facade when all the parameters are valid.");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.fAgent.Dispose();
        }
    }
}
