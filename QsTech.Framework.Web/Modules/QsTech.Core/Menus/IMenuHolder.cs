using System.Collections.Generic;
using QsTech.Core.Models.Menus;
using QsTech.Framework;

namespace QsTech.Core.Menus
{
    public interface IMenuHolder : ISingletonDependency
    {
        IEnumerable<MenuEntry> GetRootEntries();
        IEnumerable<MenuEntry> GetAllEntries();
    }
}