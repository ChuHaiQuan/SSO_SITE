using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Framework.Security.Permissions;

namespace QsTech.Core
{
    public class Permissions : IPermissionProvider
    {

        public static readonly Permission RoleManager = Permission.Named("角色管理").SetDescription("角色的增删查改");
        
        public IEnumerable<Permission> GetPermissions()
        {
            yield return RoleManager;
        }
    }
}