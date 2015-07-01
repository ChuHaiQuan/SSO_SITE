using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Core.Interface;
using QsTech.Framework.Mvc.Resources;

namespace QsTech.Core
{
    public class ResourceDefinitions : IResourceDefinitionProvider
    {
        private readonly IThemeManager _themeManager;
        public ResourceDefinitions(IThemeManager themeManager)
        {
            _themeManager = themeManager;
        }

        public void BuilderResourceDefinitions(ResourceDefinitionBuilder builder)
        {
            builder.Script("jQuery", "scripts/jquery-1.5.1.min.js").SetDebugUrl("scripts/jquery-1.5.1.js");

            builder.Script("jQuery_debug", "scripts/jquery-1.5.1.js").SetDebugUrl("scripts/jquery-1.5.1.js");
            builder.Script("jQueryUI", "scripts/jquery-ui-1.8.11.min.js").SetDebugUrl("scripts/jquery-ui-1.8.11.js")
                .Require("jQuery")
                .Require("default_jQueryAllCss");
                //.Require("default_jQueryAutocompleteCss")
                //.Require("default_jQueryBaseCss")
                //.Require("default_jQueryButtonCss")
                //.Require("default_jQueryCoreCss")
                //.Require("default_jQueryDatepickerCss")
                //.Require("default_jQueryDialogCss")
                //.Require("default_jQueryProgressbarCss")
                //.Require("default_jQueryResizableCss")
                //.Require("default_jQuerySelectableCss")
                //.Require("default_jQuerySliderCss")
                //.Require("default_jQueryTabsCss")
                //.Require("default_jQueryThemeCss");
            builder.Script("jQueryUnobtrusive", "scripts/jquery.unobtrusive-ajax.min.js").SetDebugUrl("scripts/jquery.unobtrusive-ajax.js");
            builder.Script("jQueryValidate", "scripts/jquery.validate.min.js").SetDebugUrl("scripts/jquery.validate.js");
            builder.Script("jQueryValidateUnobtrusive", "scripts/jquery.validate.unobtrusive.min.js").SetDebugUrl("scripts/jquery.validate.unobtrusive.js")
                .Require("jQueryValidate").Require("jQueryUnobtrusive");

            builder.Script("jQueryDatePickerZHCN", "scripts/jquery.ui.datepicker-zh-CN.js").SetDebugUrl("scripts/jquery.ui.datepicker-zh-CN.js");
            builder.Script("jQueryTIMEPicker", "scripts/jquery-ui-timepicker-addon.js").SetDebugUrl("scripts/jquery-ui-timepicker-addon.js");

            builder.Style("base", "Styles/base.css").SetDebugUrl("styles/base.css").SetVersion("201209261410");
            builder.Style("site", "Styles/Site.css").SetDebugUrl("styles/Site.css").SetVersion("201209261410");
            builder.Style("style", "Styles/Style.css").SetDebugUrl("styles/Style.css").SetVersion("201209261410");
            builder.Style("jQueryUICss", "Styles/jquery-ui.css").SetDebugUrl("Styles/jquery-ui.css");
                    
            builder.Style("jQueryAccordionCss", "Styles/Themes/jquery.ui.accordion.css").SetDebugUrl("Styles/Themes/jquery.ui.accordion.css");
            builder.Style("jQueryAllCss", "Styles/Themes/jquery.ui.all.css").SetDebugUrl("Styles/Themes/jquery.ui.all.css");
            builder.Style("jQueryAutocompleteCss", "Styles/Themes/jquery.ui.autocomplete.css").SetDebugUrl("Styles/Themes/jquery.ui.autocomplete.css");
            builder.Style("jQueryBaseCss", "Styles/Themes/jquery.ui.base.css").SetDebugUrl("Styles/Themes/jquery.ui.base.css");
            builder.Style("jQueryButtonCss", "Styles/Themes/jquery.ui.button.css").SetDebugUrl("Styles/Themes/jquery.ui.button.css");
            builder.Style("jQueryCoreCss", "Styles/Themes/jquery.ui.core.css").SetDebugUrl("Styles/Themes/jquery.ui.core.css");
            builder.Style("jQueryDatepickerCss", "Styles/Themes/jquery.ui.datepicker.css").SetDebugUrl("Styles/Themes/jquery.ui.datepicker.css");
            builder.Style("jQueryDialogCss", "Styles/Themes/jquery.ui.dialog.css").SetDebugUrl("Styles/Themes/jquery.ui.dialog.css");
            builder.Style("jQueryProgressbarCss", "Styles/Themes/jquery.ui.progressbar.css").SetDebugUrl("Styles/Themes/jquery.ui.progressbar.css");
            builder.Style("jQueryResizableCss", "Styles/Themes/jquery.ui.resizable.css").SetDebugUrl("Styles/Themes/jquery.ui.resizable.css");
            builder.Style("jQuerySelectableCss", "Styles/Themes/jquery.ui.selectable.css").SetDebugUrl("Styles/Themes/jquery.ui.selectable.css");
            builder.Style("jQuerySliderCss", "Styles/Themes/jquery.ui.slider.css").SetDebugUrl("Styles/Themes/jquery.ui.slider.css");
            builder.Style("jQueryTabsCss", "Styles/Themes/jquery.ui.tabs.css").SetDebugUrl("Styles/Themes/jquery.ui.tabs.css");
            builder.Style("jQueryThemeCss", "Styles/Themes/jquery.ui.theme.css").SetDebugUrl("Styles/Themes/jquery.ui.theme.css");

            /**********************************主题 Default 样式定义**************************************/
            builder.Style("default_base", "Styles/Default/base.css").SetDebugUrl("styles/Default/base.css").SetVersion("201209261410");
            builder.Style("default_layout", "Styles/Default/layout.css").SetDebugUrl("styles/Default/layout.css").SetVersion("201209261410");
            builder.Style("default_site", "Styles/Default/Site.css").SetDebugUrl("styles/Default/Site.css").SetVersion("201209261410");
            builder.Style("default_style", "Styles/Default/Style.css").SetDebugUrl("styles/Default/Style.css").SetVersion("201209261410");
            builder.Style("default_login", "Styles/Default/login.css").SetDebugUrl("styles/Default/login.css").SetVersion("201207302310");
            builder.Style("default_jqueryui", "Styles/Default/jquery-ui.css").SetDebugUrl("Styles/Default/jquery-ui.css");

            builder.Style("default_jQueryAllCss", "Styles/Default/Themes/base/jquery.ui.all.css").SetDebugUrl("Styles/Default/Themes/base/jquery.ui.all.css");
            builder.Style("default_jQueryAccordionCss", "Styles/Default/Themes/base/jquery.ui.accordion.css").SetDebugUrl("Styles/Default/Themes/base/jquery.ui.accordion.css");
            builder.Style("default_jQueryAutocompleteCss", "Styles/Default/Themes/base/jquery.ui.autocomplete.css").SetDebugUrl("Styles/Default/Themes/base/jquery.ui.autocomplete.css");
            builder.Style("default_jQueryBaseCss", "Styles/Default/Themes/base/jquery.ui.base.css").SetDebugUrl("Styles/Default/Themes/base/jquery.ui.base.css");
            builder.Style("default_jQueryButtonCss", "Styles/Default/Themes/base/jquery.ui.button.css").SetDebugUrl("Styles/Default/Themes/base/jquery.ui.button.css");
            builder.Style("default_jQueryCoreCss", "Styles/Default/Themes/base/jquery.ui.core.css").SetDebugUrl("Styles/Default/Themes/base/jquery.ui.core.css");
            builder.Style("default_jQueryDatepickerCss", "Styles/Default/Themes/base/jquery.ui.datepicker.css").SetDebugUrl("Styles/Default/Themes/base/jquery.ui.datepicker.css");
            builder.Style("default_jQueryDialogCss", "Styles/Default/Themes/base/jquery.ui.dialog.css").SetDebugUrl("Styles/Default/Themes/base/jquery.ui.dialog.css");
            builder.Style("default_jQueryProgressbarCss", "Styles/Default/Themes/base/jquery.ui.progressbar.css").SetDebugUrl("Styles/Default/Themes/base/jquery.ui.progressbar.css");
            builder.Style("default_jQueryResizableCss", "Styles/Default/Themes/base/jquery.ui.resizable.css").SetDebugUrl("Styles/Default/Themes/base/jquery.ui.resizable.css");
            builder.Style("default_jQuerySelectableCss", "Styles/Default/Themes/base/jquery.ui.selectable.css").SetDebugUrl("Styles/Default/Themes/base/jquery.ui.selectable.css");
            builder.Style("default_jQuerySliderCss", "Styles/Default/Themes/base/jquery.ui.slider.css").SetDebugUrl("Styles/Default/Themes/base/jquery.ui.slider.css");
            builder.Style("default_jQueryTabsCss", "Styles/Default/Themes/base/jquery.ui.tabs.css").SetDebugUrl("Styles/Default/Themes/base/jquery.ui.tabs.css");
            builder.Style("default_jQueryThemeCss", "Styles/Default/Themes/base/jquery.ui.theme.css").SetDebugUrl("Styles/Default/Themes/base/jquery.ui.theme.css");

            /***********JqGrid ***/
            builder.Script("gridLocale", "Resources/Customs/qsGrid/grid.locale-cn.js").SetDebugUrl("Resources/Customs/qsGrid/grid.locale-cn.js").SetVersion("201209261410");
            builder.Script("jqGrid", "Resources/Customs/qsGrid/jquery.jqGrid.min.js").SetDebugUrl("Resources/Customs/qsGrid/jquery.jqGrid.min.js").SetVersion("201209261410");
            builder.Script("qsGrid", "Resources/Customs/qsGrid/qs.Grid.js").SetDebugUrl("Resources/Customs/qsGrid/qs.Grid.js").SetVersion("201209261410");

            builder.Style("jqGridCss", "Styles/ui.jqgrid.css").SetDebugUrl("styles/ui.jqgrid.css").SetVersion("201209261410");

            builder.Script("global", "Resources/Customs/qsCore/Global.js").SetDebugUrl("Resources/Customs/qsCore/Global.js").SetVersion("201209261410");

            /*****JsonAjax ***/
            builder.Script("ajaxSubmit", "Resources/Customs/qsCore/AjaxSubmit.js").SetDebugUrl("Resources/Customs/qsCore/AjaxSubmit.js").SetVersion("201209241954");
            /*****modalshow ***/
            builder.Script("modalShow", "Resources/Customs/qsCore/qs.modalshow.js").SetVersion("201209052021")
                .SetDebugUrl("Resources/Customs/qsCore/qs.modalshow.js")
                .Require("ajaxSubmit");

            /*****jqueryForm ***/
            builder.Script("jqueryForm", "Scripts/jquery.form.js").SetDebugUrl("Scripts/jquery.form.js").SetVersion("201209261410");
            builder.Script("qsForm", "Resources/Customs/qsCore/qs.ajaxForm.js").SetDebugUrl("Resources/Customs/qsCore/qs.ajaxForm.js").SetVersion("201209261410")
                .Require("ajaxSubmit").Require("jqueryForm");

            /***** kindeditor ****/
            builder.Script("kindeditor", "Resources/kindeditor/kindeditor-min.js").SetDebugUrl("Resources/kindeditor/kindeditor-min.js");


            /***** jquery.utilities.js ****/
            builder.Script("utilities", "Resources/Customs/qsCore/jquery.utilities.js").SetDebugUrl("Resources/Customs/qsCore/jquery.utilities.js");

            /**************imgAreaSelect******************/
            builder.Script("ImageAreaSelector", "Scripts/jquery.imgareaselect.min.js").SetDebugUrl("Scripts/jquery.imgareaselect.js").SetVersion("201209261410");
            builder.Style("ImageAreaSelectorCss", "Styles/Default/imgareaselect/imgareaselect-default.css");
            /******************qs.statusbar.js*************/
            builder.Script("StatusBar", "Resources/Customs/qsCore/qs.statusbar.js").SetDebugUrl("Resources/Customs/qsCore/qs.statusbar.js").SetVersion("201209261410");
            /*************** qs.listDataLoader  *******/
            builder.Script("listDataLoader", "Resources/Customs/qsCore/qs.listDataLoader.js").SetDebugUrl("Resources/Customs/qsCore/qs.listDataLoader.js").SetVersion("201209261410");
            /***********************异常消息处理、消息提示************************/
            builder.Script("MessageBox", "Resources/Customs/qsCore/qs.messagebox.js").SetDebugUrl("Resources/Customs/qsCore/qs.messagebox.js").SetVersion("201209052021");
            builder.Script("Notifier", "Resources/Customs/qsCore/qs.notify.js").SetDebugUrl("Resources/Customs/qsCore/qs.notify.js").SetVersion("201209052021");
        
            /**************************************************/
            builder.Style("Base-New", "Resources/Default/Css/base.css").SetDebugUrl("Resources/Default/Css/base.css").SetVersion("201209261410");
            builder.Style("Tpl-New", "Resources/Default/Css/tpl.css").SetDebugUrl("Resources/Default/Css/tpl.css").SetVersion("201209261410");
            builder.Style("Layout-New", "Resources/Default/Css/layout.css").SetDebugUrl("Resources/Default/Css/layout.css").SetVersion("201209261410");
            builder.Style("Sl-New", "Resources/Default/Css/sl.css").SetDebugUrl("Resources/Default/Css/sl.css").SetVersion("201209261410");

            builder.Script("HighCharts", "Scripts/highcharts.js").SetDebugUrl("Scripts/highcharts.src.js");
            /*************************jquery lightbox plugin***************************/
            builder.Style("LightBoxCss", "Resources/lightbox/css/jquery.lightbox.css").SetDebugUrl("Resources/lightbox/css/jquery.lightbox.css");
            builder.Script("LightBox", "Resources/lightbox/jquery.lightbox.min.js").SetDebugUrl("Resources/lightbox/jquery.lightbox.js");
            /*************************jquery miniColors plugin*************************/
            builder.Style("MiniColorsCss", "Resources/miniColors/jquery.miniColors.css").SetDebugUrl("Resources/miniColors/jquery.miniColors.css").SetVersion("201207261612");
            builder.Script("MiniColors", "Resources/miniColors/jquery.miniColors.min.js").SetDebugUrl("Resources/miniColors/jquery.miniColors.js").SetVersion("201207261609");
            /*************************jquery AD_Gallery plugin************************/
            builder.Style("ADGalleryCss", "Resources/AD_Gallery/jquery.ad-gallery.css").SetDebugUrl("Resources/AD_Gallery/jquery.ad-gallery.css").SetVersion("1.2.7");
            builder.Script("ADGallery", "Resources/AD_Gallery/jquery.ad-gallery.min.js").SetDebugUrl("Resources/AD_Gallery/jquery.ad-gallery.js").SetVersion("1.2.7");

            builder.Style("treegrid", "Resources/treegrid/treegrid.css");

            builder.Script("bgiframe", "Scripts/jquery.bgiframe.js").SetDebugUrl("Scripts/jquery.bgiframe.js").SetVersion("2.1.2");

            /************************* 登录页面 ************************/
            builder.Style("login_base", "Styles/Default/login_base.css").SetDebugUrl("styles/Default/login_base.css").SetVersion("201209261410");
            builder.Style("login_layout", "Styles/Default/login_common.css").SetDebugUrl("styles/Default/login_common.css").SetVersion("201209261410");

            builder.Style("CSS_treeview", "Resources/treeview/jquery.treeview.css");
            builder.Script("JS_treeview", "Resources/treeview/jquery.treeview.js");
            builder.Script("JS_treeview_edit", "Resources/treeview/jquery.treeview.edit.js");
            builder.Script("JS_treeview_async", "Resources/treeview/jquery.treeview.async.js");
            builder.Script("JS_treeview_sortable", "Resources/treeview/jquery.treeview.sortable.js");

            builder.Style("CSS_ztree", "Resources/zTree/zTreeStyle.css");
            builder.Script("JS_ztree", "Resources/zTree/jquery.ztree.core-3.5.js");

            builder.Style("CSS_marquee", "Resources/marquee/jquery.marquee.min.css").SetDebugUrl("Resources/marquee/jquery.marquee.css");
            builder.Script("JS_marquee", "Resources/marquee/jquery.marquee.min.js").SetDebugUrl("Resources/marquee/jquery.marquee.js");


            builder.Style("theme", _themeManager.ThemeCss).SetDebugUrl(_themeManager.ThemeCss);
        }
    }
}