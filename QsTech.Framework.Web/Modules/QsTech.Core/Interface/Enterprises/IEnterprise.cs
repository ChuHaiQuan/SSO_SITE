using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QsTech.Core.Interface
{
    public interface IEnterprise
    {
          int Id {get;}

          string NameCn {get;}

          string NameEn {get;}

          string Short {get;}

          string ErpCode { get; }

          bool IsSubject { get; set; }
    }
}
