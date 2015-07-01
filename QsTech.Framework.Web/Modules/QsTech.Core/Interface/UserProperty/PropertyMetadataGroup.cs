using System;
using System.Collections.Generic;
using QsTech.Framework.Modules;

namespace QsTech.Core.Interface.UserProperty
{
    public class PropertyMetadataGroup
    {
        public string Name { get; set; }

        public int Prority { get; set; }

        public Tuple<string, string> UiAction { get; set; }

        public ModuleDescriptor Module { get; set; }

        public IEnumerable<PropertyMetadata> Properties { get; set; }
    }
}