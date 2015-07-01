using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using QsTech.Framework;
using QsTech.Framework.Environment;

namespace QsTech.Authentication.Sso.Models
{
    public static class UrlHelperExtensions
    {
        //public static string GetLogOnUrl(this UrlHelper url)
        //{
        //    var _hostSetting = url.RequestContext.GetWorkContext().Resolve<ISsoSetting>();
        //    return _hostSetting.LogOnUrl;
        //}

        //public static string GetLogOut(this UrlHelper url)
        //{
        //    var _hostSetting = url.RequestContext.GetWorkContext().Resolve<ISsoSetting>();
        //    return _hostSetting.LogoutUrl;
        //}

       //   ///<summary>
       //  /// 跳转到子应用的地址
       //  ///</summary>
       // /// <param name="url"></param>
       // /// <param name="client">子应用名称</param>
       /////  <param name="returnUrl">链接URL</param>
       // /// <returns></returns>
       // [Obsolete]
       // public static string RedirectClientUrl(this UrlHelper url,ClientAppEnum clientEnum,string returnUrl)
       // {
       //     var clientManager = url.RequestContext.GetWorkContext().Resolve<IClientManager>();
       //     //var _hostSetting = url.RequestContext.GetWorkContext().Resolve<IHostSetting>();
       //     var client= clientManager.GetClientByAppType(clientEnum);
       //     var callback = HttpUtility.UrlEncode(client.Callback + "?returnUrl=" + client.Host+returnUrl);
       //     return url.GetLogOnUrl() + "?returnUrl=" + callback;
       // }


        /// <summary>
        /// 包装sso登录url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="clientEnum"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public static string GetLogOnUrl(this UrlHelper url, string returnUrl)
        {
            var _setting = url.RequestContext.GetWorkContext().Resolve<ISsoSetting>();
            var callback = HttpUtility.UrlEncode(string.Format("{0}?returnUrl={1}",_setting.GetReceivedTicketUrl,returnUrl));  // _setting.g + "?returnUrl=" + client.Host + returnUrl);
            return string.Format("{0}?redirect_uri={1}&response_type=code&client_id={2}", _setting.LogOnUrl, callback,_setting.GetApplicationId);
        }

    }
}
