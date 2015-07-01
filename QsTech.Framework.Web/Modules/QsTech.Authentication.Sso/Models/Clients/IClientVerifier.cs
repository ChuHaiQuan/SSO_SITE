using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Authentication.Sso.Models.Authorization;
using QsTech.Core.Interface.Clients;
using QsTech.Framework;
using QsTech.Framework.Logging;

namespace QsTech.Authentication.Sso.Clients
{
    public interface IClientVerifier:IUnitOfWorkDependency
    {
        bool Macth(AuthorizeRequestModel model);

        bool Match(AuthorizeRequestModel model, int userId);
    }

    public class ClientVerifier : IClientVerifier
    {
        private readonly IClientManager _clientManager;
        private readonly IAccountClientManager _accountClientManager;

        public ClientVerifier(IClientManager clientManager,
            IAccountClientManager accountClientManager)
        {
            _clientManager = clientManager;
            _accountClientManager = accountClientManager;
            Logger = NullLogger.Instance;
        }
        public ILogger Logger { get; set; }
        public bool Macth(AuthorizeRequestModel model)
        {
            Logger.Information("检查URL{0}",model.redirect_uri);
            var _clientlist = _clientManager.GetClients();
            if (_clientlist == null)
                return false;
            foreach (var item in _clientlist)
            {
                Logger.Information("检查URL,callback={0},url={1}", item.Callback, model.redirect_uri);
                var isMatch = ExcuteDefaultAccessPolicy(item, model);
                if (isMatch)
                    return true;
            }
            return false;
        }

        public bool Match(AuthorizeRequestModel model, int userId)
        {
            var client = _clientManager.GetClientByApplicationId(model.client_id);
            var receivedurls = model.redirect_uri.Split('?');
            Logger.Information("检查client_id,client_id={0},redirect-uri={1}", model.client_id, receivedurls[0]);
            var clientt = (Client)client;
           // if (client == null || client.Callback.ToLower() != receivedurls[0].ToLower())
            if (client == null || clientt.HostInfos.All(m => m.Callback.ToUpper() != receivedurls[0].ToUpper())) //增加多个客户端查询
                throw new QsTechException("请求的应用不存在或者返回地址错误!"){ NoAuthorizeLayout = true};
            return _accountClientManager.CheckClientAccess(userId,client.Id);
        }


        private bool ExcuteDefaultAccessPolicy(IClient client,AuthorizeRequestModel model)
        {
            var receivedurls = model.redirect_uri.Split('?');
            if (receivedurls != null)
            {
                //return client.Callback.ToUpper() == receivedurls[0].ToUpper();//&&client.ApplicationId==model.Client_Id;
                var clientt = (Client) client;
                return clientt.HostInfos.Any(m => m.Callback.ToUpper() == receivedurls[0].ToUpper());
            }
            return false;
        }

        private bool ExcuteAccessPolicy(IList<ClientAccessPolicy> policies,string url)
        {
            foreach (var policy in policies)
            {
                if (policy.Match(url))
                    return false;
            }
            return true;
        }
    }

}