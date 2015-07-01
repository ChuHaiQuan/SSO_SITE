(function ($) {
    function QsAjaxSubmit() {
        this.regional = []; // Available regional settings, indexed by language code
        this.regional[''] = {
            // Default regional settings
            typedTexts: {
                loading: '数据加载中，请稍后...',
                save: '数据正在保存，请稍候...',
                submit: '数据正在提交，请稍候...',
                remove: '数据正在删除，请稍后...'
            }
        };
        this._defaults = {
            instance: true,     // 解决 ajax bug: 137633
            type: 'POST',
            beforeSubmit: function () { },
            beforeSend: function () { },
            success: function () { }
            //error: function () { },
        };
        this.initialized = false;
        this.overlayDiv = $("<div></div>");
        $.extend(this._defaults, this.regional['']);
    }
    $.extend(QsAjaxSubmit.prototype, {
        setDefaults: function (settings) {
            extendRemove(this._defaults, settings || {});
            return this;
        },
        init: function (settings) {
            if (this._customizations !== settings) {
                this._customizations = settings;
                this.settings = $.extend({}, this._defaults || {}, settings || {});
            }
            if (!this.initialized) {
                this._overlay = (this.overlay())().create();
                this.initialized = true;
            }
            return this;
        },
        exec: function () {
            var self = this,
                settings = self.settings,
                overlay = self._overlay;

            var ajaxOptions = {
                beforeSend: function () {
                    if (false === settings.beforeSend()) return false;
                    overlay.open();
                },
                complete: function (xhr) {
                    if (settings.instance) {
                        overlay.destroy();
                    } else {
                        overlay.close();
                    }
                }
            };
            if (settings.data) {
                if (typeof (settings.data) === 'string') {
                    settings.data += "&nd=" + new Date().getTime();
                } else {
                    settings.data.nd = new Date().getTime();
                }
            }
            var tipsType = self._get(self, 'tipType');
            if (tipsType && (tipsType === 'save' || tipsType === 'submit' || tipsType === 'delete')) {
                settings.type = "POST";
            } else {
                settings.type = "GET";
            }
            ajaxOptions = $.extend({}, settings || {}, ajaxOptions || {});
            if (!settings.form) {
                $.ajax(ajaxOptions);
            } else {
                // supported ajaxForm.
                $(arguments[0]).ajaxForm(ajaxOptions);
            }
        },
        _get: function (inst, name) {
            return inst.settings[name] !== undefined ?
			    inst.settings[name] : this._defaults[name];
        },
        overlay: function () {
            var self = this,
                dialog = self.overlayDiv,
                tipsType = self._get(self, 'tipType'),
                typedTexts = self._get(self, 'typedTexts'),
                currentTypedText = typedTexts[tipsType] || typedTexts['loading'];

            return function () {
                function Overlay(d, t) {
                    this.$el = d;
                    this.text = t;
                }
                $.extend(Overlay.prototype, {
                    create: function () {
                        this.$el.html('<p>' + this.text + '</p>').appendTo(document.body);
                        this.$el.dialog({
                            minHeight: 120,
                            modal: true,
                            resizable: false,
                            closeText: false,
                            autoOpen: false
                        });
                        return this;
                    },
                    open: function () {
                        this.$el.dialog('open');
                    },
                    close: function () {
                        this.$el.dialog('close');
                    },
                    destroy: function () {
                        this.$el.dialog('destroy');
                        this.$el.remove();
                    }
                });
                return new Overlay(dialog, currentTypedText);
            };
        }
    });

    $.fn.qsAjaxForm = function (settings) {
        this.each(function () {
            if (settings.validate) {
                this.validate(settings.validate);
            }
            settings.form = true;
            settings.tipType = 'submit';
            var aser = new QsAjaxSubmit();
            aser.init(settings).exec(this);
            return aser;
        });
    };

    $.qsAjaxSubmit = new QsAjaxSubmit();

    $.AjaxSubmit = function (settings) {
        settings.instance = true;
        if (settings.instance) {
            var inst = new QsAjaxSubmit();
            inst.init(settings).exec();
            return inst;
        }
        $.qsAjaxSubmit.init(settings).exec();
    }
    /* jQuery extend now ignores nulls! */
    function extendRemove(target, props) {
        $.extend(target, props);
        for (var name in props)
            if (props[name] == null || props[name] == undefined)
                target[name] = props[name];
        return target;
    };
    //    $.getAjaxFormOptions = function (p) {
    //        var def_options = {
    //            type: 'post',
    //            dialogDiv: '#gl_dialog_modal',
    //            beforeSubmit: null,
    //            beforeSend: function () { },
    //            success: function (data) { },
    //            error: function (data) { },
    //            tips: "正在保存,请稍等...",
    //            data: {}//,
    //            //custom_error: false
    //        }//end def_options
    //        $.extend(def_options, p || {});
    //        var qs_ajax_options = {
    //            type: def_options.type,
    //            //dataType: def_options.dataType,
    //            data: def_options.data,
    //            beforeSubmit: def_options.beforeSubmit,
    //            beforeSend: function () {
    //                if (false === def_options.beforeSend()) return false;
    //                $(def_options.dialogDiv).html("<div></div><div>" + def_options.tips + "</div>");
    //                $(def_options.dialogDiv).dialog({ height: 100, modal: true, resizable: false, closeText: false });
    //            },
    //            success: function (data) {
    //                def_options.success(data);
    //            },
    //            complete: function (xhr) {
    //                $(def_options.dialogDiv).dialog('close');
    //                $(def_options.dialogDiv).html("<div></div><div>数据加载中,请稍等...</div>");
    //            },
    //            error: function (data) {
    //                $(def_options.dialogDiv).dialog('close');
    //                $(def_options.dialogDiv).html("<div></div><div>数据加载中,请稍等...</div>");
    //                if (!def_options.custom_error) {
    //                    alert(data.responseText);
    //                }
    //                def_options.error(data);
    //            }
    //        }
    //        return qs_ajax_options;
    //    } //end getAjaxFormOptions

    //    //简化调用ajax，并使用进度条
    //    $.AjaxSubmit = function (p) {
    //        var tips = "数据加载中,请稍等...";
    //        if (p.tipType) {
    //            if (p.tipType == "save") {
    //                tips = "正在保存中,请稍等...";
    //                p.type = 'post';
    //            }
    //            else if (p.tipType == "submit") {
    //                tips = "正在提交中,请稍等...";
    //                p.type = 'post';
    //            }
    //            else if (p.tipType == "delete") {
    //                tips = "正在处理中,请稍等...";
    //                p.type = 'post';
    //            }
    //        }
    //        p = $.extend({ "tips": tips }, p);
    //        var qsparm = $.getAjaxFormOptions(p);
    //        qsparm = $.extend(qsparm, { "url": p.url }, p.type || { type: 'get' });
    //        qsparm.data.nd = new Date().getTime();
    //        $.ajax(qsparm);
    //    }; //end JsonAjax
})(jQuery);