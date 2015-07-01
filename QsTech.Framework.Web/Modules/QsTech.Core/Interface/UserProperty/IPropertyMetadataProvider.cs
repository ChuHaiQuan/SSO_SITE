using System.Collections.Generic;
using System.Linq;
using QsTech.Framework;

namespace QsTech.Core.Interface.UserProperty
{
    public interface IPropertyMetadataProvider : IDependency
    {
        string Name { get; }
        int Prority { get; }
        System.Tuple<string, string> UiAction { get; }
        IEnumerable<PropertyMetadata> GetProperties();
    }
}
