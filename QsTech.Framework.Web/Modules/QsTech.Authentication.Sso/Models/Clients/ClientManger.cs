using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Core.Interface.Clients;
using QsTech.Framework;
using QsTech.Framework.Environment;
using QsTech.Framework.Caching;
using QsTech.Framework.Data;
using QsTech.Framework.Security;
using QsTech.Framework.Logging;

namespace QsTech.Authentication.Sso.Clients
{
    //[Feature("Server")]
    public class ClientManager : IClientManager
    {
        private readonly ICache<string, IEnumerable<Client>> _cacheClient;
        private readonly IClientLoader _clientLoader;
        private readonly IRepository<UserClient> _repUserClient;

        public ClientManager(IClientLoader clientLoader, ICacheManager cache, IRepository<UserClient> repUserClient)
        {
            _clientLoader = clientLoader;
            _cacheClient = cache.Get<string, IEnumerable<Client>>("client");
            _repUserClient = repUserClient;
            Logger = NullLogger.Instance;
        }

        public ILogger Logger;

        public IEnumerable<IClient> GetClients()
        {
            return _cacheClient.GetOrAdd("", m =>
            {
                return _clientLoader.LoadClientsData();
            });
        }

        public IClient GetClientById(int id)
        {
            //if (!_authorizer.Authorize(Permissions.ManagerClient, "没有权限查看应用!"))
            //    return null;
            return GetClients().SingleOrDefault(m => m.Id == id);
        }

        public IClient GetClientByApplicationId(string applicationId)
        {
            return GetClients().SingleOrDefault(m => m.ApplicationId == applicationId);
        }

        public IEnumerable<IClient> GetClientByUserId(int userId)
        {
            var ids = _repUserClient.Fetch(m => m.UserId == userId).Select(m => m.ClientId).ToArray();
            return GetClients().Where(m => ids.Contains(m.Id));
        }

    }



}