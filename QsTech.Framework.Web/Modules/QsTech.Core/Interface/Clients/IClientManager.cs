using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QsTech.Framework;

namespace QsTech.Core.Interface.Clients
{
    public interface IClientManager : ITransientDependency
    {
        IEnumerable<IClient> GetClients();

        IEnumerable<IClient> GetClientByUserId(int userId);

        IClient GetClientById(int id);

        IClient GetClientByApplicationId(string applicationId);
    }
}
