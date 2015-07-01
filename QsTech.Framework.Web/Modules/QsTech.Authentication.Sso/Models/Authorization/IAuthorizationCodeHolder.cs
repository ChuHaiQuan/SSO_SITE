using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Framework;
using QsTech.Framework.Security;

namespace QsTech.Authentication.Sso.Models.Authorization
{
    public interface IAuthorizationCodeHolder : ISingletonDependency
    {

        IAuthorizationProvider GetAuthorizationProvider(string requesttype);
    }



    public class AuthorizationCodeHoder : IAuthorizationCodeHolder
    {
        private readonly IEnumerable<IAuthorizationProvider> _authorizationProviders;

        public AuthorizationCodeHoder(IEnumerable<IAuthorizationProvider>  authorizationProviders)
        {
            _authorizationProviders = authorizationProviders;
        }


        public IAuthorizationProvider GetAuthorizationProvider(string requesttype)
        {
            return    _authorizationProviders.SingleOrDefault(m => m.ReponseType == requesttype);
        }
    }



    public class DefaultAuthorizationProvider : IAuthorizationProvider
    {

        private readonly ISsoSetting _ssoSetting;

        public DefaultAuthorizationProvider(ISsoSetting ssoSetting)
        {
            _ssoSetting = ssoSetting;
        }

        public string ReponseType
        {
            get { return  ""; }
        }

        public string GetAuthorizeReponseUrl(IAccount account, AuthorizeRequestModel authorizeModel)
        {
            if (string.IsNullOrEmpty(authorizeModel.redirect_uri))
                return _ssoSetting.DefaultUrl;
            return authorizeModel.redirect_uri;
        }

    }
}