using System.Collections.Generic;
using MQ163.External.Facebook;

namespace MQ163.Application.External
{
    /// <summary>
    /// Encapsulats Facebook Entity
    /// </summary>
    public class FacebookPost : IFacebookPost
    {
        #region IFacebookPost Members

        /// <summary>
        /// Count of comments on a given post
        /// </summary>
        public int CommentCount { get; set; }

        /// <summary>
        /// Count of LIkes on a given post
        /// </summary>
        public int LikeCount { get; set; }

        public IEnumerable<IFacebookProfile> Likes
        {
            get;
            set;
        }

        /// <summary>
        /// Reposts on a Facebook Post (Lists users reposted)
        /// </summary>
        public IEnumerable<IFacebookProfile> Reposts
        {
            get;
            set;
        }

        /// <summary>
        /// Comments on a Facebook Post
        /// </summary>
        public IEnumerable<FacebookComment> Comments
        {
            get;
            set;
        }

        /// <summary>
        /// Identifier for the Facebook Post
        /// </summary>
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// Picture posted along with the Facebook Post
        /// </summary>
        public string PostPicture
        {
            get;
            set;
        }

        /// <summary>
        /// Post Text/ content
        /// </summary>
        public string PostText
        {
            get;
            set;
        }

        /// <summary>
        /// Tagged users (if any)
        /// </summary>
        public IEnumerable<IFacebookProfile> Tags
        {
            get;
            set;
        }

        #endregion
    }
}