using System.Collections.Generic;
using QsTech.Core.Interface;
using QsTech.Framework;

namespace QsTech.Core.Home
{
    public interface IHomeZoneMetadataHolder : ISingletonDependency
    {
        IDictionary<string,  IEnumerable<HomeZoneMetadata>> GetAllMetadatas();
    }
}