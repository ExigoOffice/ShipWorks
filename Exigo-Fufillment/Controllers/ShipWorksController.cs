using System;
using System.Web.Mvc;
using static ExigoShipWorks.Utils;
using System.Web;
using shipworks;
using System.Net;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using Serilog;

namespace ExigoShipWorks.Controllers
{
   // [ShipWorksAuth]
    public class ShipWorksController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Api(string companykey, string sandbox)
        {
            string action = Request.Form["action"];
            action = action.ToLowerInvariant();
            // Username and password are handled in the ShipWorksAuth Filter
            var exigoAdapter = new ShipWorks(companykey, sandbox, Request.Form["username"], Request.Form["password"]);
            switch (action)
            {
                case "getmodule":
                    return new XmlActionResult(exigoAdapter.GetModuleResponse());
                case "getstore":
                    return new XmlActionResult(exigoAdapter.GetStoreResponse());
                case "getstatuscodes":
                    return new XmlActionResult(exigoAdapter.GetStatusCodesResponse());
                case "getcount":
                    return new XmlActionResult(exigoAdapter.GetCountResponseFromApi(DateTime.ParseExact(Request.Form["start"].ToLowerInvariant(), @"yyyy-M-d\tH:m:s", null)));
                    //return new XmlActionResult(exigoAdapter.GetCountResponseFromReportingDb(DateTime.ParseExact(Request.Form["start"].ToLowerInvariant(), @"yyyy-M-d\tH:m:s", null)));
                case "getorders":
                      return new XmlActionResult(exigoAdapter.GetOrdersResponseFromApi(DateTime.ParseExact(Request.Form["start"].ToLowerInvariant(), @"yyyy-M-d\tH:m:s", null), int.Parse(Request.Form["maxcount"])));
                    //return new XmlActionResult(exigoAdapter.GetOrdersResponseFromReportingDb(DateTime.ParseExact(Request.Form["start"].ToLowerInvariant(), @"yyyy-M-d\tH:m:s", null), int.Parse(Request.Form["maxcount"])));
                case "updatestatus":
                    return new XmlActionResult(exigoAdapter.UpdateStatus(int.Parse(Request.Form["order"]), Request.Form["status"]));
                case "updateshipment":
                    return new XmlActionResult(exigoAdapter.UpdateShipment(int.Parse(Request.Form["order"]), Request.Form["tracking"], Request.Form["carrier"]));
            }

            // If we get a post request that doesn't match any of these respond appropriatly.
            return new HttpStatusCodeResult(HttpStatusCode.NotImplemented);
        }

        
    }
}
