using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Core.Interface;
using QsTech.Framework;
using QsTech.Framework.Caching;
using QsTech.Framework.Security;

namespace QsTech.Authentication.Sso.Models.Authorization
{
    public class AuthorizationCodeManager:IAuthorizationCodeManager
    {
        private readonly IRandomStringManager _randomStringManager;
        private readonly ICache<string, AuthorizationCodeCacheModel> _cacheAuthrizationCode;
        private readonly TimeSpan _duration = new TimeSpan(0, 5, 0);

        public AuthorizationCodeManager(ICacheManager cache,IRandomStringManager randomStringManager)
        {
            _randomStringManager = randomStringManager;
            _cacheAuthrizationCode = cache.Get<string, AuthorizationCodeCacheModel>("authrizationCode");
        }

        public string CreateAndReturnAuthorizationCode(IAccount account)
        {
            var code = CreateAuthorizationCode();
            _cacheAuthrizationCode.GetOrAdd(code, m =>
            {
                m.Token = new ManualDurationToken(_duration);
                return  new AuthorizationCodeCacheModel() 
                {
                     UserId= account.Id,
                     UserName=account.Name
                };
            });
            return code;
        }

        private string CreateAuthorizationCode()
        {
            do
            {
                var code = _randomStringManager.GetRandomNumberAndCharacters(22);
                AuthorizationCodeCacheModel acc;
                if (!_cacheAuthrizationCode.TryGet(code, out acc))
                    return code;
            } while (true);
        }

        public AuthorizationCodeCacheModel GetUserInfoByAuthorizationCode(string code, ref string msgref)
        {
            AuthorizationCodeCacheModel ul;
            if (_cacheAuthrizationCode.TryGet(code, out ul))
            {
                var validtoken=_cacheAuthrizationCode.GetToken(code);
                if(!validtoken.IsValid())
                {
                    msgref = "授权码已过期";
                    return null;
                }
                validtoken.Invalidate();//使用一次后,使授权码失效
                return ul;
            }
            else
            {
                msgref = "不是有效的授权码";
                return null;
            }
        }
    }
}