using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QsTech.Core.Interface
{
    public interface IDateRange
    {
        DateTime? BeginDate { get; }
        DateTime? EndDate { get; }
        int? Month { get; }
    }

    public class DefaultDateRange:IDateRange
    {
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Month { get; set; }

    }
}
