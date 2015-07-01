using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QsTech.Framework;

namespace QsTech.Core.Interface.Clients
{
    public interface IAccountClientManager : IUnitOfWorkDependency
    {
        /// <summary>
        /// 检查权限
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        bool CheckClientAccess(int accountId,int clientId);
    }
}
