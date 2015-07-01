using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace QsTech.Core.Interface.JqGrid
{
    public interface IJqGridList<T>
    {
        /// <summary>
        /// 当前页
        /// </summary>
        int page { get; set; }

        int total { get; set; }

        int records { get; set; }

        IEnumerable<T> rows { get; set; }
    }


    public class JqGridList<T>
    {
        public JqGridList()
        {
            this.page = 1;
            this.showCount = 5;
            this.userdata = new Hashtable(3);
        }

        public JqGridList(JqGridPageInfo jp)
        {
            this.page = 1;
            this.GetPageInfo(jp);
            this.userdata = new Hashtable(3);
        }

        public Hashtable userdata { get; private set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int page { get; set; }


        /// <summary>
        /// 搜索信息
        /// </summary>
        public string search { get; set; }

        public int showCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int total
        {
            get
            {
                if (records == 0 || rownum == 0)
                    return 1;
                else
                {
                    return (int)Math.Ceiling(records * 1.0 / rownum);
                }
            }
        }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int records { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int rownum { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public IEnumerable<T> rows { get; set; }

        public void GetPageInfo(JqGridPageInfo jp)
        {
            this.page = jp.page;
            this.rownum = jp.rows;
        }
    }


   
}
