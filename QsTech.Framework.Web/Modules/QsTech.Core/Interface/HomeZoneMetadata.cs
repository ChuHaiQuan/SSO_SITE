using System;
using QsTech.Framework.Security.Permissions;
using System.Collections.Generic;

namespace QsTech.Core.Interface
{
    public class HomeZoneMetadata
    {
        public HomeZoneMetadata()
        {
            RowSpan = RowSpan.Whole;
            ColumnSpan = 1;
            ChannelId = "Home";
            Required = new List<Permission>();
        }
        public int Priority { get; set; }

        public Tuple<string, string> Url { get; set; }

        public Tuple<string, string> MoreUrl { get; set; }

        public RowSpan RowSpan { get; set; }

        public int ColumnSpan { get; set; }

        internal string ModuleId  { get; set; }

        public string Name { get; set; }

        public string ChannelId { get; set; }

        public IList<Permission> Required { get; set; }
    }

    public enum RowSpan
    {
        Whole,
        One,
        Two,
    }
}