using System.Collections.Generic;

namespace QsTech.Core.Models.Menus
{
    public class MenuEntry
    {
        public MenuEntry(string id)
        {
            Id = id;
            Entries = new List<MenuEntry>();
        }

        public MenuEntry Parent { get; set; }

        public string Id { get; set; }

        public int Order { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public IList<MenuEntry> Entries { get; set; }

        public QsTech.Core.Interface.MenuAccessPermissionCollection AccessPermissions { get; set; }
    }
}