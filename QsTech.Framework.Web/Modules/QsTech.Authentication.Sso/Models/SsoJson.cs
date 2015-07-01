using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QsTech.Authentication.Sso.Models
{
    public class SsoJson<T>
    {
        public T data { get; set; }

        /// <summary>
        /// 返回码
        /// </summary>
        public int ret { get; set; }

        /// <summary>
        /// 如果ret<0，会有相应的错误信息提示
        /// </summary>
        public string message { get; set; }
    }
}