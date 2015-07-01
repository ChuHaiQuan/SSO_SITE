using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QsTech.Authentication.Sso.Models.AccessToken
{
    public interface IOauthMsg
    {
        /// <summary>
        /// 返回码
        /// </summary>
        int ret { get; }
        /// <summary>
        /// 如果ret<0，会有相应的错误信息提示
        /// </summary>
        string message { get; }
    }

    public class OauthMsgBase:IOauthMsg
    {
        public int ret { get;  set; }
        public string message { get;  set; }
    }
}