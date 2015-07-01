using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Framework.Data;

namespace QsTech.Core.Interface.Entities
{
    public class DicPort : IPort, IEntity
    {
        public virtual int Id { get; set; }
        public virtual string NameEn { get; set; }
        public virtual string NameCn { get; set; }
        public virtual int? CountryId { get; set; }
        public virtual int? Number { get; set; }
        public virtual bool IsLocal { get; set; }
        /// <summary>
        /// 从ERP导入之后，用来关联数据的键值，必须确保唯一。
        /// </summary>
        public virtual string ErpCode { get; set; }
    }
}