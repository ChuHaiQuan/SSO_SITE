using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QsTech.Authentication.Sso.Models.AccessToken
{

    public class TokenModel
    {
        /// <summary>
        /// 必须 申请SSO，分配给网站的appid
        /// </summary>
        public string client_id { get; set; }

        /// <summary>
        ///必须 申请SSO，分配给网站的appkey。
        /// </summary>
        public string client_secret { get; set; }

        /// <summary>
        ///必须 授权类型，此值固定为“authorization_code”。
        /// </summary>
        public string grant_type { get; set; }

        /// <summary>
        ///必须 授权码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        ///必须  回调地址参数
        /// </summary>
        public string redirect_uri { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string scope { get; set; }
    }


    public class AccessTokenOauthModel:OauthMsgBase
    {
        /// <summary>
        /// Access token
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 刷新令牌
        /// </summary>
        public string refresh_token { get; set; }

        /// <summary>
        /// access token的类型目前只支持bearer
        /// </summary>
        public string token_type { get; set; }

        /// <summary>
        /// Access token过期时间 10（表示10秒后过期）
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        /// 刷新令牌的过期时间
        /// </summary>
        public int re_expires_in { get; set; }

        ///// <summary>
        ///// 错误值
        ///// </summary>
        //public string error { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public string error_description { get; set; }
    }
}