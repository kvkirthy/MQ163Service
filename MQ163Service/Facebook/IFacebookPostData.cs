
namespace MQ163.External.Facebook
{
    /// <summary>
    /// Defines contract for Facebook Post Data
    /// </summary>
    public interface IFacebookPostData
    {
        /// <summary>
        /// Post Message
        /// </summary>
        string Message { get; set; }
        //object Tags { get; set; }
        /// <summary>
        /// Tagged User's email (on the Facebook Post)
        /// </summary>
        string TaggedUserEmail { get; set; }

        /// <summary>
        /// Facebook post picture URL
        /// </summary>
        string PictureUrl { get; set; }

        /// <summary>
        /// User Access Token
        /// </summary>
        string AccessToken { get; set; }

        /// <summary>
        /// Gets post object
        /// </summary>
        /// <returns>Facebook Post object</returns>
        dynamic GetPostObject();
    }
}
