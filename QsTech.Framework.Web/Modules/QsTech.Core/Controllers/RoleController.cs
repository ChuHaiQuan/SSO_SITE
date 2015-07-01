using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QsTech.Core.Menus;
using QsTech.Core.Models.Roles;
using QsTech.Framework.Localization;
using QsTech.Framework.Security.Permissions;
using Autofac.Features.Metadata;
using QsTech.Framework.Modules;
using QsTech.Core.Models.Menus;
using QsTech.Framework.Json;
using QsTech.Framework;
using QsTech.Core.Interface.Extensions;
using QsTech.Framework.Security;

namespace QsTech.Core.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IPermissionManager _permissionManager;
        private readonly IModuleManager _moduleManager;
        private readonly IMenuService _menuService;
        private readonly IRoleBelongsSystemService _belongsSystemService;
        private readonly IAuthorizer _authorizer;
        private readonly ITranslation _translation;
        private readonly IRolePermissionsService _rolePermissionsService;

        public RoleController(IRoleService roleService,
            IModuleManager moduleManager,
            IPermissionManager permissionManager,
            IMenuService menuService,
            IRoleBelongsSystemService belongsSystemService,
            IAuthorizer authorizer, ITranslation translation,
            IRolePermissionsService rolePermissionsService)
        {
            _roleService = roleService;
            _moduleManager = moduleManager;
            _permissionManager = permissionManager;
            _menuService = menuService;
            _belongsSystemService = belongsSystemService;
            _authorizer = authorizer;
            _translation = translation;
            _rolePermissionsService = rolePermissionsService;
        }

        public ActionResult List()
        {
            CheckPermission();
            var roles = _roleService.GetAllRoles().ToArray();
            Func<string, string> getBelongsName = value =>
            {
                if (string.IsNullOrEmpty(value)) return string.Empty;
                var entry = _belongsSystemService.FindByValue(value);
                if (entry != null) return entry.Name;
                return string.Empty;
            };
            var model = roles.Select(r => new ListRoleModel(r)
            {
                BelongsTo = getBelongsName(r.BelongsTo)
            });
            return View(model);
        }

        public ActionResult Create()
        {
            CheckPermission();
            var model = new CreateRoleModel();
            model.BelongsArray = _belongsSystemService.GetSystems();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateRoleModel model)
        {
            CheckPermission();
            if (ModelState.IsValid)
            {
                if (_roleService.ExistsRole(model.Name))
                {
                    ModelState.AddModelError("Name", string.Format("角色名 {0} 已经存在", model.Name));
                    return View(model);
                }
                Role newRole = new Role
                {
                    Name = model.Name,
                    Description = model.Description,
                    Category = model.Category,
                    BelongsTo = model.BelongsTo,
                    Enabled = true
                };
                _roleService.AddRole(newRole);
                return RedirectToAction("Edit", new { id = newRole.Id });
            }
            model.BelongsArray = _belongsSystemService.GetSystems();
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            CheckPermission();
            var role = _roleService.GetRole(id);

            var model = new EditRoleModel(role);
            LoadPermissions(model, role);
            model.Navigations = _menuService.GetAllMenus().Where(m => m.Parent == null).ToList();
            //var users = _userManager.GetUsers();
            //model.Owners = (from u in users
            //                join ru in role.Owners
            //                on u.Id equals ru
            //                select new RoleOwner
            //                {
            //                    Id = u.Id,
            //                    Name = u.Name
            //                }).ToList();
            model.BelongsArray = _belongsSystemService.GetSystems();
            return View(model);
        }

        void LoadPermissions(EditRoleModel model, Role role)
        {
            List<ModulePermissionModel> permissionModels = new List<ModulePermissionModel>();
            var modules = _moduleManager.GetAvailableModules();
            foreach (var module in modules)
            {
                var permissions = _permissionManager.GetPermissions(module.Id);
                if (!permissions.Any())
                {
                    continue;
                }
                permissionModels.Add(new ModulePermissionModel
                {
                    ModuleDescriptor = module,
                    Permissions = permissions.Select(p => new PermissionModel
                    {
                        PermissionName = _translation.T(p.Name),
                        PermissionDescription = p.Description,
                        Allow = role.Permissions != null && role.Permissions.Any(rp => rp == p.Name),
                        ImpliedPermissionString = p.ImpliedBy != null ? string.Join(",", p.ImpliedBy.Where(imp=>imp != null).Select(imp => imp.Name)) : string.Empty
                    })
                });
            }
            model.Original = role;
            model.Permissions = permissionModels;
        }

        [HttpPost]
        public ActionResult Edit(EditRoleModel model)
        {
            Role role;
            if (_roleService.TryGetRole(model.Name, out role))
            {
                if (role.Id != model.Id)
                {
                    //ModelState.AddModelError("Name", string.Format("角色名 {0} 已经存在", model.Name));
                    throw new QsTechException(string.Format("角色名 {0} 已经存在", model.Name));
                }
            }
            if (ModelState.IsValid)
            {
                if (role == null)
                {
                    role = _roleService.GetRole(model.Id);
                }
                role.Name = model.Name;
                role.Description = model.Description;
                role.Category = model.Category;
                role.BelongsTo = model.BelongsTo;
                ViewBag.Success = true;
            }
            else
            {
                ModelState.AddModelError("", this.GetModelErrorsMessage());
            }
            LoadPermissions(model, role);
            model.Navigations = _menuService.GetAllMenus().Where(m => m.Parent == null).ToList();
          //  var users = _userManager.GetUsers();
            //model.Owners = (from u in users
            //                join ru in role.Owners
            //                on u.Id equals ru
            //                select new RoleOwner
            //                {
            //                    Id = u.Id,
            //                    Name = u.Name
            //                }).ToList();
            model.BelongsArray = _belongsSystemService.GetSystems();
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            CheckPermission();
            _roleService.DeleteRole(id);

            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult AuthorizePermission(int roleId, string permission)
        {
            _rolePermissionsService.AuthorizePermission(roleId, permission);
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult UnauthorizePermission(int roleId, string permission)
        {
            _rolePermissionsService.UnauthorizePermission(roleId, permission);
            return new EmptyResult();
        }

    

        [HttpPost]
        public ActionResult Authorize(int roleId, int[] users)
        {
            var role = _roleService.GetRole(roleId);
            foreach (var u in users)
            {
                if (!role.Owners.Any(uid => uid == u))
                {
                    role.Owners.Add(u);
                }
            }
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Unauthorize(int roleId, int userId)
        {
            var role = _roleService.GetRole(roleId);
            if (role.Owners != null && role.Owners.Any(owner => owner == userId))
            {
                role.Owners.Remove(userId);
            }
            return new EmptyResult();
        }


        #region 权限检查
        private void CheckPermission()
        {
            if (!_authorizer.Authorize(Permissions.RoleManager))
                throw new QsTechException(string.Format("没有{0}的权限!", Permissions.RoleManager.Description));
        }
        #endregion



    }
}
