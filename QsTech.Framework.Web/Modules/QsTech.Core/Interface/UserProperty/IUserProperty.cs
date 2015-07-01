namespace QsTech.Core.Interface.UserProperty
{
    public interface IUserProperty
    {
        string Name { get; set; }

        string Value { get; set; }

        string Description { get; set; }

        string Category { get; set; }
    
    }
}