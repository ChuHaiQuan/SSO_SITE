using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QsTech.Framework.Data;

namespace QsTech.Core.Interface
{
    public class GlobalSetting : IEntity
    {
        public virtual int Id { get; set; }

        public virtual string Category { get; set; }

        public virtual string Name { get; set; }

        public virtual string Value { get; set; }

        public virtual string Description { get; set; }
    }
}
