using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QsTech.Authentication.Sso.Models.AccessToken
{
    public class AccountOauthModel:OauthMsgBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //public string error { get; set; }

        //public string error_description { get; set; }
    }
}