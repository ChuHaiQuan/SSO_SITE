using QsTech.Framework.Data;

namespace QsTech.Core.Interface
{
    public class ScopeComponent : IComponent
    {
        public virtual ScopeType Type { get; set; }

        public virtual string Id { get; set; }
    }

    public enum ScopeType
    {
        /// <summary>
        /// 某用户可见
        /// </summary>
        User,
        /// <summary>
        /// 某组织结构可见
        /// </summary>
        Organization,
        /// <summary>
        /// 某权限可见
        /// </summary>
        Permission,
    }
}
