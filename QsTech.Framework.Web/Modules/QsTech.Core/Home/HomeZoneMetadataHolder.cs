using System.Collections.Generic;
using System.Linq;
using Autofac.Features.Metadata;
using QsTech.Core.Interface;

namespace QsTech.Core.Home
{
    public class HomeZoneMetadataHolder : IHomeZoneMetadataHolder
    {
        private readonly IEnumerable<Meta<IHomeZoneProvider>> _providers;
        private IDictionary<string,  IEnumerable<HomeZoneMetadata>> _metadatas;

        public HomeZoneMetadataHolder(IEnumerable<Meta<IHomeZoneProvider>> providers)
        {
            _providers = providers;
        }

        public IDictionary<string, IEnumerable<HomeZoneMetadata>> GetAllMetadatas()
        {
            EnsureMetedata();
            return _metadatas;
        }

        private void EnsureMetedata()
        {
            if (_metadatas == null)
            {
                var source = _providers.SelectMany(
                    p =>
                        {
                            var m = p.Value.GetHomeZones().ToArray();
                            var mId = (string)p.Metadata["Module"];
                            foreach (var homeZoneMetadata in m)
                            {
                                homeZoneMetadata.ModuleId = mId;
                            }
                            return m;
                        }).ToArray();
                _metadatas = source.OrderBy(m => m.Priority).GroupBy(m => m.ChannelId)
                    .ToDictionary(g => g.Key, g => g.AsEnumerable());
            }
        }
    }
}