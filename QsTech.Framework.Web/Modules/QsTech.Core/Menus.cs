using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Core.Interface;

namespace QsTech.Core
{
    public class CoreMenus : IMenuProvider
    {
        public const string GroupManagementHome = "Home";
        public void BuildMenus(IMenuBuilder builder)
        {
            builder.Group("Home").SetName("首页").SetOrder(0).SetUrl("/Core/Home/ManagerIndex");

            builder.Group("Management").SetName("设置").SetOrder(4).SetUrl("/Core/Home/Index");
            builder.Menu("Management", "Role").SetName("角色").SetUrl("/Core/Role/List").AddAccessPermission(Permissions.RoleManager);
        }
    }
}