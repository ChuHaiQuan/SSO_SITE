using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using QsTech.Authentication.Sso.Clients;
using QsTech.Authentication.Sso.Models.Authorization;
using QsTech.Core.Interface;
using QsTech.Framework;
using QsTech.Framework.Caching;
using QsTech.Framework.Security;

namespace QsTech.Authentication.Sso.Models.AccessToken
{
    public interface IAccessTokenManager : ISingletonDependency
    {
        /// <summary>
        /// 根据授权码获取token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        AccessTokenOauthModel GetTokenByAuthorizationCode(TokenModel tokenArg);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        string CreateAndReturnAccessCode(IAccount account);

        /// <summary>
        ///  根据token获取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        UserLite GetAccountInfoByToken(string token,ref string message);

    }


    public class AccessTokenManager : IAccessTokenManager
    {
        private readonly IAuthorizationCodeManager _authorizationCodeManager;

        private readonly ICache<string,UserLite> _cacheAccessToken;
        private readonly ICache<string, UserLite> _cacheRefreshToken;
        private readonly TimeSpan accessDuration = new TimeSpan(0,30, 0);
        private readonly TimeSpan refreshDuration = new TimeSpan(1,30, 0); 
        public AccessTokenManager(IAuthorizationCodeManager authorizationCodeManager,
            ICacheManager cache
            )
        {
            _authorizationCodeManager = authorizationCodeManager;
            _cacheAccessToken = cache.Get<string, UserLite>("accessToken");
            _cacheRefreshToken = cache.Get<string, UserLite>("refreshToken");
        }

        public AccessTokenOauthModel GetTokenByAuthorizationCode(TokenModel tokenArg)
        {
            string msgref = "";
            var ul = _authorizationCodeManager.GetUserInfoByAuthorizationCode(tokenArg.code, ref msgref);
            if (ul == null)
                return new AccessTokenOauthModel(){ ret = -10010, message = msgref};
            var validClient = CheckClient(tokenArg, ref msgref);
            if (!validClient) return new AccessTokenOauthModel() { ret = -10011, message = msgref };
            var token = GetMd5Str(Guid.NewGuid().ToString());
            _cacheAccessToken.GetOrAdd(token, m =>
                                                  {
                                                      m.Token = new DurationToken(accessDuration);
                                                      return new UserLite(ul.UserId,ul.UserName);
                                                  });
            return new AccessTokenOauthModel(){ access_token = token, expires_in = (int)accessDuration.TotalSeconds};
        }


        public string CreateAndReturnAccessCode(IAccount account)
        {
            var token = GetMd5Str(Guid.NewGuid().ToString());
            _cacheAccessToken.GetOrAdd(token, m =>
            {
                m.Token = new DurationToken(accessDuration);
                return new UserLite(account.Id, account.Name);
            });
            return token;
        }

        public UserLite GetAccountInfoByToken(string token,ref string message)
        {
            UserLite ul;
            if (_cacheAccessToken.TryGet(token, out ul))
            {
                var validtoken = _cacheAccessToken.GetToken(token);
                if (!validtoken.IsValid())
                {
                    message = "access_token已过期";
                    return null;
                }
                return ul;
            }
            else
            {
                message = "不是有效的access_token";
                return null;
            }
        }


        private  bool CheckClient(TokenModel tokenArg,ref string message)
        {
            return true;
        }


        private  string GetMd5Str(string convertString)
        {
            var md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(convertString)));
            t2 = t2.Replace("-", "");
            return t2;
        }

    }
}