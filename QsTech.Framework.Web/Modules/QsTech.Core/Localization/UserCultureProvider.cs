using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Core.Interface.UserProperty;
using QsTech.Framework.Localization;
using QsTech.Framework.WorkContexts;
using QsTech.Core.Interface.Extensions;

namespace QsTech.Core.Localization
{
    public class UserCultureProvider// : ICultureProvider
    {
        private readonly IWorkContextAccessor _accessor;
        private readonly IUserPropertyManager _propertyManager;

        public UserCultureProvider(IWorkContextAccessor accessor, IUserPropertyManager propertyManager)
        {
            _accessor = accessor;
            _propertyManager = propertyManager;
        }

        public int Priority
        {
            get { return 20; }
        }

        public string GetCurrentCulture()
        {
            var ctx = _accessor.GetContext();
            var user = ctx.GetCurnentUser();
            return _propertyManager.GetValue(user, "Lang");
        }

        public bool HasValue()
        {
            var ctx = _accessor.GetContext();
            var user = ctx.GetCurnentUser();
            if (user==null)
            {
                return false;
            }
            return !string.IsNullOrEmpty(_propertyManager.GetValue(user, "Lang"));
        }
    }
}