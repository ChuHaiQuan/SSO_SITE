using System.Collections.Generic;
using System.Web.Mvc;
using Autofac.Features.Metadata;
using QsTech.Core.Interface;
using QsTech.Layout.Models;
using QsTech.Core.Interface;

namespace QsTech.Core.Controllers
{
    public class ZoneController : Controller
    {
        private readonly IEnumerable<Meta<IZoneProvider>> _zoneProviders;

        public ZoneController(IEnumerable<Meta<IZoneProvider>> zoneProviders)
        {
            _zoneProviders = zoneProviders;
        }

        public ActionResult Render(string id)
        {
            foreach (var zoneProvider in _zoneProviders)
            {
                var cAnda = zoneProvider.Value.GetZone(id);
                if (cAnda != null)
                {
                    var zone = new Zone(id, (string)zoneProvider.Metadata["Module"], cAnda.Item1, cAnda.Item2);
                    return View(zone);
                }
            }

            return View((object)null);
        }
    }
}
