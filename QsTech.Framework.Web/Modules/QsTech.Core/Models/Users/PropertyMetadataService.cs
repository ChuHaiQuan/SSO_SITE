using System.Collections.Generic;
using System.Linq;
using Autofac.Features.Metadata;
using QsTech.Core.Interface.UserProperty;
using QsTech.Framework.Modules;

namespace QsTech.Core.Models.Users
{
    public class PropertyMetadataService : IPropertyMetadataService
    {
        private readonly IEnumerable<Meta<IPropertyMetadataProvider>> _metadataProviders;
        private readonly IModuleManager _moduleManager;
        private IEnumerable<PropertyMetadataGroup> _metadataGroups;
        private IEnumerable<PropertyMetadata> _metadatas;

        public PropertyMetadataService(IEnumerable<Meta<IPropertyMetadataProvider>> metadataProviders, IModuleManager moduleManager)
        {
            _metadataProviders = metadataProviders;
            _moduleManager = moduleManager;
        }

        public IEnumerable<PropertyMetadata> GetAllValidate()
        {
            if (_metadatas == null)
            {
                _metadatas = _metadataProviders.SelectMany(
                    pvd =>
                        {
                            var props = pvd.Value.GetProperties();
                            return props;
                        }).ToArray();
            }
            return _metadatas;
        }

        public bool ValidateProperty(string propertyName)
        {
            return GetAllValidate().Any(p => p.Name == propertyName);
        }

        public PropertyMetadata GetValidProperty(string propertyName)
        {
            return GetAllValidate().SingleOrDefault(p => p.Name == propertyName);

        }

        public IEnumerable<PropertyMetadata> GetAllValidate(string moduleId)
        {
            var propertyMetadataGroup = GetAllGroup().SingleOrDefault(g => g.Module.Id == moduleId);
            if (propertyMetadataGroup != null)
                return propertyMetadataGroup.Properties;
            return null;
        }

        public IEnumerable<PropertyMetadataGroup> GetAllGroup()
        {
            if (_metadataGroups == null)
            {
                _metadataGroups = _metadataProviders.Select(
                    p =>
                        {
                            var group = new PropertyMetadataGroup();
                            group.Module = _moduleManager.GetModule((string) p.Metadata["Module"]).Descriptor;
                            group.Name = p.Value.Name;
                            group.Prority = p.Value.Prority;
                            group.UiAction = p.Value.UiAction;
                            group.Properties = p.Value.GetProperties();
                            return group;
                        }).ToArray();
            }
            return _metadataGroups;
        }
    }
}