using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Framework.Environment;
using QsTech.Framework;
using QsTech.Framework.Modules.Folders;
using System.IO;
using System.Xml.Linq;
using QsTech.Framework.Logging;

namespace QsTech.Authentication.Sso.Clients
{
    public interface IClientLoader : ISingletonDependency
    {
        IEnumerable<Client> LoadClientsData();
    }


    public class ClientLoader : IClientLoader
    {
        private readonly string _path;
        private IEnumerable<Client> _listClients;
        public ILogger Logger { get; set; }
        public ClientLoader(IHostEnvironment hostEnvironment)
        {
            _path = //System.IO.Path.Combine(moduleFolder.Path, "QsTech.Authentication.Sso", "client.xml");
            System.IO.Path.Combine(hostEnvironment.SitePath,"client.xml");
            Logger = NullLogger.Instance;
        }

        public IEnumerable<Client> LoadClientsData()
        {
            LoadXml();
            return _listClients;
        }

        private void LoadXml()
        {
            using (var fReader = File.OpenRead(_path))
            {
                try
                {
                    var reader = XDocument.Load(fReader);
                    _listClients = GetDescriptorForClients(reader);
                }
                catch(Exception ex)
                {
                    Logger.Information("加载客户端应用信息时出错:{0}",ex.ToString());
                }

            }

        }

        private IEnumerable<Client> GetDescriptorForClients(XDocument clientXml)
        {
            var clientDescriptors = XmlClientParser.Parse(clientXml);
            return clientDescriptors;
        }

    }


}