using System;
using QsTech.Core.Interface;

namespace QsTech.Accounts.Models.Publicties
{
    public class LogOnZone: IZoneProvider
    {
        public Tuple<string, string> GetZone(string zoneId)
        {
            if (zoneId == "LogOnDisplay")
            {
                return new Tuple<string, string>("Account", "LogOnDisplayZone");
            }
            return null;
        }
    }
}