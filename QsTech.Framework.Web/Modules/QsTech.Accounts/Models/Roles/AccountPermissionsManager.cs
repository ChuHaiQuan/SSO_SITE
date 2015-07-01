using System.Collections.Generic;
using System.Linq;
using QsTech.Accounts.Models.AccountServices;
using QsTech.Core.Interface;
using QsTech.Core.Interface.User;
using QsTech.Framework.Caching;
using QsTech.Framework.Events;
using QsTech.Framework.Data;
using QsTech.Core.Interface.Entities;

namespace QsTech.Accounts.Models.Roles
{
    public class AccountPermissionsManager : IAccountPermissionsManager
    {
        private readonly IEnumerable<IUserPermissionPattern> _userPermissionsPattern;
        private readonly ICache<int, IEnumerable<string>> _userPermissionsCache;
        private readonly IEventManager _eventManager;
        private readonly IAccountService _accountService;

        public AccountPermissionsManager(IEnumerable<IUserPermissionPattern> userPermissionsPattern,
            ICacheManager cacheManager,
            IEventManager eventManager,
            IAccountService accountService
            )
        {
            _userPermissionsPattern = userPermissionsPattern;
            _userPermissionsCache = cacheManager.Get<int, IEnumerable<string>>("USER_PERMISSIONS_CACHE");
            _eventManager = eventManager;
            _accountService = accountService;
        }

        public IEnumerable<string> GetUserPermissions(Framework.Security.IUser user)
        {
            return _userPermissionsCache.GetOrAdd(user.Id, context =>
            {
                foreach (var item in _userPermissionsPattern)
                {
                    if (item.Type == user.GetAccountType())
                        return item.GetUserPermissions(user);
                }
                return Enumerable.Empty<string>();
            });
        }

        public void AuthorizeRole(int userId, int roleId)
        {
            var account = _accountService.GetAccountById(userId);
            if (account.SystemAccount_N)
            {
                throw new QsTech.Framework.QsTechException(string.Format("系统用户[{0}]，无法分配角色。", account.Name));
            }
            if (account.Roles != null && !account.Roles.Any(r => r == roleId))
            {
                account.Roles.Add(roleId);
            }
            _accountService.UpdateAccount(account);
            _eventManager.Trigger<IRolePermissionsChangedEventHandler>(caller => caller.ResetUserPermissionsCache(new UserLite(account.Id,account.AccountName)));
        }

        public void UnAuthorizeRole(int userId, int roleId)
        {
            var account = _accountService.GetAccountById(userId);
            if (account.Roles != null && account.Roles.Any(r => r == roleId))
            {
                account.Roles.Remove(roleId);
            }
            _accountService.UpdateAccount(account);
            _eventManager.Trigger<IRolePermissionsChangedEventHandler>(caller => caller.ResetUserPermissionsCache(new UserLite(account.Id, account.AccountName)));
        }
    }
}