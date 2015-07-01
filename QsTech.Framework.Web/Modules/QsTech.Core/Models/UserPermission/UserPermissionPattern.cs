using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QsTech.Framework;
using QsTech.Framework.Security;
using QsTech.Framework.Data;
using QsTech.Core.Interface;
using QsTech.Framework.Security.Permissions;
using QsTech.Core.Interface.User;
using QsTech.Core.Models.Roles;

namespace QsTech.Core.Models
{

    public class DefaultUserPermissionPattern : IUserPermissionPattern
    {
        private readonly IRepository<Role> _rolesRepo;
        public DefaultUserPermissionPattern(IRepository<Role> rolesRepo)
        {
            _rolesRepo = rolesRepo;
        }
        public string Type
        {
            get { return string.Empty; }
        }

        public IEnumerable<string> GetUserPermissions(IUser user)
        {
            var userId = user.Id;
            var data = from item in _rolesRepo.Table
                       where item.Owners.Contains(userId)
                       select item;
            return data.SelectMany(m => m.Permissions).ToArray();
        }
    }

    /// <summary>
    /// 系统管理员帐户
    /// </summary>
    public class SystemAdminPermissionPattern : IUserPermissionPattern
    {
        private readonly IPermissionManager _permissionManager;
        public SystemAdminPermissionPattern(IPermissionManager permissionManager)
        {
            _permissionManager = permissionManager;
        }

        public string Type
        {
            get { return "administrator"; }
        }

        public IEnumerable<string> GetUserPermissions(IUser user)
        {
            return _permissionManager.GetAllPermissions().Select(m => m.Name).ToArray();
        }
    }
}
