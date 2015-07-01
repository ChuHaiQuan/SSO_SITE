using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QsTech.Framework;
using QsTech.Framework.Security;

namespace QsTech.Core.Interface
{
    public interface IRoleManager : ITransientDependency
    {
        IEnumerable<IRole> GetAllRoles();

        IEnumerable<IRole> FindByCategory(string category);

        IRole GetRole(int id);

       IEnumerable<IRole> GetUserRoles(IUser user);

       IEnumerable<IRole> GetAccountRoles(IAccount account);

       IEnumerable<IRole> FindByBelongs(string belongs);

       IEnumerable<IRole> FindRoles(string category, string belongs);
    }
}
