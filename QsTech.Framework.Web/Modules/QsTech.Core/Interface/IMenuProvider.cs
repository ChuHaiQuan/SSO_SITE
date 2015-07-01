using QsTech.Framework;

namespace QsTech.Core.Interface
{
    public interface IMenuProvider : ISingletonDependency
    {
        void BuildMenus(IMenuBuilder builder);
    }
}