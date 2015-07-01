using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace QsTech.Authentication.Sso.Clients
{
    public class XmlClientParser
    {
        //<?xml version="1.0" encoding="utf-8" ?>
        //<clients>
        //<client id="s1" name="Sample 1" host="https://tradeserving.com" callback="https://tradeserving.com/Core/Sso/RecievedTicket" description="sample">
        //  <accessPolicies>
        //    <clientAccessPolicy accessMode="D1" pattern="" PatternMatchType=""></clientAccessPolicy>
        //    <clientAccessPolicy accessMode="D2" pattern="" PatternMatchType=""></clientAccessPolicy>
        //  </accessPolicies>
        //</client>
        //</clients>

        private const string ClientCollection = "clients";
        private const string Client = "client";
        private const string ClientId = "id";
        private const string ClientName = "name";
        private const string ClientHost = "host";
        private const string ClientCallback = "callback";
        private const string ApplicationId = "applicationId";
        private const string ApplicationSecret = "applicationSecret";
        private const string ClientLogOut = "logOut";
        private const string ClientDescription = "description";
        private const string ClientAccessPolicyCollection = "accessPolicies";
        private const string ClientAccessPolicy = "clientAccessPolicy";
        private const string ClientAccessPolicyAccessMode = "accessMode";
        private const string ClientAccessPolicyPattern = "pattern";
        private const string ClientAccessPolicyPatternMatchType = "patternMatchType";

        private const string ClientIsClient = "isClient";
        public static IEnumerable<Client> Parse(XDocument xml)
        {
            if (xml.Root.Name != ClientCollection)
            {
                return null;
            }
            var desc = new List<Client>();
            var xElements = xml.Root.Elements(Client);
            foreach (var item in xElements)
            {
                var xAttribute = int.Parse(item.Attribute(ClientId).Value);
                var xAttribute1 = item.Attribute(ClientName).Value;
                var xAttribute2 = item.Attribute(ClientHost).Value;
                var xAttribute3 = item.Attribute(ClientCallback).Value;
                var xAttribute7 = item.Attribute(ClientLogOut).Value;
                var xAttribute8 = item.Attribute(ApplicationId).Value;
                var xAttribute9 = item.Attribute(ApplicationSecret).Value;
                var xAttribute10 = item.Attribute(ClientDescription).Value;
                var xAttribute11 = item.Attribute(ClientIsClient)!=null? item.Attribute(ClientIsClient).Value:"false";
                var client = new Client(xAttribute, xAttribute1, xAttribute2, xAttribute3, xAttribute7, xAttribute8, xAttribute9, xAttribute10, xAttribute11);
                var xElements1 = item.Elements(ClientAccessPolicyCollection);
                if (xElements1 != null)
                {
                    client.AccessPolicies = xElements1.Elements(ClientAccessPolicy).Select(n =>
                    {
                        var xAttribute4 =n.Attribute(ClientAccessPolicyAccessMode);
                        var xattribute5 =n.Attribute(ClientAccessPolicyPattern);
                        var xattribute6 =n.Attribute(ClientAccessPolicyPatternMatchType);
                        return new ClientAccessPolicy()
                        {
                            AccessMode = xAttribute4 == null ? AccessMode.All : (AccessMode)(int.Parse(xAttribute4.Value)),
                            Pattern = xattribute5 == null ? null : xAttribute4.Value,
                            PatternMatchType = xattribute6 == null ? PatternMatchType.Head : (PatternMatchType)(int.Parse(xattribute6.Value))
                        };
                    }).ToList();
                }
                desc.Add(client);
            }
            return desc;
        }

    }
}