using System;
using System.Linq;
using QsTech.Framework.Security;
using QsTech.Framework.Security.Permissions;
using QsTech.Core.Interface;
using QsTech.Core.Interface.User;

namespace QsTech.Core.Models.Roles
{
    public class RoleAuthorization : IAuthorizationService
    {
        private readonly IAccountPermissionsManager _userPermissionsManager;

        public RoleAuthorization(IAccountPermissionsManager userPermissionsManager)
        {
            _userPermissionsManager = userPermissionsManager;
        }

        public void CheckAccess(Permission permission, IUser user)
        {
            if (!TryCheckAccess(permission, user))
            {
                throw new SecurityException("") //TODO: �쳣����
                {
                    PermissionName = permission.Name,
                    User = user
                };
            }
        }

        public bool TryCheckAccess(Permission permission, IUser user)
        {
            //TODO: ���Ӽ̳е�Ȩ�޲��
            // return _roleService.GetUserRoles(user).Any(r => r.Permissions.Any(p => p == permission.Name));
            return _userPermissionsManager.GetUserPermissions(user).Any(p => p == permission.Name);
        }
    }
}