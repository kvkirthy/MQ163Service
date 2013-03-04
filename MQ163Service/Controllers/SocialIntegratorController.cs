using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MQ163Service.Controllers
{
    public class SocialIntegratorController : ApiController
    {
        //public void Post([FromBody]byte[] image)
        //{
        //    var file = new FileInfo(@"C:\Users\VenCKi\AppData\Local\" + Guid.NewGuid().ToString());

        //    //file.Create();
            
        //    var fileWriter = file.OpenWrite();
        //    fileWriter.Write(image, 0, image.Count());
        //    fileWriter.Close();
        //}

        //[HttpPost]
        //public void Post([FromBody]string sample)
        //{

        //}

        public async Task<HttpResponseMessage> PostFormData()
        {
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

                var sb = new StringBuilder();

                //foreach( provider.FormData

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    sb.Append("---");
                    sb.Append(file.Headers.ContentDisposition.FileName);
                    sb.Append("Server file path: " + file.LocalFileName);
                }

                sb.Append(provider.FormData.Get("caption"));

                //return Request.CreateResponse(HttpStatusCode.OK);
               return Request.CreateErrorResponse(HttpStatusCode.Accepted, sb.ToString());
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

    }
}
