using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Core.Interface;
using QsTech.Framework.Data;

namespace QsTech.Core.Models
{
    public class GlobalSettingManager:IGlobalSettingManager
    {
        private readonly IRepository<GlobalSetting> _repGlobalSetting;
        public GlobalSettingManager(IRepository<GlobalSetting> repGlobalSetting)
        {
            _repGlobalSetting = repGlobalSetting;
        }
        public string GetValue(string category, string name)
        {
            var global=_repGlobalSetting.Fetch(m => m.Category == category && m.Name == name).SingleOrDefault();
            if (global != null) return global.Value;
            return string.Empty;
        }

        public void SetValue(string category, string name, string value)
        {
            var val = GetValue(category, name);
            if (val == string.Empty)
                Create(category, name, val);
            else
                Update(category,name,val);
        }

        private void Create(string category, string name, string value)
        { 
        
        }

        private void Update(string category, string name, string value)
        { 
        
        }
    }
}