using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Framework.Data;

namespace QsTech.Core.Models
{
    public class ActionStatus:IEntity
    {
        public  virtual int Id { get; set; }

        public virtual int Type { get; set; }

        public virtual string EntityId { get; set; }

        public virtual string EntityCode { get; set; }
        /// <summary>
        /// 操作人员ID
        /// </summary>
        public virtual int OperatorId { get; set; }

        public virtual string OperatorName { get; set; }

        public virtual int OwnerId { get; set; }

        public virtual string OwnerName { get; set; }

        public virtual DateTime OperatorDate { get; set; }

        /// <summary>
        /// 操作动作，相应业务实体的状态码
        /// </summary>
        public virtual string Action { get; set; }
        /// <summary>
        /// 业务动作说明
        /// </summary>
        public virtual string ActionDescription { get; set; }
    }
}