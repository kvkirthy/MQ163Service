using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using MQ163.Application.Facade;
using Newtonsoft.Json.Linq;

namespace MQ163Service.Controllers
{
    // ToDo: Consider merging this with Social Integrator Controller

    /// <summary>
    /// Facebook API controller for getting Facebook data
    /// </summary>
    public class FacebookController : ApiController
    {
        /// <summary>
        /// Get list of posts on given Facebook page.
        /// </summary>
        /// <returns>JSON representation of Facebook Postso</returns>
        public IEnumerable<JObject> Get()
        {

            var jObjects = new List<JObject>();

            var facebookPosts = new FacebookFacade()
                    .GetAllPosts();

            if (facebookPosts != null && facebookPosts.Count() > 0)
            {
                facebookPosts.ToList()
                    .ForEach(x => jObjects.Add(JObject.FromObject(x)));
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.NoContent);
            }

            return jObjects;

        }

       
        /// <summary>
        /// Get all comments on given Facebook post
        /// </summary>
        /// <param name="postId">Post Id for which comments need to be retreived</param>
        /// <returns>List of Facebook Comments</returns>
        [ActionName("comments")]
        public IEnumerable<JObject> GetComments(string postId)
        {
            var jObjects = new List<JObject>();

            var facebookPosts = new FacebookFacade()
                    .GetAllCommentsForPost(postId);

            if (facebookPosts != null && facebookPosts.Count() > 0)
            {
                facebookPosts.ToList()
                    .ForEach(x => jObjects.Add(JObject.FromObject(x)));
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.NoContent);
            }

            return jObjects;
        }
    }
}
