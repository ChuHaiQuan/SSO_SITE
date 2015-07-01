using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QsTech.Core.Interface.User;
using QsTech.Framework;
using QsTech.Framework.Security;

namespace QsTech.Core.Interface
{
    public interface IUserManager : IUnitOfWorkDependency
    {
        IUser FindUserByAccount(string account);

        IUser FindByERPKey(string erpKey);

        IEnumerable<IUser> FindUsersByPermission(QsTech.Framework.Security.Permissions.Permission permission);

        IEnumerable<IUser> FindUsersByPermission(QsTech.Framework.Security.Permissions.Permission permission,int? EnterpriseId);

        IUser GetUser(int id);

        IEnumerable<IUser> GetUsers();

        bool ValidateUser(string account, string password);

        void RecordLogonTime(IUser user);

        string GetERPKey(int id);

       

        int GetEnterpriseId(IUser user);


        IUser GetSystemDefaultUser();

        string GetUserEmail(int userKey);

        string GetUserMobilePhone(int userKey);

        IEnumerable<IUser> GetUsersByOwerId(int ownerId);

      //  IUser AddSsoUser(SsoUserModel userModel);

        /// <summary>
        /// 根据openId获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IUser GetUserByOpenId(int openId);

        /// <summary>
        /// 根据openId获取Iuser信息
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        IUser GetUserByAccountId(int openId);

    }
}
