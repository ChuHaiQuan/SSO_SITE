using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Core.Interface.Entities;
using NHibernate.Mapping.ByCode.Conformist;

namespace QsTech.Core.Entities
{
    public class DicPortMapping : ClassMapping<DicPort>
    {
        public DicPortMapping()
        {
            this.Property(prop => prop.Number, mapping =>
            {
                mapping.Column("`NUMBER`");     // 对 ORACLE 关键字的映射处理。
            });
        }
    }
}