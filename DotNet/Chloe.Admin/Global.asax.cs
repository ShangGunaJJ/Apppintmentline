using Ace.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Timers;
using System.Net.Http;
using Chloe.Application.Interfaces.Appointment;
namespace Chloe.Admin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            System.Timers.Timer myTimer = new System.Timers.Timer(10000);
            myTimer.Elapsed += new System.Timers.ElapsedEventHandler(TimeExecuteTask);
            myTimer.Interval = 10000;
            myTimer.Enabled = true;

            AppServiceFactory.RegisterServices();
            AppServiceFactory.RegisterServicesFromAssembly(Chloe.Application.CurrentAssembly.Value);

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        private void TimeExecuteTask(object sender, ElapsedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                var responseString = client.GetStringAsync("http://47.93.230.40:8000/WeChat/RefAccess_Token");
            }
        }
    }

    public class BigBigException : Exception
    {
        public BigBigException(string msg)
            : base(msg)
        {
        }
    }

}
