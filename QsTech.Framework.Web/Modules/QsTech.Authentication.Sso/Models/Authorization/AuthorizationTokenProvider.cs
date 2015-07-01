using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Authentication.Sso.Models.AccessToken;
using QsTech.Framework.Caching;
using QsTech.Framework.Security;

namespace QsTech.Authentication.Sso.Models.Authorization
{
    public class AuthorizationTokenProvider:IAuthorizationProvider
    {
        private readonly ISsoSetting _ssoSetting;
        private readonly IAccessTokenManager _accessTokenManager;
        private readonly TimeSpan _duration = new TimeSpan(0, 5, 0);
        public AuthorizationTokenProvider(ISsoSetting ssoSetting,
            IAccessTokenManager accessTokenManager)
        {
            _ssoSetting = ssoSetting;
            _accessTokenManager = accessTokenManager;
        }

        public string ReponseType
        {
            get { return "token"; }
        }


        public string GetAuthorizeReponseUrl(IAccount account, AuthorizeRequestModel authorizeModel)
        {
            var accessToken = _accessTokenManager.CreateAndReturnAccessCode(account);
            if (string.IsNullOrEmpty(authorizeModel.redirect_uri))
                return _ssoSetting.DefaultUrl;
            var returnUrl = authorizeModel.redirect_uri;
            //if (!IsCurrentUrl(authorizeModel.redirect_uri))
            //{
                try
                {
                    returnUrl = EncodeReturnUrl(returnUrl);
                    returnUrl = returnUrl.AppendTokenParam("access_token", accessToken);
                    returnUrl = returnUrl.AppendTokenParam("state", authorizeModel.state);
                    returnUrl = returnUrl.AppendTokenParam("expires_in", _duration.TotalSeconds.ToString("D5"));
                    return returnUrl;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            //}
            //else
            //    return returnUrl;
        }

        #region

        //private bool IsCurrentUrl(string url)
        //{
        //    var visit = new Uri(url);
        //    var current =new Uri(_ssoSetting.GetCurrentApplicationUrl); //修改
        //    return visit.Authority == current.Authority;
        //}

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