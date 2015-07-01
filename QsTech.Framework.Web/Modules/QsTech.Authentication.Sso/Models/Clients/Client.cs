using System;
using System.Collections.Generic;
using QsTech.Core.Interface.Clients;

namespace QsTech.Authentication.Sso.Clients
{
    public class Client : IClient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public IList<ClientAccessPolicy> AccessPolicies { get; set; }
        public string Callback { get; set; }

        public string ApplicationId { get; set; }

        public string ApplicationSecret { get; set; }

        public string LogOut { get; set; }

        public string Description { get; set; }
        public bool IsClient { get; set; }

        public List<HostInfo> HostInfos { get; set; }

        public Client()
        {
        }

        public Client(int id, string name, string host, string callback, string logOut, string applicationId, string applicationSecret,string description,string isclient)
        {
            Id = id;
            Name = name;
            Host = host;
            Callback = callback;
            LogOut = logOut;
            ApplicationId = applicationId;
            ApplicationSecret = applicationSecret;
            Description = description;
            IsClient = bool.Parse(isclient);
            var hosts = host.Split(new string[] {@"#$#"}, StringSplitOptions.RemoveEmptyEntries);
            var cbs = Callback.Split(new string[] { @"#$#" }, StringSplitOptions.RemoveEmptyEntries);
            var los = LogOut.Split(new string[] { @"#$#" }, StringSplitOptions.RemoveEmptyEntries);
            HostInfos = new List<HostInfo>();
            for (var i = 0; i < hosts.Length; i++)
            {
                var hi = new HostInfo();
                hi.Host = hosts[i];
                hi.Callback = cbs[i];
                if (los.Length == hosts.Length)
                    hi.LogOut = los[i];
               HostInfos.Add(hi);
            }

        }
    }

    public class HostInfo
    {
        /// <summary>
        ///   
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Callback { get; set; }

        public string LogOut { get; set; }


    }

}