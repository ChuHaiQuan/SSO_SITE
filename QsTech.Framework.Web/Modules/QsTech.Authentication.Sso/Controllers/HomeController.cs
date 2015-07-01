using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QsTech.Framework.Security;

namespace QsTech.Authentication.Sso.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        //[NoAuthorize]
        public ActionResult Index()
        {
            return View();
        }

    }
}
