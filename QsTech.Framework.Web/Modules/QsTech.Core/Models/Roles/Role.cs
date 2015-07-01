using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Framework.Data;
using QsTech.Core.Interface;

namespace QsTech.Core.Models.Roles
{
    public class Role : IRole, IEntity
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }
         
        public virtual bool Enabled { get; set; }

        public virtual string Category { get; set; }

        public virtual string BelongsTo { get; set; }

        public virtual IList<int> Owners { get; set; }

        public virtual IList<string> Permissions { get; set; }

        //public virtual IList<string> Navigations { get; set; }
    }
}