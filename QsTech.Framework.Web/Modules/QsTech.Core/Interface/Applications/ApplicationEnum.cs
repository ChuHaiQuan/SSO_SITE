using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QsTech.Core.Interface
{
    public enum ApplicationEnum
    {
        供应商管理平台=2,
        客户管理平台=3,
    }


    public static class ClientAppEnumHelper
    {
        public static string IntToString(this ApplicationEnum type)
        {
            switch (type)
            {
                case ApplicationEnum.供应商管理平台:
                    return "供应商管理平台";
                default:
                    return "";
            }
        }
    }
}
