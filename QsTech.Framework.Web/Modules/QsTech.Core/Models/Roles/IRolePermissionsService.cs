using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Framework.Security;
using QsTech.Framework;
using QsTech.Framework.Security.Permissions;
using QsTech.Framework.Events;
using QsTech.Core.Interface.User;

namespace QsTech.Core.Models.Roles
{
    public interface IRolePermissionsService : QsTech.Framework.IUnitOfWorkDependency
    {
        void AuthorizePermission(int roleId, string permission);

        void UnauthorizePermission(int roleId, string permission);
    }

    public class RolePermissionsService : IRolePermissionsService
    {
        private readonly IRoleService _roleService;
        private readonly IAuthorizer _authorizer;
        private readonly IPermissionManager _permissionManager;
        private readonly IEventManager _eventManager;
        private readonly IEnumerable<Permission> _allPermissionsCache;

        public RolePermissionsService(IRoleService roleService,
            IAuthorizer authorizer,
            IPermissionManager permissionManager,
            IEventManager eventManager)
        {
            _roleService = roleService;
            _authorizer = authorizer;
            _permissionManager = permissionManager;
            _eventManager = eventManager;
            _allPermissionsCache = _permissionManager.GetAllPermissions();
        }

        public void AuthorizePermission(int roleId, string permission)
        {
            if (string.IsNullOrEmpty(permission))
            {
                throw new ArgumentException("Invalid parameter", "permission");
            }
            if (!_authorizer.Authorize(Permissions.RoleManager))
            {
                throw new QsTechException(string.Format("没有{0}的权限!", Permissions.RoleManager.Description));
            }
            if (!_permissionManager.GetAllPermissions().Any(per => per.Id == permission))
            {
                throw new ArgumentException("Invalid parameter", "permission");
            }

            var role = _roleService.GetRole(roleId);
            if (role != null)
            {
                Action<Role, string> addInvoker = (r, k) =>
                {
                    if (!r.Permissions.Any(p => p == k))
                    {
                        r.Permissions.Add(k);
                    }
                };
                addInvoker.Invoke(role, permission);

                // 查找所有递归隐式包含的权限并添加到当前指定的角色
                var allRecursionImpliedPermissions = GetAllImpliedPermissions(permission);
                foreach (var p in allRecursionImpliedPermissions)
                {
                    addInvoker.Invoke(role, p.Id);
                }
            }
            ResetUserPermissionsCache(role);      // 角色权限发生变化，重置用户权限缓存。
        }

        public void UnauthorizePermission(int roleId, string permission)
        {
            if (string.IsNullOrEmpty(permission))
            {
                throw new ArgumentException("Invalid parameter", "permission");
            }
            if (!_authorizer.Authorize(Permissions.RoleManager))
            {
                throw new QsTechException(string.Format("没有{0}的权限!", Permissions.RoleManager.Description));
            }
            if (!_permissionManager.GetAllPermissions().Any(per => per.Id == permission))
            {
                throw new ArgumentException("Invalid parameter", "permission");
            }

            if (!_authorizer.Authorize(Permissions.RoleManager))
                throw new QsTechException(string.Format("没有{0}的权限!", Permissions.RoleManager.Description));

            var role = _roleService.GetRole(roleId);
            if (role != null)
            {
                if (role.Permissions.Any(o => o == permission))
                {
                    role.Permissions.Remove(permission);
                }
            }
            ResetUserPermissionsCache(role);      // 角色权限发生变化，重置用户权限缓存。
        }

        IEnumerable<Permission> GetAllImpliedPermissions(string permission)
        {
            Func<string, IEnumerable<Permission>> finder = findKey =>
            {
                var anyImpliedBy = _allPermissionsCache.Where(per => per.ImpliedBy.Count > 0).ToArray();
                return anyImpliedBy.Where(aip => aip.ImpliedBy.Any(p => p.Id == findKey));
            };
            var impliedPerissions = finder.Invoke(permission);

            IList<Permission> lstPermissions = impliedPerissions.ToList();
            foreach (var p in impliedPerissions)
            {
                FindAndAppendImpliedPermissions(lstPermissions, finder, p.Id);
            }
            return lstPermissions;
        }

        void FindAndAppendImpliedPermissions(IList<Permission> impliedPermissions, Func<string, IEnumerable<Permission>> finder, string permissionId)
        {
            var permissions = finder.Invoke(permissionId);

            foreach (var p in permissions)
            {
                FindAndAppendImpliedPermissions(impliedPermissions, finder, p.Id);
                impliedPermissions.Add(p);
            }
        }

        void ResetUserPermissionsCache(Role role)
        {
            _eventManager.Trigger<IRolePermissionsChangedEventHandler>(caller =>
            {
                caller.ResetUserPermissionsCache(role.Owners.ToArray());
            });
        }
    }

}