using QsTech.Core.Interface.AccountRegister;
using QsTech.Core.Interface.Clients;
using QsTech.Framework;

namespace QsTech.Accounts.Models.AccountServices
{
    public class AccountRegisterManager : IAccountRegisterManager
    {
        private readonly IAccountService _accountService;
        private readonly IClientManager _clientManager;

        public AccountRegisterManager(IAccountService accountService,IClientManager clientManager)
        {
            _accountService = accountService;
            _clientManager = clientManager;
        }

        public void Register(AccountRegisterInfo userInfo)
        {
            var accountId=_accountService.Register(userInfo);
            if (!string.IsNullOrEmpty(userInfo.ApplicationId))
            {
                var client = _clientManager.GetClientByApplicationId(userInfo.ApplicationId);
                if (client == null)
                {
                    throw new QsTechException(string.Format("找不到id为{0}的应用!", userInfo.ApplicationId)) { NoAuthorizeLayout = true };
                }
                _accountService.NewAccountAuthorizeClient(accountId, client.Id);
            }
            
        }
    }
}