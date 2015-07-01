using System.Linq;
using QsTech.Core.Interface;
using QsTech.Core.Interface.Account;
using QsTech.Framework;
using QsTech.Framework.Logging;
using QsTech.Framework.Security;

namespace QsTech.Accounts.Models.AccountServices
{
    public class AccountManager:IAccountManager
    {
        private readonly IAccountService _accountService;
        private readonly IEncryption _encryption;
        public AccountManager(IAccountService accountService,IEncryption encryption)
        {
            _accountService = accountService;
            _encryption = encryption;
        }

        public ILogger Logger { get; set; }

        public bool ValidateAccount(string account, string password)
        {
            Logger.Debug(string.Format("验证用户 {0}", account));
            var user = _accountService.GetAllAccounts().FirstOrDefault(u => u.AccountName == account);
            if (user != null)
            {
                var encryptionPassword = _encryption.GetBase64Hash(System.Text.Encoding.UTF8.GetBytes(password.ToCharArray()));
                if (string.Compare(encryptionPassword, user.Password, false, System.Globalization.CultureInfo.InvariantCulture) == 0)
                {
                    return true;
                }
                
            }
            return false;
        }

        public IAccount GetAccountById(int accountId)
        {
            return _accountService.GetAllAccounts().FirstOrDefault(m => m.Id == accountId);
        }

        public IAccount FindAccountByAccount(string account)
        {
            return _accountService.GetAllAccounts().FirstOrDefault(u => u.AccountName == account);
        }

        public SsoAccountModel GetSsoAccountModelById(int id)
        {
            var account = _accountService.GetAllAccounts().FirstOrDefault(m => m.Id == id);
            if(account==null) throw  new QsTechException("账户不存在!");
            return new SsoAccountModel()
                       {
                           Id = account.Id,
                           AccountName = account.AccountName,
                           Age = account.Age,
                           Birthday = account.Birthday,
                           Email = account.Email,
                           Gender = account.Gender,
                           Mobile = account.Mobile,
                           Name = account.Name,
                           NickName = account.NickName,
                           Telephone = account.Telephone

                       };
        }
    }
}