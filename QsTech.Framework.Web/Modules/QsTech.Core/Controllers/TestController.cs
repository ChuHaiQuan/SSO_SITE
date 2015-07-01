using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QsTech.Core.Interface;
using QsTech.Framework;
using QsTech.Framework.Environment;
using QsTech.Framework.Security;

namespace QsTech.Core.Controllers
{
    
    public class TestController : Controller
    {

        public TestController()
        {

        }


        //
        // GET: /Test/
        [NoAuthorize]
        public ActionResult Index()
        {
            
            return View();
        }

    }
}
