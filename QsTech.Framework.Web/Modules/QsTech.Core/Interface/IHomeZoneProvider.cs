using System.Collections.Generic;
using QsTech.Framework;

namespace QsTech.Core.Interface
{
    public interface IHomeZoneProvider : ITransientDependency
    {
        IEnumerable<HomeZoneMetadata> GetHomeZones();

    }
}