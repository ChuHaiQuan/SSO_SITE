using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Framework;
using QsTech.Framework.Environment;
using QsTech.Core.Interface;

namespace QsTech.Authentication.Sso.Models
{
    public interface ISsoSetting : ISingletonDependency
    {
        string LogOnUrl { get; }

        string LogoutUrl { get; }

        string SsoLogoutUrl { get; }

        string DefaultUrl { get; }

        string GetUserClientsUrl { get; }

        string GetClientByAppEnumUrl { get; }

        string GetCurrentApplicationUrl { get; }

        string GetReceivedTicketUrl { get; }

        /// <summary>
        /// 获取当前应用程序的注册ID
        /// </summary>
        string GetApplicationId { get; }

        /// <summary>
        /// 获取当前应用程序的私钥
        /// </summary>
        string GetApplicationSecret { get; }

        /// <summary>
        /// 获取token的Url
        /// </summary>
        string GetTokenUrl { get; }

        /// <summary>
        /// 获取用户信息uRL
        /// </summary>
        string GetUserInfoUrl { get; }

        /// <summary>
        /// 获取用户详细信息Url
        /// </summary>
        string GetUserDetailInfoUrl { get; }

        /// <summary>
        /// 获取openId的Url
        /// </summary>
        string GetOpenIdUrl { get; }

        /// <summary>
        /// 客户端登陆首页地址
        /// </summary>
        string GetClientInfoUrl { get; }
    }

    public class SsoSetting :ISsoSetting
    {
        private readonly ICoreSetting _coreSetting;
        public SsoSetting(ICoreSetting coreSetting)
        {
            _coreSetting = coreSetting;
        }

        public string LogOnUrl
        {
            get { return  "/Authentication/Account/LogOn"; }
        }

        public string LogoutUrl
        {
            get { return _coreSetting.GetLogOutUrl; }
        }



        /// <summary>
        /// sso的登出Url
        /// </summary>
        public string SsoLogoutUrl
        {
            get { return string.Format("{0}/Authentication/Account/LogOut", SsoHostUrl); }
        }

        public string GetApplicationSecret
        {
            get { return _coreSetting.GetApplicationSecret; }
        }

        public string GetTokenUrl
        {
            get { return SsoHostUrl + "/Authentication/Oauth2/token"; }
        }

        public string GetUserInfoUrl
        {
            get { return SsoHostUrl + "/Authentication/Oauth2/GetUserInfo"; }
        }

        public string GetUserDetailInfoUrl
        {
           get { return SsoHostUrl + "/Authentication/Oauth2/GetUserDetailInfo"; }
        }

        public string GetOpenIdUrl
        {
            get { return SsoHostUrl + "/Authentication/Oauth2/GetOpenId"; }
        }

        public string GetClientInfoUrl
        {
            get { return  "/Authentication/Account/ClientInfo"; }
        }


        public string DefaultUrl
        {
            get { return "/Core/Home/ManagerIndex"; }
        }

        public string GetUserClientsUrl
        {
            get { return SsoHostUrl + "/Authentication/Api/GetUserClientByUserId"; }
        }

        public string GetClientByAppEnumUrl
        {
            get { return SsoHostUrl + "/Authentication/Api/GetClientByAppType"; }
        }

        public string GetReceivedTicketUrl { get { return GetCurrentApplicationUrl + "/authentication/Sso/ReceivedTicket"; } }
       
        public string GetApplicationId { 
            get { return _coreSetting.GetApplicationId; }
        }

        public string SsoHostUrl
        {
            get { return _coreSetting.GetHostUrl; }
        }

        public string GetCurrentApplicationUrl
         {
            get { return _coreSetting.GetCurrentApplicationUrl; }
        }

       
    }

}