using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QsTech.Framework.Data;

namespace QsTech.Core.Interface.Entities
{
    public interface IPort
    {
        int Id { get; set; }
        string NameEn { get; set; }
        string NameCn { get; set; }
    }
}
