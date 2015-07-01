using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QsTech.Authentication.Sso.Clients;
using QsTech.Authentication.Sso.Models;
using QsTech.Authentication.Sso.Models.AccessToken;
using QsTech.Authentication.Sso.Models.Oauth2;
using QsTech.Core.Interface;
using QsTech.Core.Interface.Clients;
using QsTech.Core.Interface.User;
using QsTech.Framework.Logging;
using QsTech.Framework.Security;

namespace QsTech.Authentication.Sso.Controllers
{
    public class Oauth2Controller : Controller
    {
        private readonly IAccessTokenManager _accessTokenManager;
        private readonly IClientManager _clientManager;
        private readonly IAccountManager _accountManager;


        public Oauth2Controller(IAccessTokenManager accessTokenManager,
            IClientManager clientManager,IAccountManager accountManager)
        {
            _accessTokenManager = accessTokenManager;
            _clientManager = clientManager;
            _accountManager = accountManager;
            Logger = NullLogger.Instance;

        }

        public ILogger Logger;
        //[RequireHttps]
        [NoAuthorize]
        [HttpPost]
         public ActionResult token(TokenModel data)
         {
             Logger.Information("根据code获取access-token");
             if (string.IsNullOrEmpty(data.code))
             {
                 return Json(new AccessTokenOauthModel() { ret = -10012,  message = "不是有效的授权码" });
             }
             var clientValid=CheckClientValid(data);
             if (clientValid != null) return Json(clientValid);
             var reJson = _accessTokenManager.GetTokenByAuthorizationCode(data);
              return Json(reJson);
         }

         private AccessTokenOauthModel CheckClientValid(TokenModel data)
         {
             var legalClient =
              _clientManager.GetClients().SingleOrDefault(
                  m => m.ApplicationId == data.client_id && m.ApplicationSecret == data.client_secret);
             if (legalClient == null)
                 return new AccessTokenOauthModel() { ret = -10009, message = "不是合法的第三方应用!" };
             // if (data.redirect_uri == null || (data.redirect_uri.ToUpper() != legalClient.Callback.ToUpper()))
             var clientt = (Client)legalClient;
             if (data.redirect_uri == null || clientt.HostInfos.All(m => m.Callback.ToUpper() != data.redirect_uri.ToUpper())) //增加多个客户端查询
                 return new AccessTokenOauthModel() { ret = -10008, message = "不是合法的第三方应用!" };
             return null;
         }

         #region
         //[RequireHttps]
         //[NoAuthorize]
         //[HttpPost]
         //public ActionResult GetAccountInfo(string access_token)
         //{
         //    //Logger.Information("根据code获取access-token");
         //    if (string.IsNullOrEmpty(access_token))
         //    {
         //        return Json(new AccountOauthModel()
         //                        {
         //                             ret = -10012,
         //                             message = "不是有效的token"
         //                        });
         //    }
         //    else
         //    {
         //        string msg = "";
         //        var reJson = _accessTokenManager.GetAccountInfoByToken(access_token,ref msg);
         //        if (reJson == null) return Json(new AccountOauthModel()
         //        {
         //            ret = -10012,
         //            message = msg
         //        });
         //        return Json(reJson);
         //    }

         //}
         #endregion

         //[RequireHttps]
         [NoAuthorize]
         [HttpPost]
         public ActionResult GetOpenId(string access_token)
         {
           
             if (string.IsNullOrEmpty(access_token))
             {
                 return Json(new OpenIdOauthModel()
                 {
                     ret = -1001,
                     message = "不是有效的token"
                 });
             }
             else
             {
                 string msg = "";
                 var reJson = _accessTokenManager.GetAccountInfoByToken(access_token, ref msg);
                 if (reJson == null) return Json(new OpenIdOauthModel()
                 {
                     ret = -1002,
                     message = msg
                 });
                   var accountModel=_accountManager.GetSsoAccountModelById(reJson.Id);
                   return Json(new OpenIdOauthModel() { openid = reJson.Id, AccountName = accountModel.AccountName });
             }

         }


         //[RequireHttps]
         [NoAuthorize]
         [HttpPost]
         public ActionResult GetAccountInfo(string access_token, string client_id, string openid)
         {
             if (string.IsNullOrEmpty(access_token)||string.IsNullOrEmpty(client_id)||string.IsNullOrEmpty(openid))
             {
                 return Json(new AccountInfoOauthModel()
                 {
                     ret = -1001,
                     message = "参数缺失"
                 });
             }
             else
             {
                 string msg = "";
                 var reJson = _accessTokenManager.GetAccountInfoByToken(access_token, ref msg);
                 if (reJson == null) return Json(new AccountInfoOauthModel()
                 {
                     ret = -1002,
                     message = msg
                 });
               //判断账户是否有使用clientId的权限；暂缺

                 var account = _accountManager.GetSsoAccountModelById(reJson.Id);
                 if (account == null) return Json(new AccountInfoOauthModel()
              {
                  ret = -1003,
                  message = "用户信息不存在"
              });
                 return Json(account);
                 
             }

         }
    }
}
