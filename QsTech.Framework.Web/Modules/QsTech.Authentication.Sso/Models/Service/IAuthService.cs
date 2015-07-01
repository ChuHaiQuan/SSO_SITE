using QsTech.Framework;

namespace QsTech.Authentication.Sso.Models
{
    public interface IAuthService :IUnitOfWorkDependency
    {
        /// <summary>
        /// 根据用户名/密码, 来验证用户名是否合法
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        bool Login(string userName, string password, out QsTech.Framework.Security.IAccount acount);

        ///// <summary>
        ///// 账户和密码验证
        ///// </summary>
        ///// <param name="accountName"></param>
        ///// <param name="password"></param>
        ///// <returns></returns>
        //bool Login(string accountName, string password);


    }
}
