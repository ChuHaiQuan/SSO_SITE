using System.Collections.Generic;
using QsTech.Accounts.Models.Accounts;
using QsTech.Accounts.Models.BindUsers;
using QsTech.Core.Interface.AccountRegister;
using QsTech.Framework;
using QsTech.Framework.Data;
using QsTech.Framework.Pagination;
using QsTech.Framework.Security;

namespace QsTech.Accounts.Models.AccountServices
{
    public interface IAccountService : IUnitOfWorkDependency
    {
        /// <summary>
        /// 获取所有的账户信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<Account> GetAllAccounts();
 
        /// <summary>
        /// 获取账户分页列表
        /// </summary>
        /// <returns></returns>
        IPagedList<Account> GetAccounts(DataQueryAndSearchOptions<Account> searchOptions);

        /// <summary>
        /// 根据ID获取账户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Account GetAccountById(int id);

        /// <summary>
        /// 查询登录账户是否存在
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        bool ExistsAccount(string loginName);

        /// <summary>
        /// 添加账户
        /// </summary>
        /// <param name="account"></param>
        void AddAccount(Account account);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        void AuditAccount(Account account);

        /// <summary>
        /// 更新账户信息
        /// </summary>
        /// <param name="account"></param>
        void UpdateAccount(Account account);

        /// <summary>
        /// 删除注册申请账户
        /// </summary>
        /// <param name="account"></param>
        void UpdateRegisterAccount(Account account);

        /// <summary>
        /// 初始化密码
        /// </summary>
        /// <param name="account"></param>
        /// <param name="newInput"></param>
        void InitPassword(int Id, string newInput);
        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="account"></param>
        /// <param name="original"></param>
        /// <param name="newInput"></param>
        void ChangePassword(int accountId, string original, string newInput);


        void BindAccountUser(Account account,BindUserModel model);


        void AuthorizeClient(int accountId, int clientId);

        void AuthorizeClients(int accountId, int[] clientIds);

        void UnAuthorizeClient(int accountId, int clientId);


        void NewAccountAuthorizeClient(int accountId, int clientId);

        /// <summary>
        /// 根据clientID获取账户
        /// </summary>
        /// <param name="clinetId"></param>
        /// <returns></returns>
        IList<int> GetAccountsByClientId(int clinetId);

        IList<int> GetClientsByAccountId(int accountId);
        bool CheckClientAccessPermission(int accountId, int clientId);

        /// <summary>
        /// 申请注册
        /// </summary>
        /// <param name="userInfo"></param>
        int Register(AccountRegisterInfo userInfo);


        /// <summary>
        /// 获取申请注册账户分页列表
        /// </summary>
        /// <returns></returns>
        IPagedList<Account> GetRegisterAccounts(DataQueryAndSearchOptions<Account> searchOptions);

        PagedList<Account> GetAccountPagedList(DataQueryOptions<Account> searchOption);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Account GetRegisterAccountById(int id);
    }
}
