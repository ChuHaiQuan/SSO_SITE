using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QsTech.Core.Controllers
{
    [QsTech.Framework.Security.NoAuthorize]
    public class FeaturesController : Controller
    {
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Howdo()
        {
            return View();
        }

        public ActionResult Cando()
        {
            return View();
        }

        public ActionResult Problem()
        {
            return View();
        }
    }
}
