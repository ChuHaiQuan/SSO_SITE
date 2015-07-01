using QsTech.Framework.Security;
using QsTech.Core.Interface;

namespace QsTech.Authentication.Sso.Models
{
    public class AuthService:IAuthService
    {
        private readonly IAccountManager _accountManager;
        public AuthService(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        public bool Login(string accountName, string password, out IAccount account)
        {
            account = _accountManager.FindAccountByAccount(accountName);
            return _accountManager.ValidateAccount(accountName, password);
            //return true;
        }
    }
}