using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QsTech.Framework;

namespace QsTech.Core.Interface
{
    public interface IThemeManager:ISingletonDependency
    {
        /// <summary>
        /// loggo
        /// </summary>
        string Title { get; }

        string BottomDescription { get; }

        /// <summary>
        /// 
        /// </summary>
        string ThemeCss { get; }

    }



    [Feature("Red")]
    public class RedTheme : IThemeManager
    {
        private readonly ICoreSetting _coreSetting;

        public RedTheme(ICoreSetting coreSetting)
        {
            _coreSetting = coreSetting;
        }

        public string Title {
            get { return _coreSetting.Title; }
        }

        public string BottomDescription { get { return _coreSetting.BottomDescription; } }

        public string ThemeCss {
            get
            {
                return "Styles/base_r.css";
            }
        }
    }

    [Feature("Blue")]
    public class BlueTheme : IThemeManager
    {
        private readonly ICoreSetting _coreSetting;

        public BlueTheme(ICoreSetting coreSetting)
        {
            _coreSetting = coreSetting;
        }

        public string Title
        {
            get { return _coreSetting.Title; }
        }

        public string BottomDescription { get { return _coreSetting.BottomDescription; } }

        public string ThemeCss
        {
            get
            {
                return "Styles/base_b.css";
            }
        }
    }


}
