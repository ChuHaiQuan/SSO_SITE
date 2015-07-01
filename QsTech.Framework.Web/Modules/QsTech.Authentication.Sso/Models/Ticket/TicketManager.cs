using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Framework;
using QsTech.Framework.Caching;
using QsTech.Framework.Security;

namespace QsTech.Authentication.Sso.Models
{
    public class TicketManager:ITicketManager
    {
        private readonly ICache<string, SsoTicketData> _cacheticket;

        private readonly TimeSpan Duration = new TimeSpan(0, 5, 0);
        public TicketManager(ICacheManager cache)
        {
            _cacheticket = cache.Get<string, SsoTicketData>("Ticket");
        }


        public SsoTicketData GetTicket(TicketUserInfo user)
        {
           var ticket = CreateTicket(user);
            user.AuthorizeCode = ticket;
           //return _cacheticket.GetOrAdd(user.Id.ToString(), m =>
            return _cacheticket.GetOrAdd(user.AuthorizeCode, m =>
           {
                m.Token = new DurationToken(Duration);
                return new SsoTicketData() {
                Ticket=ticket,
                IssueTime= System.DateTime.Now,
                AccountName = user.Id.ToString(),
                Duration = Duration
               };
            });
        }


        private string CreateTicket(TicketUserInfo user)
        {
            return Guid.NewGuid().ToString().Replace("-","");
        }

        private string CreateTicket()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
        public IValidateToken GetToken(string ticket)
        {
            return _cacheticket.GetToken(ticket);
        }

        public string GetAuthorizeCode(string key)
        {
            SsoTicketData data;
            if (_cacheticket.TryGet(key, out data))
            {
                return data.AccountName;
            }
            else
            {
                
                throw new QsTechException("不存在授权码!");
            }
        }

        public string AddAuthrizeCode(string userId)
        {
            var authorizeCode = CreateTicket();
             _cacheticket.GetOrAdd(authorizeCode, m =>
            {
                m.Token = new DurationToken(Duration);
                return new SsoTicketData()
                {
                    Ticket = authorizeCode,
                    IssueTime = System.DateTime.Now,
                    AccountName = userId,
                    Duration = Duration
                };
            });
            return authorizeCode;
        }
    }
}