using System;
using QsTech.Core.Interface;

namespace QsTech.Core.Models.Menus
{
    public class NavZone : IZoneProvider
    {
        
        public Tuple<string, string> GetZone(string zoneId)
        {
            if (zoneId == "Menu")
            {
                return new Tuple<string, string>("Menu", "MenuZone");
            }
            else if (zoneId == "PortalManagerMenu")
            {
                return new Tuple<string, string>("Menu", "PortalManagerMenuZone");
            }
            return null;
        }
    }
}