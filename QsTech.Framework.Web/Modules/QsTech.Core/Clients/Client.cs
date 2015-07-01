using System.Collections.Generic;

namespace QsTech.Core.Clients
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }

        public Client()
        {
        }

        public Client(int id, string name, string host)
        {
            Id = id;
            Name = name;
            Host = host;
        }
    }
}