using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Core.Interface;
using QsTech.Framework.Caching;
using QsTech.Framework.Logging;

namespace QsTech.Core.Clients
{
    public class ApplicationManager : IApplicationManager
    {
        private readonly ICache<string, IEnumerable<Client>> _cacheClient;
        private readonly ICoreClientLoader _clientLoader;
        public ApplicationManager(ICoreClientLoader clientLoader, ICacheManager cache)
        {
            _clientLoader = clientLoader;
            _cacheClient = cache.Get<string, IEnumerable<Client>>("client");
           Logger = NullLogger.Instance;
        }

        public ILogger Logger;

        public IEnumerable<Client> GetClients()
        {
                return _cacheClient.GetOrAdd("", m =>
                {
                    return _clientLoader.LoadClientsData();
                });
        }

        public string GetApplicationHost(ApplicationEnum client)
        {
           return  GetClients().Where(m => m.Id == (int)client).SingleOrDefault().Host;
        }


    }
}