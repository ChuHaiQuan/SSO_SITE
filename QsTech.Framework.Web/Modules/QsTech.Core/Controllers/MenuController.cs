using System.Collections.Generic;
using System.Web.Mvc;
using QsTech.Core.Menus;
using QsTech.Core.Models.Menus;
using QsTech.Framework.Security;
using QsTech.Core.Interface.Extensions;
namespace QsTech.Core.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public ActionResult MenuZone()
        {
            IEnumerable<MenuEntry> menus = _menuService.GetMenus(this.GetUser());
            return View(menus);
        }

        public ActionResult PortalManagerMenuZone()
        {
            IEnumerable<MenuEntry> menus = _menuService.GetMenus(this.GetUser());
            return View(menus);
        }
    }
}
