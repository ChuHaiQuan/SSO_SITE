using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QsTech.Core.Interface.User
{
    public interface IRolePermissionsChangedEventHandler : QsTech.Framework.Events.IEventHandler
    {
        /// <summary>
        /// 清除指定用户的权限缓存
        /// </summary>
        /// <param name="user"></param>
        void ResetUserPermissionsCache(QsTech.Framework.Security.IUser user);

        /// <summary>
        /// 清楚指定角色下的账户权限缓存
        /// </summary>
        /// <param name="cacheKeys"></param>
        void ResetUserPermissionsCache(params int[] cacheKeys);
    }
}
