using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Framework.Security.Permissions;
using QsTech.Core.Interface;
namespace QsTech.Authentication.Sso
{
    public class Menus : IMenuProvider
    {
        private readonly string _hostUrl;
        public Menus()//(ICoreSetting coreSetting)
        {
            //_hostUrl = coreSetting.GetHostUrl;
        }
        public void BuildMenus(IMenuBuilder builder)
        {
            //builder.Group("Management").SetName("应用管理");
            builder.Menu("Management", "Client").SetName("应用列表").SetUrl("/Authentication/Client/List")
                .AddAccessPermission(Permissions.ManagerClient);
        }
    }

}