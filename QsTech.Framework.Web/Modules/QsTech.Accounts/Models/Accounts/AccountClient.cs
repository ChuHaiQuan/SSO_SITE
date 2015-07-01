using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Mapping.ByCode.Conformist;
using QsTech.Framework.Data;

namespace QsTech.Accounts.Models.Accounts
{
    public class AccountClient:IEntity
    {
        public virtual int Id { get; set; }

        public virtual int AccountId { get; set; }

        public virtual int ClientId { get; set; }

    }

    public class AccountClientMapping : ClassMapping<AccountClient>
    {
        public AccountClientMapping()
        {
            //Bag(r => r.Roles, mapper =>
            //{
            //    mapper.Table("ACCOUNT_ROLE");
            //    mapper.Key(r => r.Column("ACCOUNT_ID"));
            //    mapper.Inverse(false);
            //    mapper.Fetch(CollectionFetchMode.Join);
            //}, relation => relation.Element(elmapper => elmapper.Column("ROLE_ID")));
        }
    }
}