using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Core.Interface;
using QsTech.Framework;
using QsTech.Framework.Security;

namespace QsTech.Authentication.Sso.Models.Authorization
{
    public interface IAuthorizationCodeManager: ISingletonDependency
    {
        /// <summary>
        ///  根据用户创建授权码,并返回授权码
        /// </summary>
        /// <returns></returns>
        string CreateAndReturnAuthorizationCode(IAccount account);

        /// <summary>
        /// 根据授权码获取用户信息,删除缓存中授权码
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msgref">返回信息标记信息</param>
        /// <returns></returns>
        AuthorizationCodeCacheModel GetUserInfoByAuthorizationCode(string code, ref string msgref);



    }


   
}