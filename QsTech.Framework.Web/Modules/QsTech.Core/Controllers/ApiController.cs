using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QsTech.Core.Interface;
using QsTech.Framework.Pagination;

namespace QsTech.Core.Controllers
{
    public class ApiController : Controller
    {
        #region 搜索条 时间部分
        public ActionResult DateRange(DefaultDateRange ranger)
        {
            return View("UserCtl/_DateRange", ranger);
        }
        #endregion

        #region 搜索条 排序部分
        public ActionResult Sort(DefaultListSort sort)
        {
            return View("UserCtl/_Sort", sort);
        }
        #endregion

        #region 搜索条 关键字
        public ActionResult KeyWord(QueryModel query)
        {
            return View("UserCtl/_Keyword", query);
        }
        #endregion

    }
}
