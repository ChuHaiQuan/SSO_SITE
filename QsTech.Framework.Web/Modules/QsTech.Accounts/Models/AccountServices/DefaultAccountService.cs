using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QsTech.Accounts.Models.Accounts;
using QsTech.Accounts.Models.BindUsers;
using QsTech.Core.Interface.Account;
using QsTech.Core.Interface.AccountRegister;
using QsTech.Framework;
using QsTech.Framework.Caching;
using QsTech.Framework.Data;
using QsTech.Framework.Logging;
using QsTech.Framework.Pagination;
using QsTech.Framework.Security;

namespace QsTech.Accounts.Models.AccountServices
{
    //[Feature("Sso")]
    public class DefaultAccountService:IAccountService
    {
        private readonly IRepository<Account> _repAccount;
        private readonly IEnumerable<IAccountProvider> _accountProviders;
        private readonly IEncryption _encryption;
        private readonly IRepository<AccountClient> _repAccountClient;
        private readonly ICache<string, IEnumerable<Account>> _accountCache;
        public DefaultAccountService(ICacheManager cacheManager, 
            IRepository<Account> repAccount,
            IEnumerable<IAccountProvider> accountProviders,
            IEncryption encryption,
            IRepository<AccountClient> repAccountClient)
        {
            _accountCache = cacheManager.Get<string, IEnumerable<Account>>("AllAccount");
            _repAccount = repAccount;
            _accountProviders = accountProviders;
            _encryption = encryption;
            _repAccountClient = repAccountClient;
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }
        public IEnumerable<Account> GetAllAccounts()
        {
            return GetAccounts();
        }

        public IPagedList<Account> GetAccounts(DataQueryAndSearchOptions<Account> searchOptions)
        {
            var linq = GetAccounts().Where(m=>!m.SystemAccount_N).AsQueryable();
            linq = linq.Append(searchOptions);
            return linq.ToPagedList(searchOptions.page, searchOptions.limit);
        }

        public Account GetAccountById(int id)
        {
            return GetAccounts().FirstOrDefault(m => m.Id == id);
        }

        public bool ExistsAccount(string loginName)
        {
            return GetAccounts().Any(u => u.AccountName == loginName && !u.Deleted);
        }

        public void AddAccount(Account account)
        {
            account.CreateTime = System.DateTime.Now;
            var encryptedPassword = EncryptPassword(account.Password);
            account.Password = encryptedPassword;
            if (account.Name==null)
                account.Name = string.Empty;
            if (account.AccountName == null)
                account.AccountName = string.Empty;
            _repAccount.Create(account);
            ReloadAccountData();
        }

        public void AuditAccount(Account account)
        {
            var encryptedPassword = EncryptPassword(account.Password);
            account.Password = encryptedPassword;
            account.State = 0;
            _repAccount.Update(account);
            ReloadAccountData();
        }

        private string EncryptPassword(string original)
        {
            return _encryption.GetBase64Hash(System.Text.Encoding.UTF8.GetBytes(original.ToCharArray()));
        }

        public void UpdateAccount(Account account)
        {
            if (account.SystemAccount_N)
            {
                throw new QsTechException("系统账户不能更改信息！");
            }
            _repAccount.Update(account);
            ReloadAccountData();
        }

        public void UpdateRegisterAccount(Account account)
        {
            _repAccount.Update(account);
        }

        public void InitPassword(int id, string newInput)
        {
            var account = GetAccountById(id);
            if (account.SystemAccount_N)
            {
                throw new QsTechException("系统账户，无需更改密码！");
            }
            var encryptedPassword = EncryptPassword(newInput);
            account.Password = encryptedPassword;
            _repAccount.Update(account);
            ReloadAccountData();
        }

        public void ChangePassword(int accountId, string original, string newInput)
        {
            Logger.Error(string.Format("accountId:{0}修改密码",accountId));
            var account = GetAccounts().FirstOrDefault(m => m.Id == accountId);
            if (account.SystemAccount_N) throw new SecurityException(string.Format("内置账号 {0} 不需要修改密码。", account.Name));
            if (account == null)
            {
                throw new SecurityException(string.Format("账号 {0} 不存在。", account.Name));
            }
            string encryptedOriginal = EncryptPassword(original);
            if (String.Compare(encryptedOriginal, account.Password, false, System.Globalization.CultureInfo.CurrentCulture) != 0)
            {
                throw new SecurityException("原始密码校验失败。");
            }
            string encryptedPassword = EncryptPassword(newInput);
            account.Password = encryptedPassword;
            _repAccount.Update(account);
            ReloadAccountData();
        }

