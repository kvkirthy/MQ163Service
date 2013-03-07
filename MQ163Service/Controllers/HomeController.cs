using System.Web.Mvc;
using MQ163.Application.Facade;
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

        [HttpPost]
        public ActionResult AddPost()
        {
            FacebookFacade facade = new FacebookFacade();
            facade.PostPictureMesssage("This is TEst image1", @"C:\Users\Public\Pictures\Sample Pictures\Penguins.jpg", "seshumiriyala@gmail.com");
            return View("Success");
        }
    }
}
