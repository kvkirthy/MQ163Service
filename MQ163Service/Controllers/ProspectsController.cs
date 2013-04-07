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
    /// Web API that exposes functionality related to retrieving and saving Customer/Lead data
    /// </summary>
    public class ProspectsController : ApiController
    {
        /// <summary>
        /// Get all customers or leads
        /// </summary>
        /// <param name="isCustomer">True for customer, false for lead</param>
        /// <returns>JSON object of customers or leads</returns>
        public IEnumerable<JObject> Get(bool isCustomer)
        {
            return getProspectsJson(isCustomer);
        }

        /// <summary>
        /// Retreives data calling Data Access function 
        /// </summary>
        /// <param name="isCustomer">True for customer, false for lead</param>
        /// <returns>JSON representation of the object</returns>
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

        // ToDo: make it Http post
        /// <summary>
        /// Creates a lead
        /// </summary>
        /// <param name="fullName">Full name of the lead</param>
        [HttpGet,ActionName("CreateLead")]
        public void CreateLead(string fullName)
        {
            DataAccess.CreateLead(fullName);
        }

    }

}