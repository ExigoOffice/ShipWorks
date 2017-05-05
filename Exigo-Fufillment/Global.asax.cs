using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Serilog;
using SerilogWeb.Classic;
using Serilog.Events;
using System.Configuration;

namespace ExigoShipWorks
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            #region Serilog 
            // ** NOTE: DO NOT ENABLE UNTIL YOU CHANGE THE COMPANY BELOW..
            //  EXAMPLE: .Enrich.WithProperty("App", "Company.Backoffice") 
            //  should change to...
            //  .Enrich.WithProperty("App", "ExigoDemo.Backoffice") 
            // **


            /* SeriLog */
            var version = this.GetType().Assembly.GetName().Version;

            ApplicationLifecycleModule.RequestLoggingLevel = LogEventLevel.Verbose;
            ApplicationLifecycleModule.LogPostedFormData = LogPostedFormDataOption.Always;
            ApplicationLifecycleModule.FormDataLoggingLevel = LogEventLevel.Error;

            Log.Logger = new LoggerConfiguration()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("App", "ShipWorks")
                .WriteTo.Seq("https://log.exigo.com")
                .CreateLogger();

            Log.Information("Starting up API");
            #endregion

        }

        // SEE CODE ABOVE IN Application_Start FIRST
        protected void Application_End()
        {
            Log.CloseAndFlush();
        }
    }
}
