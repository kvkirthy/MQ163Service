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
    public class ProspectsController : ApiController
    {
        // GET api/values
        public IEnumerable<JObject> Get(bool isCustomer)
        {
            return getProspectsJson(isCustomer);
        }

        private IEnumerable<JObject> getProspectsJson(bool isCustomer)
        {
            var prospects  = new List<JObject>();
            if (isCustomer)
            {
                DataAccess.GetCustomers()
                    .ForEach(x => prospects.Add(JObject.FromObject(x)));
            }
            else
            {
                DataAccess.GetLeads()
                    .ForEach(x => prospects.Add(JObject.FromObject(x)));
            }

            return prospects;
        }

        [HttpGet,ActionName("CreateLead")]
        public void CreateLead(string fullName)
        {
            DataAccess.CreateLead(fullName);
        }

    }

}