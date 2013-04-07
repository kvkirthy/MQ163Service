using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using MQ163.Application.Facade;
using System.Diagnostics;

namespace MQ163Service.Controllers
{
    /// <summary>
    /// API Controller that exposes Social Integrator related functionality.
    /// </summary>
    public class SocialIntegratorController : ApiController
    {
        /// <summary>
        /// Multi part form post. Client uses it to post Facebook Post
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostFormData()
        {
            string fileUri = string.Empty;
            string messageCaption = string.Empty, taggedUserEmail = string.Empty;

            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                // if not throw exception
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            // location to store data, in this case images.
            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                // Get image file. It's already saved in App Data folder
                foreach (MultipartFileData file in provider.FileData)
                {
                    fileUri = file.LocalFileName;
                }

                //Additional pieces of information in the  form data along with images.
                messageCaption = provider.FormData.Get("caption");
                taggedUserEmail = provider.FormData.Get("tagUser");

                CommonEventsHelper.WriteToEventLog(string.Format("{0}. {1}",messageCaption,fileUri),EventLogEntryType.Information);

                // Post on to facebook
                new FacebookFacade()
                    .PostPictureMesssage(messageCaption, fileUri, taggedUserEmail);

                return Request.CreateResponse(HttpStatusCode.OK);
                //return Request.CreateErrorResponse(HttpStatusCode.Accepted, sb.ToString());
            }
            catch (System.Exception e)
            {
                CommonEventsHelper.WriteToEventLog(e.Message, EventLogEntryType.Error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
