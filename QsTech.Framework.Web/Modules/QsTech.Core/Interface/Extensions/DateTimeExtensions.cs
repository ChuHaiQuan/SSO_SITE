using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QsTech.Core.Interface.Extensions
{
    public static class DateTimeExtensions
    {
        private static string ToQSDateString(this DateTime dt, string formatter)
        {
            if (dt == null)
                return "";
            else
                return ((DateTime)dt).ToString(formatter);
        }

        #region 日期年月日
        public static string ToYMDString(this DateTime dt)
        {
            return dt.ToQSDateString("yyyy-MM-dd");
        }

        public static string ToYMDString(this DateTime? dt)
        {
            if (dt == null)
                return "";
            else
                return ((DateTime)dt).ToYMDString();
        }
        #endregion


        #region 精确到分钟
        public static string ToYMDHString(this DateTime dt)
        {
            return dt.ToQSDateString("yyyy-MM-dd HH:mm");
        }


        public static string ToYMDHString(this DateTime? dt)
        {
            if (dt == null)
                return "";
            else
                return ((DateTime)dt).ToYMDHString();
        }

        public static string ToDateFormatString(this DateTime dt, string format)
        {
            return dt.ToQSDateString(format);
        }

        public static string ToDateFormatString(this DateTime? dt,string format)
        {
            if (dt == null)
                return "";
            else
                return ((DateTime)dt).ToQSDateString(format);
        }
        #endregion

        #region 日期年月日
        public static string ToMDYString(this DateTime dt)
        {
            return dt.ToQSDateString("MM-dd-yyyy");
        }

        public static string ToMDYString(this DateTime? dt)
        {
            if (dt == null)
                return "";
            else
                return ((DateTime)dt).ToMDYString();
        }
        #endregion



    }
}
