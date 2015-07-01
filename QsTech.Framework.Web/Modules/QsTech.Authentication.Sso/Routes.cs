using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Framework.Mvc.Routes;
using System.Web.Mvc;
using QsTech.Framework;

namespace QsTech.Authentication.Sso
{
    [Feature("Server")]
    public class Routes : IRouteProvider
    {
        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            routes.MapRoute("QsTech.Authentication.Sso", null, 1, "",
                           new { controller = "Account", action = "LogOn", id = UrlParameter.Optional });

            routes.MapRoute("QsTech.Authentication.Sso", null, 1, "Authentication/{controller}/{action}",
                           new { controller = "Account", action = "LogOn" });

        }
    }

    [Feature("Client")]
    public class ClientRoutes : IRouteProvider
    {
        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            routes.MapRoute("QsTech.Authentication.Sso", null, -100, "",
                           new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute("QsTech.Authentication.Sso", null, 1, "Authentication/{controller}/{action}",
                           new { controller = "Login", action = "Login" });
        }
    }
}