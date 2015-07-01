using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Framework;
using QsTech.Framework.Caching;
using QsTech.Framework.Security;

namespace QsTech.Authentication.Sso.Models
{
    public interface ITicketManager : ISingletonDependency
    {
        SsoTicketData GetTicket(TicketUserInfo userInfo);

        IValidateToken GetToken(string key);

        string GetAuthorizeCode(string key);

        string AddAuthrizeCode(string userId);
    }

    public class TicketUserInfo
    {
        public TicketUserInfo()
        {
        }
        public TicketUserInfo(int userId)
        {
            Id = userId;
        }
        public TicketUserInfo(IUser user)
        {
            Id = user.Id;
            Name = user.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// 授权码
        /// </summary>
        public string AuthorizeCode { get; set; }

    }

}