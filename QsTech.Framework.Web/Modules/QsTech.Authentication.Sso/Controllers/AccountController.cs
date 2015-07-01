using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using QsTech.Authentication.Sso.Models.Authorization;
using QsTech.Core.Interface;
using QsTech.Core.Interface.AccountRegister;
using QsTech.Core.Interface.Clients;
using QsTech.Framework;
using QsTech.Framework.Json;
using QsTech.Framework.Localization;
using QsTech.Framework.Security;
using QsTech.Authentication.Sso.Models;
using QsTech.Framework.Logging;
using QsTech.Authentication.Sso.Clients;
using System.Web.UI;

namespace QsTech.Authentication.Sso.Controllers
{

    public class AccountController : Controller
    {
        private readonly IAuthenticationService _svcAuthentication;
        private readonly IAuthService _svcAuth;
        private readonly IAuthorizationCodeManager _authorizationCodeManager;
        private readonly IClientVerifier _clientVerifier;
        private readonly ITranslation _translation;
        private readonly IAccountRegisterManager _accountRegisterManager;
        private readonly IClientManager _clientManager;
        private readonly IValidateCodeBuilder _validateCodeBuilder;
        private readonly IAuthorizationCodeHolder _authorizationCodeHolder;
        private readonly ISsoSetting _ssoSetting;

        public AccountController(ISsoSetting ssoSetting, IAuthenticationService svcAuthentication, IAuthService svcAuth,
            IAuthorizationCodeManager authorizationCodeManager, 
            IClientVerifier clientVerifier,
            ITranslation translation,
            IAccountRegisterManager accountRegisterManager,
            IClientManager clientManager,
            IValidateCodeBuilder validateCodeBuilder,
            IAuthorizationCodeHolder authorizationCodeHolder)
        {
            _svcAuthentication = svcAuthentication;
            _svcAuth = svcAuth;
            _authorizationCodeManager = authorizationCodeManager;
            _clientVerifier = clientVerifier;
            _translation = translation;
            _accountRegisterManager = accountRegisterManager;
            _clientManager = clientManager;
            _validateCodeBuilder = validateCodeBuilder;
            _authorizationCodeHolder = authorizationCodeHolder;
            _ssoSetting = ssoSetting;
            Logger =NullLogger.Instance;
        }
        public ILogger Logger { get; set; }

