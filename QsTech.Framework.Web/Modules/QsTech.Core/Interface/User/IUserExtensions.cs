using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Framework.Security;
using QsTech.Framework;

namespace QsTech.Core.Interface.User
{

    public static class IUserExtensions
    {
        public static string GetAccountType(this　IUser user)
        {
            if (user.Id == 10)
                return "administrator";
            else
                return string.Empty;
        }
    }
}