using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QsTech.Framework;

namespace QsTech.Core.Interface
{
    public interface ICoreSetting : ISingletonDependency
    {
        /// <summary>
        /// 获取用户中心的host
        /// </summary>
        string GetHostUrl { get; }
        /// <summary>
        /// 获取本应用的host
        /// </summary>
        string GetCurrentApplicationUrl { get;}

        /// <summary>
        /// 获取登出Url
        /// </summary>
        string GetLogOutUrl { get; }

        /// <summary>
        /// 获取当前应用的私钥
        /// </summary>
        string GetApplicationSecret { get; }

        string GetApplicationId { get; }

        bool IsSsoServer { get; }

        /// <summary>
        /// 标题
        /// </summary>
        string Title { get; }

        string BottomDescription { get; }
    }
}
