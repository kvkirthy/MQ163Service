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
    public class MerchandizeController : ApiController
    {
        public IEnumerable<JObject> Get()
        {
            var objects = new List<JObject>();
            DataAccess.GetOffers()
                .ForEach(x => objects.Add(JObject.FromObject(x)));
            return objects;
        }

    }
}
