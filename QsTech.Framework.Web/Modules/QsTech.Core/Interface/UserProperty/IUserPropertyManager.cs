using QsTech.Framework;
using QsTech.Framework.Security;

namespace QsTech.Core.Interface.UserProperty
{
    public interface IUserPropertyManager : IUnitOfWorkDependency
    {
        string GetValue(IUser user, string name);
        void SetValue(IUser user, string name, string value);
    }
}
