using System.Collections.Generic;
using QsTech.Core.Models.Menus;
using QsTech.Framework;
using QsTech.Framework.Security;

namespace QsTech.Core.Menus
{
    public interface IMenuService : IUnitOfWorkDependency
    {
        IEnumerable<MenuEntry> GetMenus(IUser user);

        IEnumerable<MenuEntry> GetAllMenus();

        MenuEntry GetMenuById(string id, IUser user);

        /// <summary>
        /// ��ȡָ��Ƶ�����Ӳ˵�
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<MenuEntry> GetAllSubMenus(string id);
        /// <summary>
        /// ��ȡָ��Ƶ�����Ӳ˵�
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<MenuEntry> GetSubMenus(string id, IUser user);
    }
}