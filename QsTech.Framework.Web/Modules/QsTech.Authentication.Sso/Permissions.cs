using System.Collections.Generic;
using QsTech.Framework.Security.Permissions;

namespace QsTech.Authentication.Sso
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ManagerClient = Permission.Named("SSO登录系统列表").SetDescription("查看SSO支持的系统");
        public IEnumerable<Permission> GetPermissions()
        {
            yield return ManagerClient;
        }
    }
}