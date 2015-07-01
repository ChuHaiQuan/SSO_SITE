using System;
using System.Collections.Generic;
using System.Web;
using QsTech.Core.Interface.Clients;
using QsTech.Framework.Security;
using QsTech.Framework;
using System.Web.Security;
using QsTech.Framework.Utility;
using QsTech.Framework.Logging;
using QsTech.Authentication.Sso.Clients;
using QsTech.Core.Interface;

namespace QsTech.Authentication.Sso.Models.Authentication
{
    //[Feature("Server")]
    public class DefaultAuthentication : IAuthenticationService
    {
        private readonly IClientManager _clientManager;
        private readonly IAccountManager _accountManager;
        private IAccount _currentAccount;
        public DefaultAuthentication(IClientManager clientManager,
            IAccountManager accountManager) 
        {
            Logger =NullLogger.Instance;
            _clientManager = clientManager;
            _accountManager = accountManager;
        }

        public ILogger Logger { get; set; }

        public void SignIn(IAccount account, bool createPersistentCookie)
        {
            var token=string.Format("{0}", account.Id);
            FormsAuthentication.SetAuthCookie(token, createPersistentCookie);
        }


        public void SignOut(string userId=null)
        {
            FormsAuthentication.SignOut();
            //foreach (var item in _clientManager.GetClients())
            //{
            //    try
            //    {
            //        WebInvoke _invoke = new WebInvoke();
            //        _invoke.Post(item.LogOut, new Dictionary<string, string> { { "id", userId } });
            //    }
            //    catch (Exception ex)
            //    {
            //        Logger.Information("用户ID{0}注销子应用{1}时出错,错误信息:{2}", userId, item.Name, ex.ToString());
            //    }
            //}
        }

        public void SetAuthenticatedAccountForRequest(IAccount account)
        {
            _currentAccount = account;
        }

        public IAccount GetAuthenticatedAccount()
        {
            if (_currentAccount != null)
            {
                return _currentAccount;
            }
            var  paramId = HttpContext.Current.User.Identity.Name;
            int accountId;
            if (int.TryParse(paramId, out accountId))
            {
                return _accountManager.GetAccountById(accountId);
            }
            return null;
        }

        public bool IsAuthenticated()
        {
            if (_currentAccount != null)
            {
                return true;
            }
            return HttpContext.Current.Request.IsAuthenticated;
        }

        public IUser GetAuthenticatedUser()
        {
            //if (_currentAccount != null)
            //{
            //    return _currentAccount;
            //}
            var paramId = HttpContext.Current.User.Identity.Name;
            int accountId;
            if (int.TryParse(paramId, out accountId))
            {
                var account= _accountManager.GetAccountById(accountId);
                return new UserLite(account.Id,account.Name);
            }
            return null;
          // return new TicketUserInfo(loginName);
        }


        public string GetLogOnUrl(string returnUrl)
        {
            //throw new NotImplementedException();
            return string.Format("{0}?returnUrl={1}", "/Authentication/Account/LogOn", HttpUtility.UrlEncode(returnUrl)); 
        }
    }
}