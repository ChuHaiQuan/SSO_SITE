using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QsTech.Accounts.Models
{

    public class PageParameter<T>
    {
        public PageParameter()
        {
            pageSize = 10;
        }

        private int _pageIndex = 0;

        public virtual int pageSize { get; set; }

        public virtual int pageIndex
        {
            get { return _pageIndex; }
            set
            {
                _pageIndex = (value - 1) < 0 ? 0 : (value - 1);
            }
        }
    }
    public class PagedList<T> //: PageParameter<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public PagedList()
        {

        }

        public PagedList(IEnumerable<T> data, int pageSize, int pageIndex, int itemCount)
        {
            this.rows = data.ToList();
            this.pageIndex = pageIndex;
            this.pageSize = pageSize;
            total = itemCount;
             pageSize = pageSize;
             pageIndex = pageIndex;

            if (total > 0)
                totalPage = (int)Math.Ceiling(total / (double)pageSize);
            else
                totalPage = 0;
        }

        public int totalPage { get; set; }

        public int total { get; set; }

        public  int pageSize { get; set; }

         public  int pageIndex { get; set; }

        public IList<T> rows { get; set; }


        internal protected PagedList(int index, int pageSize, int totalItemCount)
        {
            // set source to blank list if superset is null to prevent exceptions
            total = totalItemCount;
            this.pageSize = pageSize;
            pageIndex = index;

            if (total > 0)
                totalPage = (int)Math.Ceiling(total / (double)pageSize);
            else
                totalPage = 0;
            if (index < 0)
                pageIndex = 0;
            if (pageSize < 1)
                this.pageSize = 10;


        }

        public static PagedList<T> Empty()
        {
            return new PagedList<T>();
        }
    }
}