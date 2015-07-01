namespace QsTech.Core.Interface
{
    public interface IMenuBuilder
    {
        Menu Menu(string groupId, string id);
        GroupMenu Group(string id);
    }
}