using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QsTech.Core.Interface.Entities
{
    public interface IPortService : QsTech.Framework.IUnitOfWorkDependency
    {
        IEnumerable<DicPort> GetAll();

        IEnumerable<DicPort> GetLocal();

        DicPort GetPort(int id);

        DicPort FindByName(string name);

        DicPort FindByKey(string key);
    }
}
