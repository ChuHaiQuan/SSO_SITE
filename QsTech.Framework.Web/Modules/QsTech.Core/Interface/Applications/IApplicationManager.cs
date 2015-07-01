using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QsTech.Core.Interface;
using QsTech.Framework;

namespace QsTech.Core.Interface
{
    public interface IApplicationManager :ITransientDependency
    {
        /// <summary>
        /// 根据应用枚举获取子应用的域名
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        string GetApplicationHost(ApplicationEnum type);

    }
}
