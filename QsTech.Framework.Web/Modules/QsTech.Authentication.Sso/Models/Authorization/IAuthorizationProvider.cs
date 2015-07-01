using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QsTech.Framework;
using QsTech.Framework.Security;

namespace QsTech.Authentication.Sso.Models.Authorization
{
    /// <summary>
    /// 授权方式提供接口
    /// </summary>
    public interface IAuthorizationProvider : ISingletonDependency
    {
        string ReponseType { get; }

        string GetAuthorizeReponseUrl(IAccount account, AuthorizeRequestModel authorizeModel);
    }
}
