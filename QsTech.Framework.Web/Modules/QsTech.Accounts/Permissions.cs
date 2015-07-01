using System.Collections.Generic;
using QsTech.Framework.Security.Permissions;

namespace QsTech.Accounts
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission CreateAccount = Permission.Named("开设账户").SetDescription("开设账户");

        public static readonly Permission ViewAccount = Permission.Named("查看账户").SetDescription("查看账户");

        //public static readonly Permission RegisterAccountManager = Permission.Named("账户申请管理").SetDescription("管理申请的账户，对其进行审核");

        public IEnumerable<Permission> GetPermissions()
        {
            yield return CreateAccount;
            yield return ViewAccount;
          //  yield return RegisterAccountManager;
        }
    }
}