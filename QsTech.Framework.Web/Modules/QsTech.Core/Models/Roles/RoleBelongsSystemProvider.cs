using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Framework.Caching;
using Autofac.Features.Metadata;

namespace QsTech.Core.Models.Roles
{
    public class RoleBelongsSystemEntry
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }

    public interface IRoleBelongsSystemProvider : QsTech.Framework.ITransientDependency
    {
        IEnumerable<RoleBelongsSystemEntry> GetSystems();
    }

    public class RoleBelongsSystemProvider : IRoleBelongsSystemProvider
    {
        public static readonly RoleBelongsSystemEntry SupplierSystem = new RoleBelongsSystemEntry { Name = "供应商系统", Value = "SupplierSystem" };
        public static readonly RoleBelongsSystemEntry PortalServiceSystem = new RoleBelongsSystemEntry { Name = "宁波贝发SSO管理平台", Value = "PortalSystem" };

        public IEnumerable<RoleBelongsSystemEntry> GetSystems()
        {
            yield return PortalServiceSystem;
        }
    }

    public interface IRoleBelongsSystemService : QsTech.Framework.IUnitOfWorkDependency
    {
        IEnumerable<RoleBelongsSystemEntry> GetSystems();

        RoleBelongsSystemEntry FindByValue(string value);

        RoleBelongsSystemEntry FindByName(string name);
    }

    public class RoleBelongsSystemService : IRoleBelongsSystemService
    {
        private readonly IEnumerable<Meta<IRoleBelongsSystemProvider>> _providers;
        private readonly ICache<string, IEnumerable<RoleBelongsSystemEntry>> _entriesCache;
        private const string EntriesCacheKey = "Global";

        public RoleBelongsSystemService(IEnumerable<Meta<IRoleBelongsSystemProvider>> providers,
            ICacheManager cacheManager)
        {
            _providers = providers;
            _entriesCache = cacheManager.Get<string, IEnumerable<RoleBelongsSystemEntry>>("AllRoleBelongsSystemEntry");
        }

        public IEnumerable<RoleBelongsSystemEntry> GetSystems()
        {
            return _entriesCache.GetOrAdd(EntriesCacheKey, context =>
            {
                context.Token = new UtcToken(DateTime.Now.AddDays(1));
                return _providers.SelectMany(meta => meta.Value.GetSystems());
            });
        }

        public RoleBelongsSystemEntry FindByValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Argument \"value\" is required.", "value");
            }
            return GetSystems().FirstOrDefault(sys => sys.Value == value);
        }

        public RoleBelongsSystemEntry FindByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Argument \"name\" is required.", "name");
            }
            return GetSystems().FirstOrDefault(sys => sys.Name == name);
        }
    }

}