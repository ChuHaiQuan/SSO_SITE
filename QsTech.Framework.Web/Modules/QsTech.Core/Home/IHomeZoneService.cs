using System.Collections.Generic;
using QsTech.Core.Interface;
using QsTech.Framework;

namespace QsTech.Core.Home
{
    public interface IHomeZoneService : ITransientDependency
    {
        IEnumerable<HomeZoneMetadata> GetCurrentZones(string channelId);
        IEnumerable<HomeZone> GetArrangedZones(string channelId, int columns);
    }

    public class HomeZone
    {
        public HomeZone()
        {
            
        }

        public HomeZoneMetadata Metadata { get; set; }

        public int StartRow { get; set; }

        public int StartColumn { get; set; }

        public int RowSpan { get; set; }

        public int ColumnSpan { get; set; }
    }
}