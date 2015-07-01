using System.Collections.Generic;
using System.Linq;
using QsTech.Core.Interface;
using QsTech.Framework.Security;

namespace QsTech.Core.Home
{
    public class HomeZoneService : IHomeZoneService
    {
        private readonly IHomeZoneMetadataHolder _holder;
        private readonly IAuthorizer _authorizer;


        public HomeZoneService(IHomeZoneMetadataHolder holder, IAuthorizer authorizer)
        {
            _holder = holder;
            _authorizer = authorizer;
        }

        public IEnumerable<HomeZoneMetadata> GetCurrentZones(string channelId)
        {
            List<HomeZoneMetadata> rtn = new List<HomeZoneMetadata>();
            IEnumerable<HomeZoneMetadata> src = null;
            if (_holder.GetAllMetadatas().TryGetValue(channelId, out src))
            {
                foreach (var homeZoneMetadata in src)
                {
                    if (homeZoneMetadata.Required != null && homeZoneMetadata.Required.Count>0)
                    {
                        //if(_authorizer.Authorize(homeZoneMetadata.Required))
                        //{
                        //    rtn.Add(homeZoneMetadata);
                        //}
                        foreach (var  re in homeZoneMetadata.Required)
                        {
                            if (_authorizer.Authorize(re))
                            {
                                rtn.Add(homeZoneMetadata);
                                break;
                            }
                        
                        }
                    }
                    else
                    {
                        rtn.Add(homeZoneMetadata);
                    }
                }
                return rtn;
            }
            return Enumerable.Empty<HomeZoneMetadata>();
        }

        public IEnumerable<HomeZone> GetArrangedZones(string channelId, int columns)
        {
            var metadatas = GetCurrentZones(channelId);
            
            return ArrangedZones(metadatas, columns);
        }

        private IEnumerable<HomeZone> ArrangedZones(IEnumerable<HomeZoneMetadata> metadatas, int columns)
        {
            var zones = new List<HomeZone>();
            int idx = 0;
            foreach (var metadata in metadatas)
            {
                var zone = new HomeZone() {Metadata = metadata};
                zone.StartColumn = 0;
                zone.StartRow = idx;
                zone.ColumnSpan = 1;
                zone.RowSpan = metadata.ColumnSpan;
                zones.Add(zone);
            }
            return zones;
        }
    }
}