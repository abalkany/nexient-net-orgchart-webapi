using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Nexient.Net.Orgchart.Data.Ninject;

namespace Nexient.Net.Orgchart.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            NinjectBag.InitializeNinject();
        }
    }
}
