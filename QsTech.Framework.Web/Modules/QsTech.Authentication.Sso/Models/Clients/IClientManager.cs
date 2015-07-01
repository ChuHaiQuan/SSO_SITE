using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QsTech.Framework;
using QsTech.Framework.Environment;
using QsTech.Framework.Caching;
using QsTech.Framework.Utility;
namespace QsTech.Authentication.Sso.Clients
{
    //public interface IClientManager : IUnitOfWorkDependency
    //{
    //    IEnumerable<Client> GetClients();

    //    IEnumerable<Client> GetClientByUserId(int userId);

        
    //    Client GetClientById(int id);
    //}


    //[Feature("Client")]
    //public class ClientClientManager : IClientManager
    //{
    //     private readonly ISsoSetting _setting;
    //     private readonly ICache<string, IEnumerable<Client>> _cacheClient;
    //     public ClientClientManager(ISsoSetting setting)
    //    {
    //        _setting = setting;
    //        //_cacheClient = cache.Get<string, IEnumerable<Client>>("client");
    //    }

    //    #region
    //    public IEnumerable<Client> GetClients()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Client GetClientById(int id)
    //    {
    //        throw new NotImplementedException();
    //    }
    //    #endregion

    //    public Client GetClientByAppType(ClientAppEnum client)
    //    {
    //        var invoker = new WebInvoke();
    //        try
    //        {
    //            var clients = invoker.Get<Client>(_setting.GetClientByAppEnumUrl, new Dictionary<string, string>() { { "type", ((int)client).ToString() } });
    //            return clients;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }

    //    }

    //    public IEnumerable<Client> GetClientByUserId(int userId)
    //    {
    //        var invoker = new WebInvoke();
    //        try
    //        {
    //            var clients = invoker.Get<Client[]>(_setting.GetUserClientsUrl, new Dictionary<string, string>() { { "userId", userId.ToString() } });
    //            return clients;
    //        }
    //        catch
    //        {
    //            return new Client[0];
    //        }
    //    }

    //    public Client GetClientByReturnUrl(string url)
    //    {
    //        return null;
    //    }

    //}
}
