using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Accounts.Models.Accounts;
using QsTech.Framework.Data;

namespace QsTech.Accounts.Models.Registers
{
    public class RegisterAccounSearchParameter:DataQueryAndSearchOptions<Account>
    {
        public int RoleId { get; set; }

        public override IQueryable<Account> AppendTo(IQueryable<Account> superset)
        {
            if (!string.IsNullOrEmpty(this.keyword))
                superset = superset.Where(m => m.Name.Contains(this.keyword));
            superset = superset.OrderBy(m => m.Name);
            return superset;
        }
    }
}