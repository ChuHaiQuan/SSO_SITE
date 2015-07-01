using QsTech.Framework.Data;
using QsTech.Framework.Security;

namespace QsTech.Core.Interface
{
    public class UserLite : IComponent,IUser
    {
        public UserLite()
        {
            
        }

        public UserLite(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public UserLite(IUser user)
            : this(user.Id, user.Name)
        {
        }
        public int Id { get; set; }

        public string Name { get; set; }
    }
}