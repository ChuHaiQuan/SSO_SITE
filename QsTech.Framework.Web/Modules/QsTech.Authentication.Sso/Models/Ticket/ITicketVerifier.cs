using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QsTech.Authentication.Sso.Models.AccessToken;
using QsTech.Framework;
using QsTech.Authentication.Sso.Models.ticket;
using QsTech.Framework.Security;

namespace QsTech.Authentication.Sso.Models
{
    public interface ITicketVerifier : ISingletonDependency
    {
        TicketVerifyResult Verify(TicketUserInfo user, string ticket);

        AccessTokenOauthModel GetToken(TokenModel model);
    }

    public class DefaultTicketVerifier : ITicketVerifier
    {
        private readonly ITicketManager _ticketManager;
        public DefaultTicketVerifier(ITicketManager ticketManager)
        {
            _ticketManager = ticketManager;
        }

        public TicketVerifyResult Verify(TicketUserInfo user, string ticket)
        {
            var ticketCache = _ticketManager.GetTicket(user);
            var result = _ticketManager.GetToken(user.Id.ToString()).IsValid();
            if (result)
            { 
               return new TicketVerifyResult(ticketCache.Ticket==ticket);
            }
            return new TicketVerifyResult(result);
        }

        public AccessTokenOauthModel GetToken(TokenModel model)
        {
            var ticketCache = _ticketManager.GetAuthorizeCode(model.code);
            var result = _ticketManager.GetToken(model.code).IsValid();
            if (result)
            {
               return new AccessTokenOauthModel()
                          {
                               access_token = Guid.NewGuid().ToString(),
                               expires_in = 3000
                          };
            }

            throw new QsTechException("sadfsadf");
        }
    }
}
