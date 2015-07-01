namespace QsTech.Core.Interface.AccountRegister
{
    public interface IAccountRegisterManager : QsTech.Framework.IUnitOfWorkDependency
    {
        void Register(AccountRegisterInfo userInfo);
    }
}
