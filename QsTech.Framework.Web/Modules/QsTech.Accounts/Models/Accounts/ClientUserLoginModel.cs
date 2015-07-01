using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QsTech.Accounts.Models.Accounts
{
    public class ClientUserLoginModel
    {

        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ClientSecret { get; set; }

    }
}