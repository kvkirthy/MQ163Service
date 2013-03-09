﻿using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using MQ163.Application.Facade;
using System.Diagnostics;

namespace MQ163Service.Controllers
{
    public class SocialIntegratorController : ApiController
    {
        public async Task<HttpResponseMessage> PostFormData()
        {
            string fileUri = string.Empty;
            string messageCaption = string.Empty, taggedUserEmail = string.Empty;
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    fileUri = file.LocalFileName;
                }

                messageCaption = provider.FormData.Get("caption");
                taggedUserEmail = provider.FormData.Get("tagUser");

                CommonEventsHelper.WriteToEventLog(string.Format("{0}. {1}",messageCaption,fileUri),EventLogEntryType.Information);

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
