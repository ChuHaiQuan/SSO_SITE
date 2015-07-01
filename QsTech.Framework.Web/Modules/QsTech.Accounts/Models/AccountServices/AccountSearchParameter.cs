using System.Linq;
using QsTech.Accounts.Models.Accounts;
using QsTech.Framework.Data;

namespace QsTech.Accounts.Models.AccountServices
{
    public class AccountSearchParameter : DataQueryAndSearchOptions<Account>
    {
        public override IQueryable<Account> AppendTo(IQueryable<Account> superset)
        {
            if (!string.IsNullOrEmpty(this.keyword))
                superset = superset.Where(m => m.Name.Contains(this.keyword)||m.AccountName.Contains(this.keyword));
            superset = superset.OrderBy(m => m.AccountName);
            return superset;
        }


      
    }

    public class AccountSearchNParameter : DataQueryOptions<Account>
    {
        public override IQueryable<Account> AppendTo(IQueryable<Account> superset)
        {
            if (!string.IsNullOrEmpty(this.Keyword))
                superset = superset.Where(m => m.Name.Contains(this.Keyword) || m.AccountName.Contains(this.Keyword));
            superset = superset.OrderBy(m => m.AccountName);
            return superset;
        }



    }

    public class AccountSearchArgument
    {

        public string Keyword { get; set; }

        public int Page { get; set; }

        public int Limit { get; set; }
    }
}