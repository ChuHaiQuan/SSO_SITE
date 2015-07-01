using System.Collections.Generic;
using QsTech.Accounts.Models.Accounts;
using QsTech.Framework.Security;

namespace QsTech.Accounts.Models.AccountServices
{
    public class SystemAccountProvider:IAccountProvider
    {
        private readonly IEncryption _encryption;

        public SystemAccountProvider(IEncryption encryption)
        {
            _encryption = encryption;
        }

        public string Source
        {
            get { return AccountSourceType.System; }

        }

        public IEnumerable<Account> GetAvailableAccounts()
        {
            yield return new Account()
            {
                Id = 10,
                AccountName ="qsadmin",
                Name = "内置管理员",
                Password =EncryptPassword("a6023"),
                SystemAccount_N = true
            };
        }

        string EncryptPassword(string original)
        {
            return _encryption.GetBase64Hash(System.Text.Encoding.UTF8.GetBytes(original.ToCharArray()));
        }
    }
}