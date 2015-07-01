using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace QsTech.Accounts.Models
{

    public class DataQueryOptions<TEntity> : PageParameter<TEntity>
    {
        public string Keyword { get; set; }

        public virtual IQueryable<TEntity> AppendTo(IQueryable<TEntity> superset)
        {
            return superset;
        }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public static class QueryOptionsExtensions
    {
        public static IQueryable<TEntity> Append<TEntity>(this IQueryable<TEntity> superset, DataQueryOptions<TEntity> options)
        {
            return options.AppendTo(superset);
        }



    }
    public static class PagedListExtensions
    {
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> queryable, PageParameter<T> pageParameter)
        {
            return ToPagedList(queryable, pageParameter.pageSize, pageParameter.pageIndex, queryable.Count());

        }

        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> data, PageParameter<T> pageParameter)
        {
            return ToPagedList(data == null ? new List<T>().AsQueryable() : data.AsQueryable(), pageParameter);
        }

        public static PagedList<T> ToPagedList<T>(this IQueryable<T> queryable, DataQueryOptions<T> options)
        {
            return ToPagedList(queryable, options.pageSize, options.pageIndex, queryable.Count());
        }


        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> data, DataQueryOptions<T> options)
        {
            return ToPagedList(data.AsQueryable(), options);
        }


        public static PagedList<T> ToPagedList<T>(this IQueryable<T> queryable, int pageSize, int pageIndex, int itemCount)
        {
            var result = new PagedList<T>(pageIndex, pageSize, itemCount);
            if (result.totalPage > 0)
            {
                if (result.pageIndex == 0)
                {
                    result.rows = queryable.Take(result.pageSize).ToList();
                }
                else if (result.pageIndex + 1 > result.totalPage)
                {
                    result.rows = queryable.Skip((result.totalPage - 1) * result.pageSize).Take(result.pageSize).ToList();
                    result.pageIndex = result.totalPage - 1;
                }
                else
                {
                    result.rows = queryable.Skip((result.pageIndex) * result.pageSize).Take(result.pageSize).ToList();
                }

            }
            result.pageIndex += 1;
            return result;
        }





    }
}