using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QsTech.Core.Interface;
using QsTech.Framework;
using QsTech.Framework.Environment;

namespace QsTech.Accounts.Models.Settings
{
    //public interface IAccountSetting : ISingletonDependency
    //{
    //    /// <summary>
    //    /// 获取账户列表的url地址
    //    /// </summary>
    //    string GetAcccountListUrl { get; }

    //    /// <summary>
    //    /// 获取账户信息的url地址
    //    /// </summary>
    //    string GetAccountInfoUrl { get; }

    //    string UpdateAccountInfoUrl { get; }

    //    /// <summary>
    //    /// 初始化密码url地址
    //    /// </summary>
    //    string InitPasswordUrl { get; }

    //    /// <summary>
    //    /// 更新密码url地址
    //    /// </summary>
    //    string UpdatePasswordUrl { get; }
    //}


    //public class AccountSetting :IAccountSetting
    //{
    //    private readonly ICoreSetting _coreSetting;

    //    public AccountSetting(ICoreSetting coreSetting)
    //    {
    //        _coreSetting = coreSetting;
    //    }

    //    public string GetAcccountListUrl
    //    {

    //        get { return string.Format("{0}/{1}", _coreSetting.GetHostUrl, "Api/GetAccountsData"); }
    //    }
    //    public string GetAccountInfoUrl
    //    {
    //        get { return string.Format("{0}/{1}", _coreSetting.GetHostUrl, "Api/GetAccountById"); }
    //    }
    //    public string UpdateAccountInfoUrl
    //    {
    //        get { return string.Format("{0}/{1}", _coreSetting.GetHostUrl, "Api/UpdateAccount"); }
    //    }
    //    public string InitPasswordUrl
    //    {
    //        get { return string.Format("{0}/{1}", _coreSetting.GetHostUrl, "Api/InitPassward"); }
    //    }
    //    public string UpdatePasswordUrl
    //    {
    //        get { return string.Format("{0}/{1}", _coreSetting.GetHostUrl, "Api/UpdatePassward"); }
    //    }
    //}
}
