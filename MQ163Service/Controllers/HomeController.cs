using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MQ163Service.Facebook;

namespace MQ163Service.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Post()
        {
            CommonData.AuthToken = Request.Form["authToken"];
            return View("Success");
        }
    }
}
