using System;

namespace QsTech.Core.Interface.AccountRegister
{
    public enum AccountRegisterInfoStatus
    {
        New = 0,
        Complete = 1
    }

    public class AccountRegisterInfo : QsTech.Framework.Data.IEntity
    {
        public virtual AccountRegisterInfoStatus Status { get; set; }

        public virtual int Id { get; set; }

        public virtual string CompanyName { get; set; }
               
        public virtual string CompanyShortName { get; set; }
               
        public virtual string CompanyAddress { get; set; }
               
        public virtual string CompanyPhone { get; set; }
               
        public virtual string CompanyFax { get; set; }
               
        public virtual string CompanyEmail { get; set; }
               
        public virtual string CompanyLinkman { get; set; }
               
        public virtual string CompanyLinkmanPhone { get; set; }

        public virtual string Name { get; set; }
               
        public virtual string LoginName { get; set; }
               
        public virtual string LoginPassword { get; set; }
               
        public virtual string Gender { get; set; }
               
        public virtual int? Age { get; set; }
               
        public virtual DateTime? Birthday { get; set; }
               
        public virtual string MobilePhone { get; set; }
               
        public virtual string Email { get; set; }
               
        public virtual string Telephone { get; set; }

        public virtual DateTime CreateDate { get; set; }

        public virtual int? UserId { get; set; }

        public virtual string Remark { get; set; }

        public virtual string ApplicationId { get; set; }
    }
}
