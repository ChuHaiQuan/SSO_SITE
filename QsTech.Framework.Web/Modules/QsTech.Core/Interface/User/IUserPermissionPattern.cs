using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QsTech.Framework;
using QsTech.Framework.Security;

namespace QsTech.Core.Interface.User
{
    public interface IUserPermissionPattern : IUnitOfWorkDependency
    {
        string Type { get; }

        IEnumerable<string> GetUserPermissions(IUser user);
    }
}
