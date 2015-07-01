using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QsTech.Authentication.Sso.Models;
using QsTech.Authentication.Sso.Models.ticket;
using QsTech.Framework.Security;
using QsTech.Framework.Json;
using QsTech.Framework.Logging;
using QsTech.Authentication.Sso.authentication;

namespace QsTech.Authentication.Sso.Controllers
{
    public class TicketVerifyController : Controller
    {
        private readonly ITicketVerifier _ticketVerifier;
        private readonly IAuthenticationService _svcAuthentication;
        public TicketVerifyController(ITicketVerifier ticketVerifier, IAuthenticationService svcAuthentication)
        {
            _ticketVerifier = ticketVerifier;
            _svcAuthentication = svcAuthentication;
            Logger = NullLogger.Instance;
        }

        public ILogger Logger;
        //[RequireHttps]
        [NoAuthorize]
        [HttpPost]
        public ActionResult Verify(SsoReceiveData data)
        {
            Logger.Information("验证ticket");
            if (string.IsNullOrEmpty(data.ticket))
            { 
                return Json(null);
            }
            else
            {//验证ticket 
                //int userId;
                //if (int.TryParse(data.accountName, out userId))
                //{
                    var user = new TicketUserInfo(data.userId);
                    var reJson = _ticketVerifier.Verify(user, data.ticket);
                    return Json(reJson);
                //}
            }
            return Json(null);
        }
    }
}
