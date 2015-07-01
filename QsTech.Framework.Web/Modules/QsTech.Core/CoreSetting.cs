using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Core.Interface;
using QsTech.Framework.Environment;

namespace QsTech.Core
{
    public class CoreSetting : Setting,ICoreSetting
    {
        public string GetHostUrl
        {
            get { return GetValue("SsoHost"); }
        }

        public string GetCurrentApplicationUrl
        {
            get { return GetValue("currentApplication"); }
        }

        public string GetLogOutUrl
        {
            get {
                //if (GetHostUrl == GetCurrentApplicationUrl)
                //    return string.Format("{0}/Authentication/Account/LogOut", GetCurrentApplicationUrl);
                //else
                //    return string.Format("{0}/Authentication/Sso/LogOut", GetCurrentApplicationUrl);
                return string.Format("/Authentication/Account/LogOut");
            }
        }

        public string GetApplicationSecret
        {
            get { return GetValue("ApplicationSecret"); }
        }
        public string GetApplicationId
        {
            get { return GetValue("ApplicationId"); }
        }
        public bool IsSsoServer
        {
            get { return GetHostUrl == GetCurrentApplicationUrl; }

        }

        public string Title { get { return GetValue("Title"); } }

        public string BottomDescription { get { return GetValue("BottomDescription"); } }

    }
}