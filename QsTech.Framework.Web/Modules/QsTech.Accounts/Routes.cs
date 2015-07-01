using System.Collections.Generic;
using QsTech.Framework.Mvc.Routes;

namespace QsTech.Accounts
{
    public class Routes : IRouteProvider
    {
        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            routes.MapRoute("QsTech.Accounts", null, 10, "Accounts/{controller}/{action}/{id}",
                new { controller = "Account", action = "List", id = System.Web.Mvc.UrlParameter.Optional });
        }
    }
}