using System;
using QsTech.Framework;

namespace QsTech.Core.Interface
{
    public interface IZoneProvider : ISingletonDependency
    {
        /// <summary>
        /// 根据区域id, 返回能用于获取Zone区域页面片段的Controller/Action
        /// </summary>
        /// <param name="zoneId"></param>
        /// <returns>包含Controller的Action信息, 第一个为Controller名称, 第二个为Actoin名称. </returns>
        Tuple<string, string> GetZone(string zoneId);
    }
}