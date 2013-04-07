using System.Collections.Generic;

namespace MQ163.External.Facebook
{
    /// <summary>
    /// Defines contract for Facebook Post
    /// </summary>
    public interface IFacebookPost
    {
        /// <summary>
        /// Get list of likes for a given post
        /// </summary>
        System.Collections.Generic.IEnumerable<IFacebookProfile> Likes
        {
            get;
            set;
        }

        /// <summary>
        /// Gets list of reposts on a given Facebook POst
        /// </summary>
        System.Collections.Generic.IEnumerable<IFacebookProfile> Reposts
        {
            get;
            set;
        }

        /// <summary>
        /// Gets list of comments on a given Facebook Post
        /// </summary>
        IEnumerable<FacebookComment> Comments
        {
            get;
            set;
        }

        /// <summary>
        /// Id of the Facebook post
        /// </summary>
        string Id
        {
            get;
            set;
        }

        /// <summary>
        /// Post picture URl
        /// </summary>
        string PostPicture
        {
            get;
            set;
        }

        /// <summary>
        /// Facebook Post text
        /// </summary>
        string PostText
        {
            get;
            set;
        }

        /// <summary>
        /// Tags on a given FacebookPost
        /// </summary>
        IEnumerable<IFacebookProfile> Tags
        {
            get;
            set;
        }

        /// <summary>
        /// Count of comments on the post
        /// </summary>
        int CommentCount { get; set; }

        /// <summary>
        /// Count of likes on the post
        /// </summary>
        int LikeCount { get; set; }
    }
}
