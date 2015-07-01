using QsTech.Core.Interface;
using QsTech.Core.Interface.Clients;

namespace QsTech.Accounts.Models.RoleAccount
{
    public class RoleDescriptor
    {
        public RoleDescriptor(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public RoleDescriptor(IRole role)
            : this(role.Id, role.Name, role.Description)
        {
            Category = role.Category;
        }
        public RoleDescriptor(IClient client)
            : this(client.Id, client.Name,client.Description)
        {
            Host = client.Host;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Allow { get; set; }

        public string Category { get; set; }

        public string Host { get; set; }
    }
}