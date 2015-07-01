using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Authentication.Sso.Models.AccessToken;

namespace QsTech.Authentication.Sso.Models.Oauth2
{
    public class AccountInfoOauthModel: OauthMsgBase
    {
        public AccountInfoOauthModel()
        {
            
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Age { get; set; }

        public string Gender { get; set; }

        public string Telephone { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public DateTime? Birthday { get; set; }

        public string AccountName { get; set; }

        public int OwnerId { get; set; }

        public int OpenId { get; set; }

        public string NickName { get; set; }

    }
}