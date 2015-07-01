using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Mapping.ByCode.Conformist;
using QsTech.Core.Interface.Account;

namespace QsTech.Core.Models.Accounts
{
    public class AccountUserMapping : ClassMapping<AccountUser>
    {
        public AccountUserMapping()
        {

            
        }
    }
}