using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QsTech.Framework;

namespace QsTech.Core.Interface
{
    public interface IGlobalSettingManager:ITransientDependency
    {
        string GetValue(string category, string name);

        void SetValue(string category, string name,string value);
    }
}
