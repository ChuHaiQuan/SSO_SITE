using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QsTech.Framework.Data;
using QsTech.Framework.Security;

namespace QsTech.Core.Interface.Entities
{
    /// <summary>
    /// 只读的用户信息，不用于任何操作。
    /// </summary>
    public class ReadonlyUser //: IEntity, IUser
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

        public virtual DateTime? LastLoginTime { get; set; }

        public virtual DateTime? LatestLoginTime { get; set; }
    }
}
