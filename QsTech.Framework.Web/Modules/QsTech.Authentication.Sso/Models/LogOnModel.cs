using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using QsTech.Framework.Security;

namespace QsTech.Authentication.Sso.Models
{
    public class LogOnModel
    {
        //[Required( ErrorMessage="请输入登录帐号!")]
        [Display(Name = "账户名:")]
         public string AccountName { get; set; }

        public int Id { get; set; }

        //[Required(ErrorMessage="请输入密码!")]
        [DataType(DataType.Password)]
        [Display(Name = "密码:")]
        public string Password { get; set; }

        [Display(Name = "下次自动登录")]
        public bool RememberMe { get; set; }
       // [Required(ErrorMessage = "请输入验证码!")]
        public string ValidateCode { get; set; }

        /// <summary>
        /// 提交来源 0 : 普通浏览器；1:移动端 
        /// </summary>
        public int SourceType { get; set; }

    }
}