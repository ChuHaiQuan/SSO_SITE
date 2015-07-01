using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QsTech.Core.Interface.Clients
{
    public interface IClient
    {
       int Id { get;}

       string Name { get;}

       string Host { get;}
       
       string Callback { get;}

       string ApplicationId { get; }

       string ApplicationSecret { get;}

        string Description { get; }

        bool IsClient { get; set; }

    }
}