        //[RequireHttps]
        [NoAuthorize(true)]
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, Location = OutputCacheLocation.None, VaryByParam = "*")]
        public ActionResult LogOn(AuthorizeRequestModel model)
        {
            CheckReturnUrl(model);
            var directurl = AutoLogOn(model);
            if (!string.IsNullOrEmpty(directurl))
            {
                return Redirect(directurl);
            }
            if (IsMobileBrowser())
                return RedirectToAction("MLogOn", model);
            return View();
        }


          [NoAuthorize(true)]
        public ActionResult MLogOn(AuthorizeRequestModel model)
        {
            return View();
        }

        /// <summary>
        /// 判断是否为移动端，
        /// </summary>
        /// <returns></returns>
          private bool IsMobileBrowser()
          {
              HttpContextBase context =this.HttpContext;
              if (context.Request.Browser.IsMobileDevice)
              {
                  return true;
              }
              if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
              {
                  return true;
              }
              if (context.Request.ServerVariables["HTTP_ACCEPT"] != null &&
              context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
              {
                  return true;
              }
              if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
              {
                  string[] mobiles =
                  new[]
{
"midp", "j2me", "avant", "docomo",
"novarra", "palmos", "palmsource",
"240x320", "opwv", "chtml",
"pda", "windows ce", "mmp/",
"blackberry", "mib/", "symbian",
"wireless", "nokia", "hand", "mobi",
"phone", "cdm", "up.b", "audio",
"SIE-", "SEC-", "samsung", "HTC",
"mot-", "mitsu", "sagem", "sony"
, "alcatel", "lg", "eric", "vx",
"NEC", "philips", "mmm", "xx",
"panasonic", "sharp", "wap", "sch",
"rover", "pocket", "benq", "java",
"pt", "pg", "vox", "amoi",
"bird", "compal", "kg", "voda",
"sany", "kdd", "dbt", "sendo",
"sgh", "gradi", "jb", "dddi",
"moto", "iphone"
};
                  foreach (string s in mobiles)
                  {
                      if (context.Request.ServerVariables["HTTP_USER_AGENT"].
                      ToLower().Contains(s.ToLower()))
                      {
                          return true;
                      }
                  }
              }

              return false;
          }

        [HttpPost]
        //[RequireHttps]
        [NoAuthorize]
        public ActionResult LogOn(LogOnModel model, AuthorizeRequestModel requestModel)
        {
            CheckReturnUrl(requestModel);
            if (ModelState.IsValid && CheckValidateCode(model))
            {
                model.AccountName = model.AccountName.Trim();
                IAccount account;
                if (_svcAuth.Login(model.AccountName, model.Password, out account))
                {
                    CheckClient(requestModel, account.Id);  //判断用户有无权限访问应用  暂时注释；2012-8/1
                    _svcAuthentication.SignIn(account, model.RememberMe);
                   var  returnUrl= _authorizationCodeHolder.GetAuthorizationProvider(requestModel.response_type??"").GetAuthorizeReponseUrl(account, requestModel);
                   return GetReturlUrl(requestModel, returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", _translation.T("loginFormAccountError"));
                }
            }
            if (model.SourceType == 1)
                return View("MLogOn", model);
            else
                return View(model);
        }


        private ActionResult GetReturlUrl(AuthorizeRequestModel model,string returnUrl)
        {
            if (string.IsNullOrEmpty(model.redirect_uri))
                return Redirect(_ssoSetting.DefaultUrl);
            if (!IsCurrentUrl(model.redirect_uri))
            {
                var client = _clientManager.GetClientByApplicationId(model.client_id);
                if (client.IsClient)
                {
                    CookieHelper.AddCookie("url", returnUrl);
                    CookieHelper.AddCookie("requestmodel", JsonHelper.ConvertToJson(model));
                    CookieHelper.AddCookie("client", JsonHelper.ConvertToJson(client));
                    return Redirect(_ssoSetting.GetClientInfoUrl);
                }
                return Redirect(returnUrl);
            }
            return Redirect(model.redirect_uri);
        }

        public ActionResult ClientInfo()
        {
            var url = CookieHelper.GetCookie("url").Value;
            var logoutUrl = CookieHelper.GetCookie("requestmodel").Value;
            var clientstr= CookieHelper.GetCookie("client").Value;
            var requstmodel= JsonHelper.ConvertToObject<AuthorizeRequestModel>(logoutUrl);
            var client= JsonHelper.ConvertToObject<Client>(clientstr);
            ViewBag.Url = url;
            ViewBag.RequestModel = requstmodel;
            ViewBag.Client = client;
            ViewBag.Open = string.Format("{0}?redirect_uri={1}&client_id={2}&response_type=code",
                                           _ssoSetting.LogOnUrl,HttpUtility.UrlEncode(client.Callback),client.ApplicationId);  
            return View();
        }

        
        [NoAuthorize(true)]
        [HttpGet]
        public ActionResult ClientLogOut()
        {
            if (!this.Request.IsAuthenticated)
                return new RedirectResult(_ssoSetting.LogOnUrl);
            _svcAuthentication.SignOut();
            var clientstr = CookieHelper.GetCookie("client").Value;
            CookieHelper.RemoveCookie("client");
            CookieHelper.RemoveCookie("requestmodel");
            CookieHelper.RemoveCookie("url");
            var client = JsonHelper.ConvertToObject<Client>(clientstr);
            var url = string.Format("{0}?redirect_uri={1}&client_id={2}&response_type=code",
                                       _ssoSetting.LogOnUrl,HttpUtility.UrlEncode(client.Callback), client.ApplicationId);
            return Redirect(url);
        }


        private bool CheckValidateCode(LogOnModel model)
        {
            //#if DEBUG
            //return true;
            //#endif
            if (model.ValidateCode == null)
                return true;
            var code=Session["ValidateCode"];
            if (code==null||(model.ValidateCode.ToUpper() != code.ToString().ToUpper()))
            {
                ModelState.AddModelError("",  _translation.T("loginFormValidCodeError"));
                return false;
            }
            return true;
            
        }
        
       //[Authorize]
        
        /// <summary>
        /// 用户显示登出
        /// </summary>
        /// <returns></returns>
        [NoAuthorize(true)]
        public ActionResult LogOut(string redirect_uri)
        {
            if (!this.Request.IsAuthenticated) 
                return new RedirectResult(_ssoSetting.LogOnUrl);
            //Logger.Error(string.Format("returnUrl={0}", redirect_uri));
            _svcAuthentication.SignOut();
            if (string.IsNullOrEmpty(redirect_uri))
            {
                var hosturl = Request.Url.Authority;
                return new RedirectResult(string.Format("http://{0}{1}", hosturl,_ssoSetting.LogOnUrl));
            }
          
            else
            {
                return new RedirectResult(redirect_uri);
            }
        }




        #region 验证码
        [NoAuthorize(true)]
        public ActionResult GetValidateCode()
        {
            var codeGraphics = new ValidateCodeGraph(this._validateCodeBuilder);
            Session["ValidateCode"] = codeGraphics.CodeDescriptor.Value;
            Session.Timeout = 5; //2分钟
            byte[] bytes = codeGraphics.CreatePicture();
            return File(bytes, @"image/jpeg"); ;
        }
        #endregion

        #region 自动登录，及获取返回地址 //REVIEW：讨论有没有必要放到Service
        private string GetTicketReturnUrl(string ticketData, AuthorizeRequestModel authorizeModel)
        {
            if (string.IsNullOrEmpty(authorizeModel.redirect_uri))
                return _ssoSetting.DefaultUrl;
            var returnUrl = authorizeModel.redirect_uri;
            if (!IsCurrentUrl(authorizeModel.redirect_uri))
            {
                try
                {
                    returnUrl=EncodeReturnUrl(returnUrl);
                    returnUrl = returnUrl.AppendUrlParam("code", ticketData);
                    returnUrl = returnUrl.AppendUrlParam("state", authorizeModel.state);
                    return returnUrl;
                }
                catch(Exception ex) {
                    Logger.Error("跳转回应用时发生错误:{0}!",ex.ToString());
                    throw ex;
                }
            }
            else
                return returnUrl;
        }


        /// <summary>
        /// 是否是当前的应用
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private bool IsCurrentUrl(string url)
        {
            //Logger.Error(string.Format("访问的url={0}", url));
            var visit = new Uri(url);
            var current = Request.Url;   // new Uri(_ssoSetting.GetCurrentApplicationUrl); //修改
           // Logger.Error(string.Format("url={0},currentUrl={1}",url,_ssoSetting.GetCurrentApplicationUrl));
            return visit.Authority == current.Authority;
        }
        private string EncodeReturnUrl(string returnUrl)
        {
            try
            {
                var receivedUrl = returnUrl.Substring(0, returnUrl.IndexOf('=') + 1);
                if (string.IsNullOrEmpty(receivedUrl)) return returnUrl;
                var rightStr = returnUrl.Substring(returnUrl.IndexOf('=') + 1, returnUrl.Length - receivedUrl.Length);
                return string.Format("{0}{1}", receivedUrl, HttpUtility.UrlEncode(rightStr));
            }
            catch
            {
                return _ssoSetting.DefaultUrl;
            }
        }

        private void CheckReturnUrl(AuthorizeRequestModel model)
        {
            #if DEBUG
            return ;
            #endif
            if (string.IsNullOrEmpty(model.redirect_uri))
                return;
            if (!IsCurrentUrl(model.redirect_uri))
            {//如果不是本地的URL
                var isMatch = _clientVerifier.Macth(model);
                if (!isMatch)
                {
                    Logger.Error(string.Format("不是有效的return_uri={0}", model.redirect_uri));
                    throw new QsTechException("不是有效的return_uri") { NoAuthorizeLayout = true };
                }
            }

        }

        private string AutoLogOn(AuthorizeRequestModel model)
        {
            if (HttpContext.Request.IsAuthenticated)
            {
                Logger.Information(string.Format("{0}自动登录!", HttpContext.User.Identity.Name));
                var account = _svcAuthentication.GetAuthenticatedAccount();    //_svcAuthentication.GetAuthenticatedUser();
                var data = _authorizationCodeManager.CreateAndReturnAuthorizationCode(account);
                var returnUrl = GetTicketReturnUrl(data, model);
                Logger.Information(string.Format("自动登录返回:{0}", returnUrl));
                return returnUrl;
            }
            return null;
        }


        private void CheckClient(AuthorizeRequestModel model, int userId)
        {
            if (string.IsNullOrEmpty(model.redirect_uri))
                return;
            if (!IsCurrentUrl(model.redirect_uri))
            {//如果不是本地的URL
                var reuslt = _clientVerifier.Match(model, userId);
                if (!reuslt)
                {
                    throw new QsTechException("没有访问权限应用!"){NoAuthorizeLayout = true};
                }
            }
        }
        #endregion


        #region  注册部分

        [NoAuthorize(true)]
        public ActionResult Register(string redirect_uri,string applicationId)
        {
            var model = new RegisterModel()
                            {ApplicationId = applicationId,
                                redirect_uri = redirect_uri
                            };
            if (string.IsNullOrEmpty(applicationId))
            {
                ModelState.AddModelError("", "申请账户链接没有应用来源ID，不能申请注册，请联系系统管理员!");
                ViewBag.Error = true;
                return View(model);
            }
            var client = _clientManager.GetClientByApplicationId(model.ApplicationId);
            if (client == null)
            {
                ModelState.AddModelError("", "申请账户的应用来源有错误，不能申请注册，请联系系统管理员!");
                ViewBag.Error = true;
                return View(model);
            }
            return View(model);
        }

        [NoAuthorize(true)]
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var remark = new  StringBuilder();
                    remark.Append(string.Format("公司名称:{0}", model.CompanyName));
                    remark.Append("<br/>");
                    remark.Append(string.Format("公司地址:{0}", model.CompanyAddress));
                    remark.Append("<br/>");
                    remark.Append(string.Format("公司联系人:{0}", model.CompanyLinkman));
                    remark.Append("<br/>");
                    remark.Append(string.Format("公司联系电话:{0}", model.CompanyLinkmanPhone));
                    remark.Append("<br/>");
                    if(string.IsNullOrEmpty(model.ApplicationId))
                    {
                        ModelState.AddModelError("", "申请账户没有应用来源ID，不能申请注册，请联系系统管理员!");
                       
                        return View(model);
                    }
                    var client = _clientManager.GetClientByApplicationId(model.ApplicationId);
                    if(client!=null)
                    {
                        remark.Append("<br/>");
                        remark.Append(string.Format("来自:{0}", client.Name));
                    }
                    else
                    {
                        ModelState.AddModelError("", "申请账户的应用来源有错误，不能申请注册,请联系系统管理员!");
                        return View(model);
                    }
                    
                    
                    _accountRegisterManager.Register(new AccountRegisterInfo
                    {
                        CompanyName = model.CompanyName,
                        Name = model.Name,
                        LoginName = model.LoginName,
                        LoginPassword = model.LoginPassword,
                        Gender = model.Gender,
                        Age = model.Age,
                        Birthday = model.Birthday,
                        MobilePhone = model.MobilePhone,
                        Email = model.Email,
                        Telephone = model.Telephone,
                        CreateDate = DateTime.Now,
                        Remark = remark.ToString(),
                        ApplicationId=model.ApplicationId
                    });
                    var redreict = string.IsNullOrEmpty(model.redirect_uri)?client.Host:model.redirect_uri;
                    return RedirectToAction("RegisterSuccess", new RouteValueDictionary { { "ln", model.Name }, { "redirect_uri", redreict } });
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "用户申请注册时发生错误。");
                    ModelState.AddModelError("", _translation.T("UserRegisterError"));
                }
            }
            return View(model);
        }

        [NoAuthorize(true)]
        public ActionResult RegisterSuccess(string ln,string redirect_uri)
        {
            return View(new RegisterSuccessModel { LoginName = ln, ReturnUrl = redirect_uri });
        }
        #endregion

    }
}
