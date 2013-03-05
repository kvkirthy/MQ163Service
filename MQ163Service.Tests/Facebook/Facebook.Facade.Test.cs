using Microsoft.VisualStudio.TestTools.UnitTesting;
using MQ163.Application.Facade;
using MQ163Service.Facebook;

namespace MQ163Service.Tests.Facebook
{
    [TestClass]
    public class FacebookFacadeTest
    {
        private FacebookFacade facade = null;

        [TestInitialize]
        public void TestInitialize()
        {
            CommonData.AuthToken = "AAAH7kNqRjzEBAIlDTwz7AiZCPumwXiZCrgExTjSDJAZAZB20FtykOrqdh6LZC6axqajTgeG48T7wiE4DRk7GxdreI3RWOUJ0LRQnk8l4lJPEp7ub4Idgn";
            this.facade = new FacebookFacade().Activate();
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void AddPostTest()
        {
            bool result = facade.AddPost("This is my Test image1", @"C:\Users\Public\Pictures\Sample Pictures\Tulips.jpg", "venckii@facebook.com");

            Assert.IsTrue(result, "Posting an image failed.");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.facade.Dispose();
        }
    }
}
