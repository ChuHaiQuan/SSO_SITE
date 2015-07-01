using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QsTech.Core.Interface.User
{
    public sealed class UserMetadata
    {
        public string Name { get; set; }

        public int? Age { get; set; }

        public string Gender { get; set; }

        public string Telephone { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public DateTime? Birthday { get; set; }

        public string AccountName { get; set; }

        public string Password { get; set; }

        //public QsTech.Core.Interface.Entities.ReadonlyUser ParseReadonlyUser(QsTech.Framework.Security.IUser user)
        //{
        //    return new Entities.ReadonlyUser
        //    {
        //        Id = user.Id,
        //        AccountName = AccountName,
        //        Name = Name,
        //        Age = Age,
        //        Gender = Gender,
        //        Telephone =Telephone,
        //        Mobile = Mobile,
        //        Email = Email,
        //        Birthday = Birthday
        //    };
        //}
    }
}
