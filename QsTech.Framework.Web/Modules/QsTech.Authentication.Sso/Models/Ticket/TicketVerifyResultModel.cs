using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QsTech.Authentication.Sso.Models.ticket
{
    public class TicketVerifyResult
    {
        public TicketVerifyResult(bool success)
        {
            Success = success;
        }
        public bool Success { get; set; }

        public string AccountName { get; set; }
    }
}
