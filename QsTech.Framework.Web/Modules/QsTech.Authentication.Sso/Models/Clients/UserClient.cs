using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Framework.Data;
using NHibernate.Mapping.ByCode.Conformist;

namespace QsTech.Authentication.Sso.Clients
{
    public class UserClient : IEntity
    {
        public virtual int Id { get; set; }

        public virtual int UserId { get; set; }

        public virtual int ClientId { get; set; }

        public virtual string UserName { get; set; }

    }

    public class UserClientMapping : ClassMapping<UserClient>
    {
        public UserClientMapping()
        {
            // this.Table("USER_ClIENT");
        }
    }
}