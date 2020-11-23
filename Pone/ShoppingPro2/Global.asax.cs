using CommLibrary.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShoppingPro2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            SetNewJsonSoft.Set();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

        }
    }
}
