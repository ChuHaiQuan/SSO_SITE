using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Authentication.Sso.Models.AccessToken;

namespace QsTech.Authentication.Sso.Models.Oauth2
{
    public class OpenIdOauthModel : OauthMsgBase
    {
        public int openid { get; set; }

        public string client_id { get; set; }

        public string AccountName { get; set; }
    }
}