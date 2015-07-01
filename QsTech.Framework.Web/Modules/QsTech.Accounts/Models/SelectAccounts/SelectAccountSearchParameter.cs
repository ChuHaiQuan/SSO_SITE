using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Accounts.Models.Accounts;
using QsTech.Framework.Data;

namespace QsTech.Accounts.Models.SelectAccounts
{
    public class SelectAccountSearchParameter : DataQueryAndSearchOptions<Account>
    {
        public bool Multiable { get; set; }

        public int[] ExcludeArray { get; set; }

        public override IQueryable<Account> AppendTo(IQueryable<Account> superset)
        {
            if (ExcludeArray != null)
                superset = superset.Where(m => !ExcludeArray.Contains(m.Id));
            if (!string.IsNullOrEmpty(this.keyword))
                superset = superset.Where(m => m.Name.Contains(this.keyword) || m.AccountName.Contains(this.keyword));
            return superset.Distinct();
        }

    }
}