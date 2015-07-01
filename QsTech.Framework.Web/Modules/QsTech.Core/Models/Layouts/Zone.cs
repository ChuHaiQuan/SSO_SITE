using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QsTech.Layout.Models
{
    public class Zone
    {
        public Zone(string zoneId, string moduleId, string controller, string action)
        {
            ZoneId = zoneId;
            ModuleId = moduleId;
            Controller = controller;
            Action = action;
        }

        public string ZoneId { get; set; }

        public string ModuleId { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }
    }
}