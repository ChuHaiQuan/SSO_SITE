using System.Collections.Generic;
using QsTech.Accounts.Models.Accounts;
using QsTech.Framework;

namespace QsTech.Accounts.Models.AccountServices
{
    public static class AccountSourceType
    {
        /// <summary>
        /// 本地（数据库）
        /// </summary>
        public const string Local = "Local";

        /// <summary>
        /// 系统内置
        /// </summary>
        public const string System = "System";
    }

    public interface IAccountProvider : IUnitOfWorkDependency
    {
        string Source { get; }

        IEnumerable<Account> GetAvailableAccounts();
    }
}
