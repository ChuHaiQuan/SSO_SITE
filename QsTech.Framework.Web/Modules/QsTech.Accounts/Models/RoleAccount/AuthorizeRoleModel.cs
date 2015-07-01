using System.Collections.Generic;

namespace QsTech.Accounts.Models.RoleAccount
{
    public class AuthorizeRoleModel
    {
        public int UserId { get; set; }

        public IEnumerable<RoleDescriptor> RoleDescriptors { get; set; }
    }
}