using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QsTech.Authentication.Sso.Models.AccessToken;
using QsTech.Authentication.Sso.authentication;
using QsTech.Authentication.Sso.Models.ticket;
using System.Text;
using QsTech.Core.Interface.User;
using QsTech.Framework;
using QsTech.Framework.Json;
using QsTech.Framework.Security;
using QsTech.Framework.Logging;
using QsTech.Authentication.Sso.Models;
using System.Net;
using QsTech.Framework.Utility;
using QsTech.Core.Interface;

namespace QsTech.Authentication.Sso.Controllers
{
    //public class SsoController : Controller
    //{
    //    private readonly IAuthenticationService _svcAuthentication;
    //    public ILogger Logger { get; set; }
    //    private readonly ISsoSetting _ssoSetting;
    //    private readonly IUserManager _userManager;

    //    public SsoController(IAuthenticationService svcAuthentication,ISsoSetting ssoSetting,
    //        IUserManager userManager)
    //    {
    //        _svcAuthentication = svcAuthentication;
    //        Logger = NullLogger.Instance;
    //        _ssoSetting = ssoSetting;
    //        _userManager = userManager;
    //    }

    //    //[RequireHttps]
    //    [NoAuthorize]
    //    public ActionResult ReceivedTicket(SsoReceiveData data)
    //    {
    //        if (!CheckTicketData(data))
    //        {
    //            Logger.Information("用户{0}CheckTicketData失败", data.accountName);
    //            //return new RedirectResult(data.returnUrl);
    //            throw new QsTechException(string.Format("用户{0}CheckTicketData失败", data.accountName)){ NoAuthorizeLayout = true};
    //        }
    //        string token;
    //        var isvalid = GetToken(data,out token);
    //        if(!isvalid)
    //        {
    //            Logger.Information("获取access_token失败!");
    //            throw new QsTechException("获取access_token失败!"){ NoAuthorizeLayout = true};
    //        }
    //        #region
    //        //var invoker = new WebInvoke(Encoding.UTF8);
    //        //try
    //        //{
    //        //    var strResult = invoker.Post(_ssoSetting.GetUserDetailInfoUrl, new Dictionary<string, string> { { "access_token", token } });
    //        //    var result = JsonHelper.ConvertToObject<SsoUserModel>(strResult);
                

    //        //    setFormAuthentication(result.Id);
    //        //}
    //        //catch(Exception ex)
    //        //{
    //        //    Logger.Information("获取用户信息失败时发生错误:{0}", ex.ToString());
    //        //    throw new QsTechException(string.Format("获取用户信息失败,请联系系统管理员!")) { NoAuthorizeLayout = true };
    //        //}
    //        #endregion

    //        var currentUser = GetOpenIdCorrespondUser(token);
    //        _svcAuthentication.SignIn(currentUser,false);
    //        if (string.IsNullOrEmpty(data.returnUrl)) data.returnUrl = _ssoSetting.DefaultUrl;
    //        return new RedirectResult(data.returnUrl);
    //    }


    //    private bool GetToken(SsoReceiveData data,out string token)
    //    {
    //        var invoker = new WebInvoke(Encoding.UTF8);
    //         token = "";
    //        try
    //        {
    //            var strResult = invoker.Post(_ssoSetting.GetTokenUrl, new Dictionary<string, string>
    //                                                                      { 
    //                                                                          { "grant_type","authorization_code" },
    //                                                                          { "code", data.code },
    //                                                                          { "client_id", _ssoSetting.GetApplicationId },
    //                                                                          { "client_secret", _ssoSetting.GetApplicationSecret },
    //                                                                          { "redirect_uri", _ssoSetting.GetReceivedTicketUrl }
    //                                                                      });
    //            var result = JsonHelper.ConvertToObject<AccessTokenOauthModel>(strResult);
    //            if (result.ret<0)
    //            {
                   
    //                Logger.Information("用户{0}验证token失败", data.userId);
    //                return false;
    //            }
    //            token = result.access_token;
    //            return true;
    //        }
    //        catch (Exception ex)
    //        {
    //            Logger.Information("用户{0}验证ticket时发生错误:{1}", data.userId, ex.ToString());
    //            return false;
    //        }

