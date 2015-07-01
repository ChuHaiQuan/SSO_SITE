using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using QsTech.Framework.Security.Permissions;
using QsTech.Framework.Modules;
using QsTech.Core.Interface;
using QsTech.Core.Models.Menus;
using QsTech.Framework.Security;

namespace QsTech.Core.Models.Roles
{
    public class EditRoleModel
    {
        public EditRoleModel() { }

        public EditRoleModel(Role role)
        {
            Original = role;
            Id = role.Id;
            Name = role.Name;
            Description = role.Description;
            Category = role.Category;
            BelongsTo = role.BelongsTo;
        }

        public Role Original { get; set; }

        public int Id { get; set; }

        [Required(ErrorMessage = "角色名不能为空")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string BelongsTo { get; set; }

        public List<ModulePermissionModel> Permissions { get; set; }

        public IList<MenuEntry> Navigations { get; set; }

        public List<RoleOwner> Owners { get; set; }

        public IEnumerable<RoleBelongsSystemEntry> BelongsArray { get; set; }
    }

    public class RoleOwner //: IUser  去除IUser继承
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }

    public class ModulePermissionModel
    {
        public ModuleDescriptor ModuleDescriptor { get; set; }

        public IEnumerable<PermissionModel> Permissions { get; set; }
    }

    public class PermissionModel
    {
        public string PermissionName { get; set; }

        public string PermissionDescription { get; set; }

        public string ImpliedPermissionString { get; set; }

        public bool Allow { get; set; }
    }
}