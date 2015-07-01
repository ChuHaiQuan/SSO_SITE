using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QsTech.Framework;
using QsTech.Framework.Security;

namespace QsTech.Core.Interface.Authentication
{
    public interface IVerifiedFilter:IUnitOfWorkDependency
    {
        void OnVerified(IUser user);
    }
}
