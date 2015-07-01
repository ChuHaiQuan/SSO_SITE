using System;
using QsTech.Core.Home;
using QsTech.Core.Interface;

namespace QsTech.Core.Views.Home
{
    public class HomeZoneViewModel
    {
        private readonly HomeZone _zone;
        private readonly string _locName;

        public HomeZoneViewModel(HomeZone zone, string name)
        {
            _locName = name;
            _zone = zone;
        }

        public int ColumnSpan { get { return _zone.ColumnSpan; } }
        public int RowSpan { get { return _zone.RowSpan; } }

        public Tuple<string, string, string> Url
        {
            get
            {
                return new Tuple<string, string, string>(
                    _zone.Metadata.ModuleId, 
                    _zone.Metadata.Url.Item1,
                    _zone.Metadata.Url.Item2);
            }
        }

        public Tuple<string, string, string> MoreUrl
        {
            get
            {
                if (_zone.Metadata.MoreUrl == null)
                {
                    return null;
                }
                return new Tuple<string, string, string>(
                    _zone.Metadata.ModuleId,
                    _zone.Metadata.MoreUrl.Item1,
                    _zone.Metadata.MoreUrl.Item2);
            }
        }

        public int StartColumn { get { return _zone.StartColumn; } }
        public int StartRow { get { return _zone.StartRow; } }

        public string Name
        {
            get { return _locName; }
        }
    }
}