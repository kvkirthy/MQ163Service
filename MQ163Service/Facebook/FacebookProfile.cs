using MQ163.External.Facebook;

namespace MQ163.Application.External
{
    /// <summary>
    /// Encapsulates Facebook Profile information
    /// </summary>
    public class FacebookProfile : IFacebookProfile
    {
        #region IFacebookProfile Members

        /// <summary>
        /// Profile Id
        /// </summary>
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// Facebook Profile First Name
        /// </summary>
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// Facebook Profile Last Name
        /// </summary>
        public string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// Facebook Profile picture Url
        /// </summary>
        public string ProfilePicture
        {
            get;
            set;
        }

        /// <summary>
        /// Facebook User Name
        /// </summary>
        public string UserName
        {
            get;
            set;
        }

        #endregion
    }
}