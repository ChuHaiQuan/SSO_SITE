using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QsTech.Framework;
using QsTech.Framework.Security;
using QsTech.Core.Interface;

namespace QsTech.Core.Models.Roles
{
    public interface IRoleService : IUnitOfWorkDependency
    {
        IEnumerable<Role> GetUserRoles(IUser user);

        IEnumerable<Role> GetAccountRoles(IAccount account);
        

        IEnumerable<Role> GetAllRoles();

        Role GetRole(int id);

        void AddRole(Role role);

        void EditRole(Role role);

        void DeleteRole(int id);

        bool ExistsRole(string name);

        bool TryGetRole(string name, out Role role);
    }
}
