using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QsTech.Core.Interface.Account;
using QsTech.Framework;
using QsTech.Framework.Security;

namespace QsTech.Core.Interface
{
    public interface IAccountManager: IUnitOfWorkDependency
    {
        /// <summary>
        /// 检验账户名、密码的合法性
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns>true:合法;false:非法</returns>
        bool ValidateAccount(string account, string password);

        /// <summary>
        /// 根据id获取帐号信息
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        IAccount GetAccountById(int accountId);

        /// <summary>
        /// 根据账户名获取账户
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        IAccount FindAccountByAccount(string account);

        /// <summary>
        /// 根据ID获取账户的详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SsoAccountModel GetSsoAccountModelById(int id);





    }
}
