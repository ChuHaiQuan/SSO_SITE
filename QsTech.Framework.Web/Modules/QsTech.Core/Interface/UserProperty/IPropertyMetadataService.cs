using System.Collections.Generic;
using QsTech.Framework;

namespace QsTech.Core.Interface.UserProperty
{
    public interface IPropertyMetadataService : ISingletonDependency
    {
        IEnumerable<PropertyMetadata> GetAllValidate();

        bool ValidateProperty(string propertyName);

        PropertyMetadata GetValidProperty(string propertyName);

        IEnumerable<PropertyMetadata> GetAllValidate(string moduleId);

        IEnumerable<PropertyMetadataGroup> GetAllGroup();

    }
}