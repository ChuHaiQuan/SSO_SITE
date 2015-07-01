using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Framework.Caching;
using QsTech.Framework.Security;

namespace QsTech.Authentication.Sso.Models.Authorization
{
    public class AuthorizationCodeProvider : IAuthorizationProvider
    {

        private readonly IAuthorizationCodeManager _authorizationCodeManager;
        private readonly ISsoSetting _ssoSetting;
        public AuthorizationCodeProvider(IAuthorizationCodeManager authorizationCodeManager,ISsoSetting ssoSetting)
        {
            _authorizationCodeManager = authorizationCodeManager;
            _ssoSetting = ssoSetting;
        }

        public string ReponseType
        {
            get { return "code"; }
        }

        public string GetAuthorizeReponseUrl(IAccount account, AuthorizeRequestModel authorizeModel)
        {
            var code = _authorizationCodeManager.CreateAndReturnAuthorizationCode(account);
            if (string.IsNullOrEmpty(authorizeModel.redirect_uri))
                return _ssoSetting.DefaultUrl;
            var returnUrl = authorizeModel.redirect_uri;
                try
                {
                    returnUrl = EncodeReturnUrl(returnUrl);
                    returnUrl = returnUrl.AppendUrlParam("code", code);
                    returnUrl = returnUrl.AppendUrlParam("state", authorizeModel.state);
                    return returnUrl;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

        }

        #region

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
        #endregion
    }
}