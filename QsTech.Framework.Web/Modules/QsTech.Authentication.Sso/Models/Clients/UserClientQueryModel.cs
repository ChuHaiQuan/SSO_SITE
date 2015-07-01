using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Framework.Pagination;

namespace QsTech.Authentication.Sso.Clients
{
    public class UserClientQueryModel :IQueryParamter
    {
        public string keyword
        {
            get;
            set;
        }

        public int ClientId { get; set; }


        public string tips
        {
            get { throw new NotImplementedException(); }
        }
    }
}