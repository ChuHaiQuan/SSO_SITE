using System.Collections.Generic;
using System.Linq;
using QsTech.Core.Models.Menus;
using QsTech.Framework.Localization;
using QsTech.Framework.Logging;
using QsTech.Framework.Security;
using System.Linq.Expressions;
using System;
using QsTech.Core.Interface;
using QsTech.Core.Interface.User;

namespace QsTech.Core.Menus
{
    public class MenuService : IMenuService
    {
        private readonly IMenuHolder _holder;
        private readonly IAccountPermissionsManager _userPermissionsManager;
        private readonly ITranslation _translation;

        public MenuService(IMenuHolder holder,
            IAccountPermissionsManager userPermissionsManager, ITranslation translation)
        {
            _holder = holder;
            _userPermissionsManager = userPermissionsManager;
            _translation = translation;
        }

        public ILogger Logger { get; set; }

        public IEnumerable<MenuEntry> GetMenus(IUser user)
        {
            Logger.Information(string.Format("MenuService.GetMenus(IUser):IEnumerable<MenuEntry> : user:{0}",
                                             ((user == null) ? "null" : user.Id.ToString())));
            //var userMenus = _roleService.GetUserRoles(user).SelectMany(r => r.Navigations).Distinct().ToArray();
            // var userPermissions = _roleService.GetUserRoles(user).SelectMany(r => r.Permissions);
            var userPermissions = _userPermissionsManager.GetUserPermissions(user);

            var datas = GetAllMenus().ToArray();

            var linq= (from m in datas
                    where m.Parent == null
                    select new MenuEntry(m.Id)
                    {
                        Id = m.Id,
                        Name = _translation.T(m.Name),
                        Url = m.Url,
                        Order = m.Order,
                        Description = _translation.T(m.Name + "_Desc"),
                        Entries = (from child in m.Entries
                                   from p in child.AccessPermissions.DefaultIfEmpty()
                                   from up in userPermissions
                                   where p!=null &&p.Name == up
                                   select child).Concat(
                                   from child in m.Entries
                                   where !child.AccessPermissions.Any()
                                   select child).Distinct().ToList()
                    }).OrderBy(m => m.Order).Where(m=>m.Entries.Any()).ToList();
            // 像全局首页是没有子菜单的，所以放宽条件。
            //edit by andy 7-31 没有子菜单的不显示，

            return linq;
        }

        public MenuEntry GetMenuById(string id, IUser user)
        {
            //var userMenus = _roleService.GetUserRoles(user).SelectMany(r => r.Navigations).Distinct().ToArray();
            //var userPermissions = _roleService.GetUserRoles(user).SelectMany(r => r.Permissions);
            var userPermissions = _userPermissionsManager.GetUserPermissions(user);
            return (from m in GetAllMenus()
                    where m.Id == id
                    select new MenuEntry(m.Id)
                    {
                        Id = m.Id,
                        Name = _translation.T(m.Name),
                        Url = m.Url,
                        Order = m.Order,
                        Description = _translation.T(m.Name + "_Desc"),
                        Entries = (from child in m.Entries
                                   from p in child.AccessPermissions.DefaultIfEmpty()
                                   from up in userPermissions
                                   where !child.AccessPermissions.Any() || p.Name == up
                                   select child).Distinct().ToList()
                    }).OrderBy(m => m.Order).Where(m => m.Entries.Any()).SingleOrDefault();
        }

        public IEnumerable<MenuEntry> GetAllMenus()
        {
            return _holder.GetAllEntries();
        }

        public IEnumerable<MenuEntry> GetAllSubMenus(string id)
        {
            var singleOrDefault = _holder.GetAllEntries().SingleOrDefault(e => e.Id == id);
            if (singleOrDefault != null)
                return singleOrDefault.Entries;
            return null;
        }

        public IEnumerable<MenuEntry> GetSubMenus(string id, IUser user)
        {
            //var userMenus = _roleService.GetUserRoles(user).SelectMany(r => r.Navigations).Distinct().ToArray();
            //var userPermissions = _roleService.GetUserRoles(user).SelectMany(r => r.Permissions);
            var userPermissions = _userPermissionsManager.GetUserPermissions(user);
            return (from m in GetAllMenus()
                    from p in m.AccessPermissions.DefaultIfEmpty()
                    from up in userPermissions
                    where m.Parent != null && m.Parent.Id == id && (!m.AccessPermissions.Any() || p.Name == up)
                    select new MenuEntry(m.Id)
                    {
                        Id = m.Id,
                        Name = _translation.T(m.Name),
                        Url = m.Url,
                        Order = m.Order,
                        Description = _translation.T(m.Name + "_Desc")
                    }).Distinct().OrderBy(m => m.Order);
        }
    }
}