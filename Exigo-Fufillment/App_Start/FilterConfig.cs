using shipworks;
using System.Configuration;
using System.Net;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace ExigoShipWorks
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
    public class ShipWorksAuthAttribute : FilterAttribute, IAuthenticationFilter
    {

        //Authenticates the user by checking if they have access to the bi reporting database.
        public void OnAuthentication(AuthenticationContext context)
        {
            var key = context.RouteData.Values["companykey"].ToString();
            var sandbox = context.RouteData.Values["sandbox"].ToString();
            var user = context.HttpContext.Request.Form["username"];
            var pass = context.HttpContext.Request.Form["password"];

            var exigoAdapter = new ShipWorks(context.RouteData.Values["companykey"].ToString(), context.RouteData.Values["sandbox"].ToString(), context.HttpContext.Request.Form["username"], context.HttpContext.Request.Form["password"]);

            if (!exigoAdapter.Auth())
            {
                context.Result = new HttpUnauthorizedResult(); // mark unauthorized
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext context)
        {
            if (context.Result == null || context.Result is HttpUnauthorizedResult)
            {
                context.Result = new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
        }
    }
}
