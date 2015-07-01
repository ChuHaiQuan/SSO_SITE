using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QsTech.Authentication.Sso.Models.Authorization
{
    public class AuthorizeRequestModel
    {
        /// <summary>
        /// 必须; 分配给应用的appid。
        /// </summary>
        public string client_id { get; set; }
        /// <summary>
        /// 必须; 授权类型，授权码模式:“code”。隐式授权:"token"。目前支持两种
        /// </summary>
        public string response_type { get; set; }
        /// <summary>
        /// 必须; 成功授权后的回调地址，必须是注册appid时填写的主域名下的地址。
        /// </summary>
        public string redirect_uri { get; set; }
        /// <summary>
        /// 抵制CSRF攻击
        /// </summary>
        public string state { get; set; }

        public string scope { get; set; }


    }
}