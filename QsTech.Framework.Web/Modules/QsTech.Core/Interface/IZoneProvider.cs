using System;
using QsTech.Framework;

namespace QsTech.Core.Interface
{
    public interface IZoneProvider : ISingletonDependency
    {
        /// <summary>
        /// ��������id, ���������ڻ�ȡZone����ҳ��Ƭ�ε�Controller/Action
        /// </summary>
        /// <param name="zoneId"></param>
        /// <returns>����Controller��Action��Ϣ, ��һ��ΪController����, �ڶ���ΪActoin����. </returns>
        Tuple<string, string> GetZone(string zoneId);
    }
}