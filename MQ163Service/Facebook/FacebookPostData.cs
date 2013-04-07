
using System.Dynamic;
using System.IO;
using Facebook;
using MQ163.External.Facebook;
namespace MQ163.Application.External
{
    /// <summary>
    /// Encapsulates Facebook Post Data
    /// </summary>
    public class FacebookPostData : IFacebookPostData
    {
        #region IFacebookPostData Members

        /// <summary>
        /// Post Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Tagged User's email
        /// </summary>
        public string TaggedUserEmail { get; set; }

        /// <summary>
        /// Post picture URL
        /// </summary>
        public string PictureUrl { get; set; }

        /// <summary>
        /// User Access Toekn
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Get Facebook Post Object
        /// </summary>
        /// <param name="userID">User to be tagged</param>
        /// <returns>Post Object</returns>
        public dynamic GetPostObject()
        {
            dynamic parameters = new ExpandoObject();
            parameters.message = Message;
            //parameters.tags = new[] { new { tag_uid = userID, x = 1, y = 1 } };
            //parameters.url = PictureUrl;
            parameters.source = new FacebookMediaObject
                {
                    ContentType = "image/jpeg",
                    FileName = Path.GetFileName(PictureUrl)
                }.SetValue(File.ReadAllBytes(PictureUrl));
            parameters.scope = "publish_stream,photo_upload";
            parameters.access_token = AccessToken;
            parameters.privacy = new
            {
                value = "EVERYONE",
            };

            return parameters;
        }

        #endregion
    }
}