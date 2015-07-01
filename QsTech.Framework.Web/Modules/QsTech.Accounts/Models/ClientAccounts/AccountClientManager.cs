using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Accounts.Models.AccountServices;
using QsTech.Core.Interface.Clients;

namespace QsTech.Accounts.Models.ClientAccounts
{
    public class AccountClientManager : IAccountClientManager
    {
        private readonly IAccountService _accountService;

        public AccountClientManager(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public bool CheckClientAccess(int accountId, int clientId)
        {
            return _accountService.CheckClientAccessPermission(accountId, clientId);
        }
    }
}