using System.Linq;
using QsTech.Accounts.Models.Accounts;
using QsTech.Framework.Data;

namespace  QsTech.Accounts.Models.ClientAccounts
{
    public class ClientAccountSearchParameter : DataQueryAndSearchOptions<Account>
    {
        public int ClientId { get; set; }

        public override IQueryable<Account> AppendTo(IQueryable<Account> superset)
        {
            superset = superset.Where(m => m.Clients.Select(c=>c.ClientId).Contains(ClientId));
            if (!string.IsNullOrEmpty(this.keyword))
                superset = superset.Where(m => m.AccountName.Contains(this.keyword));
            superset = superset.OrderByDescending(m => m.AccountName);
            return superset;
        }

    }
}