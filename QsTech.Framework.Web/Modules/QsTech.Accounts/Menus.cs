using QsTech.Core.Interface;

namespace QsTech.Accounts
{
    public class Menus : IMenuProvider
    {
        public void BuildMenus(IMenuBuilder builder)
        {
            builder.Menu("Management", "account_list").SetName("账户管理").SetUrl("/Accounts/Account/List").AddAccessPermission(Permissions.CreateAccount);
            builder.Menu("Management", "account_Password").SetName("密码安全").SetUrl("/Accounts/Account/Security");
           // builder.Menu("Management", "account_list").SetName("账户申请管理").SetUrl("/Accounts/RegisterAccount/List")
             //   .AddAccessPermission(Permissions.RegisterAccountManager);
        }
    }
}