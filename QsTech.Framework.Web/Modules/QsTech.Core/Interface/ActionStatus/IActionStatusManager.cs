using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QsTech.Framework;

namespace QsTech.Core.Interface
{
    public interface IActionStatusManager : IUnitOfWorkDependency
    {
        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <param name="type"></param>
        /// <param name="entityId">业务对象ID</param>
        /// <param name="entityCode">业务对象编码</param>
        /// <param name="action">相应业务实体的状态码</param>
        /// <param name="actionDescription">业务操作说明</param>
        void AddActionStatus(int type,string entityId,string entityCode,string action,string actionDescription);
    }
}
