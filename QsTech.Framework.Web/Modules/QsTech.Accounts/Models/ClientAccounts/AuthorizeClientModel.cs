using System.Collections.Generic;
using QsTech.Accounts.Models.RoleAccount;

namespace QsTech.Accounts.Models.ClientAccounts
{
    public class AuthorizeClientModel
    {
        public int UserId { get; set; }

        public IEnumerable<RoleDescriptor> ClientDescriptors { get; set; }
    }
}