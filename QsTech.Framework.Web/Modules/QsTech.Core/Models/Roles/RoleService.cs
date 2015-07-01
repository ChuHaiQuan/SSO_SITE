using System;
using System.Collections.Generic;
using System.Linq;
using QsTech.Framework.Data;
using QsTech.Framework.Security;
using QsTech.Framework.Logging;
using QsTech.Framework.Security.Permissions;
using QsTech.Core.Interface;
using QsTech.Framework;
using QsTech.Framework.Caching;

namespace QsTech.Core.Models.Roles
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _rolesRepo;
        private readonly IAuthorizer _authorizer;

        public RoleService(IRepository<Role> rolesRepo,
            IAuthorizer authorizer)
        {
            _rolesRepo = rolesRepo;
            _authorizer = authorizer;
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public IEnumerable<Role> GetUserRoles(IUser user)
        {
            return (from item in _rolesRepo.Table
                   where item.Owners.Contains(user.Id)
                   select item).ToArray();
        }

        public IEnumerable<Role> GetAccountRoles(IAccount account)
        {
            return (from item in _rolesRepo.Table
                    where item.Owners.Contains(account.Id)
                    select item).ToArray();
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return _rolesRepo.Table;
        }

        public Role GetRole(int id)
        {
            return _rolesRepo.Get(id);
        }

        public void AddRole(Role role)
        {
            if (!_authorizer.Authorize(Permissions.RoleManager))
                throw new QsTechException(string.Format("没有{0}的权限!", Permissions.RoleManager.Description));

            _rolesRepo.Create(role);
        }

        public void EditRole(Role role)
        {
            if (!_authorizer.Authorize(Permissions.RoleManager))
                throw new QsTechException(string.Format("没有{0}的权限!", Permissions.RoleManager.Description));

            _rolesRepo.Update(role);
        }

        public void DeleteRole(int id)
        {
            if (!_authorizer.Authorize(Permissions.RoleManager))
                throw new QsTechException(string.Format("没有{0}的权限!", Permissions.RoleManager.Description));

            var role = _rolesRepo.Get(id);
            _rolesRepo.Delete(role);
        }

        public bool ExistsRole(string name)
        {
            var role = _rolesRepo.Get(u => u.Name == name);
            return role != null;
        }

        public bool TryGetRole(string name, out Role role)
        {
            role = null;
            try
            {
                role = _rolesRepo.Get(u => u.Name == name);
                return role != null;
            }
            catch (Exception ex)
            {
                Logger.Information(ex, string.Format("根据角色名 {0} 查找角色时发生错误。", name));
            }
            return false;
        }
    }
}