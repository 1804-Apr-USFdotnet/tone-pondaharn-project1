using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your average restaurant review site.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us. Your call is important us. Beep.";

            return View();
        }
    }
}