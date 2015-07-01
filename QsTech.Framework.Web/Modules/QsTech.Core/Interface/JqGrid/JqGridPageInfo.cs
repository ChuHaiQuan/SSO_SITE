using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QsTech.Core.Interface.JqGrid
{
    public class JqGridPageInfo
    {
        public JqGridPageInfo()
        {
            page = 1;
            rows = 5;
        }
        public string _search { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int rows { get; set; }

        public string sidx { get; set; }

        public string sord { get; set; }
    }

}
