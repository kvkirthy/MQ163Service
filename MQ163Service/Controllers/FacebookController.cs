﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MQ163.Application.Facade;
using Newtonsoft.Json.Linq;

namespace MQ163Service.Controllers
{
    public class FacebookController : ApiController
    {
        public IEnumerable<JObject> Get()
        {
 
            var jObjects = new List<JObject>();

            var facebookPosts = new FacebookFacade()
                    .Activate()
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
    }
}