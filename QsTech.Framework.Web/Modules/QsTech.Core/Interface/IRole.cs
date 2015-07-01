using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QsTech.Core.Interface
{
    public interface IRole
    {
        int Id { get; }

        string Name { get; }

        string Description { get; }

        IList<string> Permissions { get;}

        IList<int> Owners { get; }

          string Category { get; }
    }
}
