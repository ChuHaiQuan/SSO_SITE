using System;
using System.Collections.Generic;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using QsTech.Core.Interface.Account;
using QsTech.Framework.Data;
using QsTech.Framework.Security;

namespace QsTech.Accounts.Models.Accounts
{
    public class Account:IEntity,IAccount
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual int? Age { get; set; }

        public virtual string Gender { get; set; }

        public virtual string Telephone { get; set; }

        public virtual string Mobile { get; set; }

        public virtual string Email { get; set; }

        public virtual DateTime? Birthday { get; set; }

        public virtual string AccountName { get; set; }

        public virtual string Password { get; set; }

        public virtual DateTime? LastLoginTime { get; set; }

        public virtual DateTime? LatestLoginTime { get; set; }

        public virtual bool Deleted { get; set; }

        public virtual string AntiPhishing { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public virtual string JobNumber { get; set; }

        public virtual string Post { get; set; }

        public virtual int? PhotoId { get; set; }

        public virtual string NickName { get; set; }

       

       // public virtual IList<AccountUser> Users { get; set; }

        public virtual IList<int> Roles { get; set; }

        /// <summary>
        /// 是否是系统内置帐号
        /// </summary>
        public virtual bool SystemAccount_N { get; set; }


        public virtual IList<AccountClient> Clients { get; set; }

        public virtual int State { get; set; }

        public virtual string Remark { get; set; }

        public virtual DateTime? CreateTime { get; set; }
    }


    public class AccountMapping : ClassMapping<Account>
    {
        public AccountMapping()
        {
            Bag(r => r.Roles, mapper =>
                                  {
                                      mapper.Table("ACCOUNT_ROLE");
                                      mapper.Key(r => r.Column("ACCOUNT_ID"));
                                      mapper.Inverse(false);
                                      mapper.Fetch(CollectionFetchMode.Join);
                                  }, relation => relation.Element(elmapper => elmapper.Column("ROLE_ID")));


            //Bag(r => r.Clients, mapper =>
            //{
            //    mapper.Table("ACCOUNT_CLIENT");
            //    mapper.Key(r => r.Column("ACCOUNT_ID"));
            //    mapper.Inverse(false);
            //    mapper.Fetch(CollectionFetchMode.Join);
            //}, relation => relation.Element(elmapper => elmapper.Column("ClIENT_ID")));

            Bag(r => r.Clients,
                map =>
                    {
                        map.Inverse(false);
                        map.Fetch(CollectionFetchMode.Join);
                        map.Key(k => k.Column("ACCOUNT_ID"));
                    },
            rel => rel.OneToMany());

            //Bag(r => r.Users, mapper =>
            //{
            //    mapper.Table("ACCOUNT_USER");
            //    mapper.Key(r => r.Column("ACCOUNT_ID"));
            //    mapper.Inverse(false);
            //    mapper.Fetch(CollectionFetchMode.Select);
            //});
        }
    }

}