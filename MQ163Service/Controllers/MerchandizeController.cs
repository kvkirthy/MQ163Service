using MQ163Service.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MQ163Service.Controllers
{
    /// <summary>
    /// Web API that is used for retreiving Merchandize data (in future create and update operations shall be added - functionality not required at this point)
    /// </summary>
    public class MerchandizeController : ApiController
    {
        /// <summary>
        /// Gets list of merchandize objects
        /// </summary>
        /// <returns>JSON represetation of merchandize objects</returns>
        public IEnumerable<JObject> Get()
        {
            var objects = new List<JObject>();
            DataAccess.GetOffers()
                .ForEach(x => objects.Add(JObject.FromObject(x)));
            return objects;
        }

    }
}
