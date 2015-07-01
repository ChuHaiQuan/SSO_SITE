using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QsTech.Core.Home;
using QsTech.Core.Views.Home;
using QsTech.Framework.Security;
using QsTech.Core.Interface;

namespace QsTech.Core.Controllers
{
    public class ErrorController : Controller
    {
        [NoAuthorize]
        public ActionResult Index(Exception ex)
        {
           // this.ControllerContext.e
            return View(ex);
        }

        public ActionResult ComingSoon()
        {
            return View();
        }
    }
}
