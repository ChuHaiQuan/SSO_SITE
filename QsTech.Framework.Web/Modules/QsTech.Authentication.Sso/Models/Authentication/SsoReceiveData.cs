using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QsTech.Authentication.Sso.authentication
{
    public class SsoReceiveData
    {
        public string returnUrl{get;set;}

        public string ticket{get;set;}

        public string accountName{get;set;}

        public int userId { get; set; }

        public string code { get; set; }

        public string state { get; set; }

     
    }
}