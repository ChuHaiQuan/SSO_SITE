using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QsTech.Framework.Security;
using QsTech.Framework.Pagination;
using QsTech.Core.Interface.Entities;

namespace QsTech.Core.Interface.User
{
    public interface IAccountPermissionsManager : QsTech.Framework.IUnitOfWorkDependency
    {
        IEnumerable<string> GetUserPermissions(IUser user);

        void AuthorizeRole(int accountId, int roleId);

        void UnAuthorizeRole(int accountId, int roleId);
    }
}
