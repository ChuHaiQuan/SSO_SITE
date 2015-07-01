using System.Collections.Generic;
using System.Linq;
using QsTech.Accounts.Models.Accounts;
using QsTech.Framework.Data;

namespace QsTech.Accounts.Models.AccountServices
{
    public class DefaultAccountProvider : IAccountProvider
    {
        private readonly IRepository<Account> _repAccount;

        public DefaultAccountProvider(IRepository<Account> repAccount)
        {
            _repAccount = repAccount;
        }

        public string Source
        {
            get { return AccountSourceType.Local; }
        }

        public IEnumerable<Account> GetAvailableAccounts()
        {
            return _repAccount.Table.Where(m => !m.Deleted&&m.State==0);
        }
    }
}