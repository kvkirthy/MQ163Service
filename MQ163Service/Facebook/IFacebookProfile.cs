
namespace MQ163.External.Facebook
{
    public interface IFacebookProfile
    {
        /// <summary>
        /// Id of the given Facebook Profile
        /// </summary>
        string Id
        {
            get;
            set;
        }

        /// <summary>
        /// First name of the Facebook Profile User
        /// </summary>
        string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// Last name of the Facebook Profile user
        /// </summary>
        string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// Profile picture of the Facebook Profile user
        /// </summary>
        string ProfilePicture
        {
            get;
            set;
        }

        /// <summary>
        /// User name of the Facebook profile user.
        /// </summary>
        string UserName
        {
            get;
            set;
        }
    }
}
