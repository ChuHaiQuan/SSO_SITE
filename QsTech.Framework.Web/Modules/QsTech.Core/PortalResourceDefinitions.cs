using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Framework.Mvc.Resources;

namespace QsTech.Core
{
    public class PortalResourceDefinitions : IResourceDefinitionProvider
    {
        public void BuilderResourceDefinitions(ResourceDefinitionBuilder builder)
        {
            builder.Script("JS_jQuery_172", "Scripts/jquery-1.7.2.min.js");

            builder.Style("CSS_Portal_base", "Styles/Portal/base.css");
            builder.Style("CSS_Portal_bs_responsive", "Styles/Portal/bootstrap-responsive.css");
            builder.Style("CSS_Portal_bs", "Styles/Portal/bootstrap.css");
            builder.Style("CSS_Portal_common", "Styles/Portal/common.css");
            builder.Style("CSS_Portal_jq_jqzoom", "Styles/Portal/jquery.jqzoom.css");
            builder.Style("CSS_Portal_adgallery", "Styles/Portal/ad-gallery.css");
            builder.Style("CSS_Portal_smoothness_jq_ui_custom", "Styles/Portal/smoothness/jquery-ui-1.9.1.custom.min.css").SetDebugUrl("Styles/Portal/smoothness/jquery-ui-1.9.1.custom.css");
            builder.Style("CSS_Portal_idialog", "Styles/Portal/skins/idialog.css");
            builder.Style("CSS_Portal_idialog_default", "Styles/Portal/skins/default.css");

            builder.Script("JS_Portal_bs", "Scripts/bootstrap/bootstrap.js");
            builder.Script("JS_Portal_bs_affix", "Scripts/bootstrap/bootstrap-affix.js");
            builder.Script("JS_Portal_bs_alert", "Scripts/bootstrap/bootstrap-alert.js");
            builder.Script("JS_Portal_bs_button", "Scripts/bootstrap/bootstrap-button.js");
            builder.Script("JS_Portal_bs_carousel", "Scripts/bootstrap/bootstrap-carousel.js");
            builder.Script("JS_Portal_bs_collapse", "Scripts/bootstrap/bootstrap-ccollapse.js");
            builder.Script("JS_Portal_bs_dropdown", "Scripts/bootstrap/bootstrap-dropdown.js");
            builder.Script("JS_Portal_bs_modal", "Scripts/bootstrap/bootstrap-modal.js");
            builder.Script("JS_Portal_bs_popover", "Scripts/bootstrap/bootstrap-popover.js");
            builder.Script("JS_Portal_bs_scrollspy", "Scripts/bootstrap/bootstrap-scrollspy.js");
            builder.Script("JS_Portal_bs_tab", "Scripts/bootstrap/bootstrap-tab.js");
            builder.Script("JS_Portal_bs_tooltip", "Scripts/bootstrap/bootstrap-tooltip.js");
            builder.Script("JS_Portal_bs_transition", "Scripts/bootstrap/bootstrap-transition.js");
            builder.Script("JS_Portal_bs_typeahead", "Scripts/bootstrap/bootstrap-typeahead.js");
            builder.Script("JS_scrool", "Scripts/bootstrap/scrool.js");
            builder.Script("JS_jqzoom", "Scripts/bootstrap/jquery.jqzoom-core-pack.js");
            builder.Script("JS_Portal_jQueryDatePickerZHCN", "scripts/jquery.ui.datepicker-zh-CN.js").SetDebugUrl("scripts/jquery.ui.datepicker-zh-CN.js");
            builder.Script("JS_Portal_jQueryTIMEPicker", "scripts/jquery-ui-timepicker-addon.js").SetDebugUrl("scripts/jquery-ui-timepicker-addon.js");
            /***** kindeditor ****/
            builder.Script("JS_Portal_kindeditor", "Resources/kindeditor/kindeditor-min.js").SetDebugUrl("Resources/kindeditor/kindeditor-min.js");
            builder.Script("JS_Portal_ADGallery", "Resources/AD_Gallery/jquery.ad-gallery.min.js").SetDebugUrl("Resources/AD_Gallery/jquery.ad-gallery.js").SetVersion("1.2.7");
            builder.Style("JS_Portal_ADGalleryCss", "Resources/AD_Gallery/jquery.ad-gallery.css").SetDebugUrl("Resources/AD_Gallery/jquery.ad-gallery.css").SetVersion("1.2.7");

            builder.Script("JS_Portal_artDialog", "Scripts/artDialog.min.js");
        
        }
    }
}