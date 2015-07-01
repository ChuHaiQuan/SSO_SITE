using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QsTech.Authentication.Sso.Clients
{
    public class ClientUserAddParameter
    {
        public int clientId { get; set; }

        public int[] userIds { get; set; }

        public string[] userNames { get; set; }  
    }
}