        public void BindAccountUser(Account account, BindUserModel model)
        {
            //var au = account.Users.FirstOrDefault();
            //if (au!=null)
            //{
            //    if(model.UserId!=null)
            //    {
            //        au.UserId = (int) model.UserId;
            //        au.UserName = model.UserName;
            //    }
            //    else
            //    {
            //        account.Users.Remove(au);
            //    }
            //}
            //else
            //{
            //    if (model.UserId != null)
            //    {
            //        var newaccountUser = new AccountUser();
            //        newaccountUser.AccountId = account.Id;
            //        newaccountUser.AccountName = account.AccountName;
            //        newaccountUser.UserId = (int)model.UserId;
            //        newaccountUser.UserName = model.UserName;
            //        account.Users.Add(newaccountUser);
            //    }
            //}
            //_repAccount.Update(account);

        }

        public void AuthorizeClient(int accountId, int clientId)
        {
            var account = GetAccountById(accountId);
            if (account.SystemAccount_N)
            {
                throw new QsTech.Framework.QsTechException(string.Format("系统用户[{0}]，无需分配应用权限。", account.Name));
            }
            if (account.Clients != null && account.Clients.Select(c=>c.ClientId).All(r => r != clientId))
            {
                var ac = new AccountClient() { AccountId = accountId,ClientId = clientId};
                account.Clients.Add(ac);
            }
            _repAccount.Update(account);
            ReloadAccountData();
        }

        public void AuthorizeClients(int accountId, int[] clientIds)
        {
            foreach (var item in clientIds)
            {
                var ac = new AccountClient();
                ac.AccountId = accountId;
                ac.ClientId = item;
                _repAccountClient.Create(ac);
            }
            _repAccountClient.Flush();
            ReloadAccountData();
        }

        public void UnAuthorizeClient(int accountId, int clientId)
        {
            var ac = _repAccountClient.Fetch(m => m.AccountId == accountId && m.ClientId== clientId).SingleOrDefault();
            if (ac != null)
            {
                _repAccountClient.Delete(ac);
            }
            ReloadAccountData();
        }

        public void NewAccountAuthorizeClient(int accountId, int clientId)
        {
            var account = _repAccount.Fetch(m=>m.Id==accountId).SingleOrDefault();
            account.Clients=new List<AccountClient>();
            account.Roles = new List<int>();
            if (account.Clients != null && account.Clients.Select(c => c.ClientId).All(r => r != clientId))
            {
                var ac = new AccountClient() { AccountId = accountId, ClientId = clientId };
                account.Clients.Add(ac);
            }
            _repAccount.Update(account);
        }

        public IList<int> GetAccountsByClientId(int clinetId)
        {
            return _repAccountClient.Fetch(m => m.ClientId == clinetId).Select(m => m.AccountId).ToList();
        }

        public IList<int> GetClientsByAccountId(int accountId)
        {
            return _repAccountClient.Fetch(m => m.AccountId == accountId).Select(m => m.ClientId).ToList();
        }

        public bool CheckClientAccessPermission(int accountId, int clientId)
        {
            var account = GetAccountById(accountId);
            if(account.SystemAccount_N) return true;
            var ac = _repAccountClient.Table.SingleOrDefault(m => m.AccountId == accountId && m.ClientId == clientId);
            return ac != null;
        }

        public int Register(AccountRegisterInfo userInfo)
        {
          
            var newAccount = new Account()
                                 {
                                     Name = userInfo.Name,
                                     Gender = userInfo.Gender,
                                     Mobile = userInfo.MobilePhone,
                                     Remark = userInfo.Remark,
                                     Email = userInfo.Email,
                                     AccountName = "",
                                     Password = ""
                                 };
            newAccount.CreateTime = System.DateTime.Now;
            newAccount.State = 1;
            _repAccount.Create(newAccount);
            return newAccount.Id;
        }

        public IPagedList<Account> GetRegisterAccounts(DataQueryAndSearchOptions<Account> searchOptions)
        {
            var linq = _repAccount.Fetch(m =>m.State==1&&!m.Deleted).AsQueryable();
            linq = linq.Append(searchOptions);
            return linq.ToPagedList(searchOptions.page, searchOptions.limit);
        }

        public PagedList<Account> GetAccountPagedList(DataQueryOptions<Account> searchOption)
        {
            var linq = GetAccounts().Where(m => !m.SystemAccount_N).AsQueryable();
            linq = linq.Append(searchOption);
            return linq.ToPagedList(searchOption);
        }

        public Account GetRegisterAccountById(int id)
        {
           return _repAccount.Fetch(m => m.Id == id&&m.State==1).SingleOrDefault();
        }

        public void ReloadAccountData()
        {
            foreach (var userProvider in _accountProviders)
            {
               _accountCache.GetToken(userProvider.Source).Invalidate();
            }
        }


        private IEnumerable<Account> GetAccounts()
        {
            IEnumerable<Account> accounts = Enumerable.Empty<Account>();
            foreach (var accountProvider in _accountProviders)
            {
                IAccountProvider provider = accountProvider;
                accounts = accounts.Concat(_accountCache.GetOrAdd(
                    accountProvider.Source,
                    context =>
                    {
                        context.Token = new ManualToken();
                        return provider.GetAvailableAccounts().ToArray();
                    }
                    ));
            }
            return accounts;
        }
    }
}