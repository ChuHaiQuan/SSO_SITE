using System.Collections.Generic;
using QsTech.Framework.Mvc.Routes;

namespace QsTech.Layout
{
    public class Routes : IRouteProvider
    {
        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            var defaultRoute = routes.MapRoute("QsTech.Core", "Home_Default", 9999, "", new
            {
                controller = "Home", action = "Index"
            });
            routes.MapRoute("QsTech.Core", null, 10, "Core/{controller}/{action}", new
            {
                controller = "Home", action = "Index"
            });
        }
    }
}