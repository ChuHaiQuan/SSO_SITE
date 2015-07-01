using QsTech.Framework.Security.Permissions;
namespace QsTech.Core.Interface
{
    public class Menu
    {
        public Menu()
        {
            AccessPermissions = new MenuAccessPermissionCollection();
        }

        public string GroupId { get; set; }

        public string Id { get; set; }

        public int Order { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public bool ComingSoon { get; set; }

        public MenuAccessPermissionCollection AccessPermissions { get; private set; }

        public Menu SetName(string name)
        {
            Name = name;
            return this;
        }

        public Menu SetDescription(string description)
        {
            Description = description;
            return this;
        }

        public Menu SetOrder(int order)
        {
            Order = order;
            return this;
        }

        public Menu SetUrl(string url)
        {
            Url = url;
            return this;
        }

        public Menu SetComingSoon(bool enable)
        {
            ComingSoon = enable;
            return this;
        }

        public Menu AddAccessPermission(Permission permission)
        {
            AccessPermissions.Add(permission);
            return this;
        }
    }
}
