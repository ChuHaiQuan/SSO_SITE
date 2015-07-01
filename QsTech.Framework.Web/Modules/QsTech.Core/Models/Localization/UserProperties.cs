using System;
using System.Collections.Generic;
using QsTech.Core.Interface.UserProperty;

namespace QsTech.Core.Models.Localization
{
    public class UserProperties //: IPropertyMetadataProvider
    {
        public static readonly PropertyMetadata Localization = new PropertyMetadata().Named("Localization").Describe("本地化");

        public string Name
        {
            get { return "常规"; }
        }

        public int Prority
        {
            get { return 10; }
        }

        public System.Tuple<string, string> UiAction
        {
            get { return new Tuple<string, string>("Setting", "Display"); }
        }

        public IEnumerable<PropertyMetadata> GetProperties()
        {
            yield return Localization;
        }
    }
}