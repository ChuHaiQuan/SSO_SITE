using QsTech.Core.Interface;

namespace QsTech.Accounts.Models.ClientAccounts
{
    public class ClientDescriptor
    {
        public ClientDescriptor(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public ClientDescriptor(IRole role)
            : this(role.Id, role.Name, role.Description)
        {
            Category = role.Category;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Allow { get; set; }

        public string Category { get; set; }
    }
}