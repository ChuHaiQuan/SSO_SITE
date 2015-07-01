using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Core.Interface;

namespace QsTech.Authentication.Sso.Clients
{
    public class ClientZoneProvider : IZoneProvider
    {
        public Tuple<string, string> GetZone(string zoneId)
        {
            if (zoneId == "Client")
            {
                return new Tuple<string, string>("Client", "ClientZone");
            }
            return null;
        }
    }
}