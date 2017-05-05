using shipworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ExigoShipWorks.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Test(string companykey)
        {
            ViewBag.Title = "Test Page";
            ViewBag.companykey = companykey;

            return View();
        }

    }
}
