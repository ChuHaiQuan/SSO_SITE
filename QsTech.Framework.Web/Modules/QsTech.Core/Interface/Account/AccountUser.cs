using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QsTech.Framework.Data;

namespace QsTech.Core.Interface.Account
{
    public class AccountUser : IEntity
    {
        public virtual int  Id { get; set; }

        public virtual int AccountId { get; set; }

        public virtual int UserId { get; set; }

        public virtual string AccountName { get; set; }

        public virtual string UserName { get; set; }
    }

   
}
