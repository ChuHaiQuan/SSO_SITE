using NHibernate.Mapping.ByCode.Conformist;
using QsTech.Core.Interface;
using NHibernate.Mapping.ByCode;

namespace QsTech.Core.Models.Roles
{
    public class RoleMapping : ClassMapping<Role>
    {
        public RoleMapping()
        {
            Bag(r => r.Permissions, mapper =>
            {
                mapper.Table("ROLE_PERMISSION");
                mapper.Key(r => r.Column("ROLE_ID"));
                mapper.Inverse(false);
                mapper.Fetch(CollectionFetchMode.Select);
            }, relation => relation.Element(elmapper => { elmapper.Column("PERMISSION_NAME");}));

            //Bag(r => r.Owners, mapper =>
            //{
            //    mapper.Table("USER_ROLE");
            //    mapper.Key(r => r.Column("ROLE_ID"));
            //    mapper.Inverse(false);
            //    mapper.Fetch(CollectionFetchMode.Select);
            //}, relation => relation.Element(elmapper => elmapper.Column("USER_ID")));

            Bag(r => r.Owners, mapper =>
            {
                mapper.Table("ACCOUNT_ROLE");
                mapper.Key(r => r.Column("ROLE_ID"));
                mapper.Inverse(false);
                mapper.Fetch(CollectionFetchMode.Select);
            }, relation => relation.Element(elmapper => elmapper.Column("ACCOUNT_ID")));

       
        }
    }
}