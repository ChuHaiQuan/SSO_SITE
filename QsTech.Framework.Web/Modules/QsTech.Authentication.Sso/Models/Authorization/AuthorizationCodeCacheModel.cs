using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QsTech.Authentication.Sso.Models.Authorization
{
    public class AuthorizationCodeCacheModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        /// <summary>
        /// 返回的地址
        /// </summary>
        public string redirect_url { get; set; }

        /// <summary>
        /// 客户端id
        /// </summary>
        public string client_id { get; set; }
    }
}