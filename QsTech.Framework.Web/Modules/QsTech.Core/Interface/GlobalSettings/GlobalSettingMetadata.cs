using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QsTech.Core.Interface
{
    public class GlobalSettingMetadata
    {
        public GlobalSettingMetadata(string category)
        {
            Category = category;
        }
        public  string Category { get; set; }

        public  string Name { get; set; }

        public  string Value { get; set; }

        public  string Description { get; set; }



        public GlobalSettingMetadata SetName(string value)
        {
            this.Value = value;
            return this;
        }
    }
}
