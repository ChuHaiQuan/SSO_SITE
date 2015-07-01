using System.Collections.Generic;
using QsTech.Core.Interface;

namespace QsTech.Core.Models.Menus
{
    public class MenuBuilder : IMenuBuilder
    {
        public MenuBuilder()
        {
            _menus = new List<Menu>();
            _groups = new List<GroupMenu>();
        }

        private IList<Menu> _menus;
        public IEnumerable<Menu> Menus
        {
            get { return _menus; }
        }

        private IList<GroupMenu> _groups;
        public IEnumerable<GroupMenu> Groups
        {
            get { return _groups; }
        }

        public GroupMenu Group(string id)
        {
            var g = new GroupMenu() {Id = id};
            _groups.Add(g);
            return g;
        }

        public Menu Menu(string groupId, string id)
        {
            var m = new Menu() {GroupId = groupId, Id = id};
            _menus.Add(m);
            return m;
        }
    }
}