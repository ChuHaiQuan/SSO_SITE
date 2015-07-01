using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Core.Interface;
using QsTech.Framework.Security;

namespace QsTech.Core.Models.Roles
{
    public class RoleManager : IRoleManager
    {
        private readonly IRoleService _roleService;

        public RoleManager(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public IEnumerable<IRole> GetAllRoles()
        {
            return _roleService.GetAllRoles();
        }

        public IEnumerable<IRole> FindByCategory(string category)
        {
            return _roleService.GetAllRoles().Where(r => r.Category == category);
        }

        public IRole GetRole(int id)
        {
            return _roleService.GetRole(id);
        }

        public IEnumerable<IRole> GetUserRoles(IUser user)
        {
            return _roleService.GetUserRoles(user);
        }

        public IEnumerable<IRole> GetAccountRoles(IAccount account)
        {
            return _roleService.GetAccountRoles(account);
        }

        public IEnumerable<IRole> FindByBelongs(string belongs)
        {
            return _roleService.GetAllRoles().Where(r => r.BelongsTo == belongs);
        }


        public IEnumerable<IRole> FindRoles(string category, string belongs)
        {
            return _roleService.GetAllRoles().Where(r => r.BelongsTo == belongs && r.Category == category);
        }
    }
}