    //    }


    //    /// <summary>
    //    /// 获取本网站对应的用户
    //    /// </summary>
    //    /// <param name="token"></param>
    //    /// <returns></returns>
    //    private IAccount GetOpenIdCorrespondUser(string token)
    //    {
    //        var invoker = new WebInvoke(Encoding.UTF8);
    //        var strResult = invoker.Post(_ssoSetting.GetOpenIdUrl, new Dictionary<string, string> { { "access_token", token } });
    //        var result = JsonHelper.ConvertToObject<SsoJson<int>>(strResult);
    //        if (result.ret != 0)
    //        {
    //            Logger.Information("获取openId失败!");
    //            throw new QsTechException(string.Format("获取openId失败，返回信息:{0}", result.message))
    //                      {NoAuthorizeLayout = true};
    //        }
    //        var localUser = _userManager.GetUserByOpenId(result.data);
    //        if (localUser != null)   return null; // return localUser;
    //        //当本地不存在openId的用户，则创建一个新用户
    //        strResult = invoker.Post(_ssoSetting.GetUserDetailInfoUrl, new Dictionary<string, string>
    //                                                                       {
    //                                                                           { "access_token", token },
    //                                                                           { "client_id", _ssoSetting.GetApplicationId },
    //                                                                           { "openid",result.data.ToString() }
    //                                                                       });
    //       var userDetail = JsonHelper.ConvertToObject<SsoJson<SsoUserModel>>(strResult);
    //       if (userDetail.ret != 0)
    //       {
    //           Logger.Information("获取用户详细信息失败!");
    //           throw new QsTechException(string.Format("获取用户详细信息失败，返回信息:{0}", userDetail.message)) { NoAuthorizeLayout = true };
    //       }
    //      //  return _userManager.AddSsoUser(userDetail.data);
    //       return null;
    //    }

    //    /// <summary>
    //    /// 客户端登录页面（跳转到AS）
    //    /// </summary>
    //    /// <returns></returns>
    //    [NoAuthorize]
    //    public ActionResult LogOn(string returnUrl)
    //    {
    //        string absulte = GetAbsoluteUrl(returnUrl);
    //        return new RedirectResult(absulte); 
    //    }

    //    /// <summary>
    //    ///  客户端登出(供SSO调用)
    //    /// </summary>
    //    /// <param name="accountName"></param>
    //    /// <returns></returns>
    //    [HttpPost]
    //    [NoAuthorize]
    //    public ActionResult LogOut(string accountName)
    //    {
    //        _svcAuthentication.SignOut(accountName);
    //        return new EmptyResult();
    //    }

    //    /// <summary>
    //    ///  客户端登出
    //    /// </summary>
    //    /// <param name="accountName"></param>
    //    /// <returns></returns>
    //    //[RequireHttps]
    //    public ActionResult LogOut()
    //    {
    //        _svcAuthentication.SignOut();
    //        return new RedirectResult(GetSsoLogOutUrl());
    //    }

    //    private string GetSsoLogOutUrl()
    //    {
    //        var callback = HttpUtility.UrlEncode(string.Format("{0}?returnUrl={1}", _ssoSetting.GetReceivedTicketUrl, _ssoSetting.GetCurrentApplicationUrl));
    //        return string.Format("{0}?redirect_uri={1}", _ssoSetting.SsoLogoutUrl, callback);
    //    }


    //    #region

    //    private void setFormAuthentication(int id)
    //    {
    //        var ul = new UserLite(id, "");
    //        _svcAuthentication.SignIn(null, false);
    //    }

    //    private bool CheckTicketData(SsoReceiveData data)
    //    {
    //        if (string.IsNullOrEmpty(data.code))
    //        {
    //            data.returnUrl = GetAbsoluteUrl(data.returnUrl);
    //            return false;
    //        }
    //        return true;
    //    }

    //    private string GetAbsoluteUrl(string returnUrl)
    //    {
    //        //REVIEW: 不要去SSO绕一圈
    //       // return Url.RedirectClientUrl(ClientAppEnum.供应商管理平台, returnUrl);

    //        return Url.GetLogOnUrl(returnUrl);
    //    }
    //    #endregion


    //}
}
