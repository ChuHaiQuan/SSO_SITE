using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QsTech.Core.Home;
using QsTech.Core.Interface.Extensions;
using QsTech.Core.Menus;
using QsTech.Core.Models.Menus;
using QsTech.Core.Views.Home;
using QsTech.Framework;
using QsTech.Framework.Localization;
using QsTech.Framework.Security;
using QsTech.Core.Interface;
using System.Collections.Generic;

namespace QsTech.Core.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IUserManager _userManager;
        private readonly IHomeZoneService _homeZoneService;
        private readonly IMenuService _menuService;
        private readonly ITranslation _translation;
        private readonly int ZoneColumns = 1;

        public HomeController(IHomeZoneService homeZoneService, IMenuService menuService, ITranslation translation)
        {
           //_userManager = userManager;
            _homeZoneService = homeZoneService;
            _menuService = menuService;
            _translation = translation;
        }

        public ActionResult Index()
        {
            // 首页频道，去掉 "首页" 的菜单组。
            var menus = _menuService.GetMenus(this.GetUser()).Where(entry => entry.Id != "Home");
            var zones = _homeZoneService.GetArrangedZones("Home", ZoneColumns);
            var vm = new HomeViewModel(menus, zones.Select(z => new HomeZoneViewModel(z, _translation.T(z.Metadata.Name))));
            return View(vm);
        }

        public  ActionResult ManagerIndex()
        {
            return View();
        }

        public ActionResult Channel(string id)
        {
            if (id == "Home")
            {
                return RedirectToAction("Index");
            }
            var menus = _menuService.GetMenuById(id,this.GetUser());   //_menuService.GetSubMenus(id, this.GetUser());
            var zones = _homeZoneService.GetArrangedZones(id, ZoneColumns);
            var vm = new HomeViewModel(new List<MenuEntry>() { menus }, zones.Select(z => new HomeZoneViewModel(z, _translation.T(z.Metadata.Name))));
            return View(vm);
        }

        [NoAuthorize(true)]
        public ActionResult Error()
        {
            return View();
        }


        [NoAuthorize]
        public ActionResult NoAuthorizeError(string msg)
        {
            var qsExcepetion = new QsTechException(msg);
            return View(qsExcepetion);
        }
    }
}
