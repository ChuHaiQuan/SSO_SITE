using System.Linq;
using QsTech.Accounts.Models.Accounts;
using QsTech.Framework.Data;

namespace  QsTech.Accounts.Models.AccountServices
{
    public class RoleAccountSearchParameter : DataQueryAndSearchOptions<Account>
    {
        public int RoleId { get; set; }

        public override IQueryable<Account> AppendTo(IQueryable<Account> superset)
        {
            superset = superset.Where(m => m.Roles.Contains(RoleId));
            if (!string.IsNullOrEmpty(this.keyword))
                superset = superset.Where(m => m.AccountName.Contains(this.keyword));
            superset = superset.OrderByDescending(m => m.AccountName);
            return superset;
        }

    }
}