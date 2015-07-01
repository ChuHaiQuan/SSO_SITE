using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace QsTech.Core.Clients
{
    public class XmlClientParser
    {
        //<?xml version="1.0" encoding="utf-8" ?>
        //<clients>
        //<client id="s1" name="Sample 1" host="https://tradeserving.com" description="sample">
        //</client>
        //</clients>

        private const string ClientCollection = "clients";
        private const string Client = "client";
        private const string ClientId = "id";
        private const string ClientName = "name";
        private const string ClientHost = "host";

        public static IEnumerable<Client> Parse(XDocument xml)
        {
            if (xml.Root.Name != ClientCollection)
            {
                return null;
            }
            var desc = new List<Client>();
            var xElements = xml.Root.Elements(Client);
            if (xElements == null) return null;
            foreach (var item in xElements)
            {
                var xAttribute = int.Parse(item.Attribute(ClientId).Value);
                var xAttribute1 = item.Attribute(ClientName).Value;
                var xAttribute2 = item.Attribute(ClientHost).Value;
                
                var client = new Client(xAttribute, xAttribute1, xAttribute2);
                desc.Add(client);
            }
            return desc;
        }

    }
}