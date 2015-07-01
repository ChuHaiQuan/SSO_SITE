

namespace QsTech.Core.Interface
{
    public class GroupMenu
    {
        public string Id { get; set; }

        public int Order { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public GroupMenu SetName(string name)
        {
            Name = name;
            return this;
        }
        public GroupMenu SetDescription(string description)
        {
            Description = description;
            return this;
        }

        public GroupMenu SetUrl(string url)
        {
            Url = url;
            return this;
        }

        public GroupMenu SetOrder(int order)
        {
            Order = order;
            return this;
        }
    }
}