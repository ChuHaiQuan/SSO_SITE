using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Core.Interface.Entities;
using QsTech.Framework.Data;
using QsTech.Framework.Caching;

namespace QsTech.Core.Entities
{
    public class PortService : IPortService
    {
        private readonly static string PortCacheKey = "Global_Port";

        private readonly ICache<string, IEnumerable<DicPort>> _portCache;

        private readonly IRepository<DicPort> _portRepo;

        public PortService(IRepository<DicPort> portRepo, ICacheManager cacheManager)
        {
            _portRepo = portRepo;
            _portCache = cacheManager.Get<string, IEnumerable<DicPort>>("AllPort");
        }

        public IEnumerable<DicPort> GetAll()
        {
            return _portRepo.Table.OrderBy(p => p.Number);
            //return _portCache.GetOrAdd(PortCacheKey, context =>
            //{
            //    return _portRepo.Table.OrderBy(p => p.Number);
            //});
        }

        public IEnumerable<DicPort> GetLocal()
        {
            return _portRepo.Table.OrderBy(p => p.Number);
            return _portCache.GetOrAdd(PortCacheKey, context =>
            {
                return _portRepo.Table.OrderBy(p => p.Number);
            }).Where(p => p.IsLocal);
        }

        public DicPort GetPort(int id)
        {
            return GetAll().FirstOrDefault(p => p.Id == id);
        }

        public DicPort FindByName(string name)
        {
            return GetAll().FirstOrDefault(p => p.NameCn == name 
                || p.NameEn == name);
        }
        
        public DicPort FindByKey(string key)
        {
            return GetAll().FirstOrDefault(p => p.ErpCode == key);
        }
    }
}