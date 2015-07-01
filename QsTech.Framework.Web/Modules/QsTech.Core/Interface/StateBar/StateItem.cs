using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QsTech.Core.Interface.StateBar
{
    /// <summary>
    /// 状态项
    /// </summary>
    public class StateItem<TValue>
    {
        /// <summary>
        /// 状态文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 状态值
        /// </summary>
        public TValue Value { get; set; }
        /// <summary>
        /// 记录总数
        /// </summary>
        public int RecordCount { get; set; }
        /// <summary>
        /// 是否是当前选中项
        /// </summary>
        public bool Selected { get; set; }
    }

    public class StateItem : StateItem<object>
    {
        
    }
}
