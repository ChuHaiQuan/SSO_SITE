namespace QsTech.Core.Interface.UserProperty
{
    public class PropertyMetadata
    {
        public string Description { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string DefaultValue { get; set; }
    }

    public static class PropertyMetadataIPropertyExtensions
    {
        public static PropertyMetadata Named(this PropertyMetadata prop, string name)
        {
            prop.Name = name;
            return prop;
        }

        public static PropertyMetadata Describe(this PropertyMetadata prop, string description)
        {
            prop.Description = description;
            return prop;
        }
    }
}
