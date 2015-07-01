using System.Collections.Generic;
using QsTech.Core.Models.Menus;

namespace QsTech.Core.Views.Home
{
    public class HomeViewModel
    {
        public HomeViewModel(IEnumerable<MenuEntry> menus, IEnumerable<HomeZoneViewModel> zones)
        {
            Menus = menus;
            Zones = zones;
        }

        public IEnumerable<MenuEntry> Menus { get; set; }

        public IEnumerable<HomeZoneViewModel> Zones { get; set; }
    }
}