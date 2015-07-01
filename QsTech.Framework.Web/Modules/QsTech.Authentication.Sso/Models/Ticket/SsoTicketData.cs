using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Authentication.Sso.Models.ticket;

namespace QsTech.Authentication.Sso.Models
{
    public class TicketBase
    {
        public string Ticket { get; set; }

        public string AccountName { get; set; }
    }

    public class SsoTicketData : TicketBase
    {
        /// <summary>
        /// 颁发时间
        /// </summary>
        public DateTime IssueTime { get; set; }


        /// <summary>
        /// 有效期timespan
        /// </summary>
        public TimeSpan Duration { get; set; }
    }
}