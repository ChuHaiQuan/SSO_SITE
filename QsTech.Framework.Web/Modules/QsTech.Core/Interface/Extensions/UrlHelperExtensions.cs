using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using QsTech.Framework;
using QsTech.Framework.Environment;

namespace QsTech.Core.Interface.Extensions
{
    public static class UrlHelperExtensions
    {
  
        public static string GetLogOut(this UrlHelper url)
        {
            var _hostSetting = url.RequestContext.GetWorkContext().Resolve<ICoreSetting>();
            return _hostSetting.GetLogOutUrl;
        }
    }
}
