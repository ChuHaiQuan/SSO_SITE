using System.Collections.Generic;
using System.Linq;
using QsTech.Core.Interface;
using QsTech.Core.Menus;

namespace QsTech.Core.Models.Menus
{
    public class MenuHolder : IMenuHolder
    {
        private readonly IEnumerable<IMenuProvider> _providers;
        private IEnumerable<MenuEntry> _allMenus; 

        public MenuHolder(IEnumerable<IMenuProvider> providers)
        {
            _providers = providers;
        }

        public IEnumerable<MenuEntry> GetRootEntries()
        {
            return GetAllEntries().Where(e => e.Parent == null).OrderBy(e => e.Order);
        }

        public IEnumerable<MenuEntry> GetAllEntries()
        {
            if (_allMenus == null)
            {
                _allMenus = new List<MenuEntry>();
                var builder = new MenuBuilder();
                foreach (var menuProvider in _providers)
                {
                    menuProvider.BuildMenus(builder);
                }

                var allGroups = builder.Groups.Distinct().Select(g =>
                                                      new MenuEntry(g.Id)
                                                          {
                                                              Order = g.Order,
                                                              Description = g.Description, 
                                                              Name = g.Name,
                                                              Url = g.Url
                                                          }).OrderBy(g=>g.Order).ToArray();

                var allMenus = builder.Menus.Select(m =>
                                                    new MenuEntry(m.Id)
                                                        {
                                                            Order = m.Order,
                                                            Name = m.Name,
                                                            Description = m.Description,
                                                            Url = m.ComingSoon?"/ComingSoon":m.Url,
                                                            Parent = allGroups.SingleOrDefault(g => g.Id == m.GroupId),
                                                            AccessPermissions = m.AccessPermissions
                                                        });
                foreach (var menuEntry in allGroups)
                {
                    menuEntry.Entries = allMenus.Where(m => m.Parent == menuEntry).OrderBy(m=>m.Order).ToArray();
                }
                _allMenus = allMenus.Union(allGroups);
            }
            return _allMenus;
        }
    }
}