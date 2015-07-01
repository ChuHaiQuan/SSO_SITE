(function ($, undefined) {
    function MessageBox() {
        this.defaults = {
            confirmTitle: '操作确认',
            alertTitle: '提示'
        };
        this.regional = [];
        this.regional[''] = {
            confirmTitle: '操作确认',
            alertTitle: '提示',
            ex_btn_view: '查看错误详细',
            ex_title_details: '错误详细信息',
            ex_btn_upload: '上传错误报告',
            close: '关闭',
            ok: '确定',
            cancel: '取消'
        }
        $.extend(this.defaults, this.regional['']);
    }
    MessageBox.prototype = {
        setDefaults: function (settings) {
            $.extend(this.defaults, settings);
            return this;
        },
        _get: function (name) {
            return this.defaults[name] !== undefined ?
			    this.defaults[name] : name;
        },
        exception: function (jsonError, title, onClose) {
            var _this = this;
            var div = this._getExeptionHTML(title, jsonError.m);
            var oldFocus = document.activeElement;
            var buttons = [{
                'class': 'yes-button',
                text: _this._get('ex_btn_view'),
                click: function () {
                    var detailDiv = _this._getExeptionHTML(_this._get('ex_title_details'), jsonError.stackTrace);
                    _this.showDialog(detailDiv, [{
                        text: _this._get('close'),
                        click: function () {
                            $(this).dialog("close");
                            return false;
                        }
                    }], null, {
                        width: 600,
                        height: 500
                    });
                    return false;
                }
            }, {
                'class': 'yes-button',
                text: _this._get('ex_btn_upload'),
                click: function () {
                    // TODO: 长传错误跟踪信息。
                    $(this).dialog("close");
                    return false;
                }
            }, {
                'class': 'yes-button',
                text: _this._get('close'),
                click: function () {
                    $(this).dialog("close");
                    return false;
                }
            }];
            this.showDialog(div, buttons, null, {
                width: 400
            });
        },
        _getExeptionHTML: function (title, message) {
            var data = [];
            data.push('<div id="dialog-confirm" class="warning-dialog" title="', title, '">');
            data.push('<p>');
            data.push(message);
            data.push('</p>');
            data.push('</div>');
            return $(data.join(''));
        },
        alert: function (message, title, onClose) {
            if (!title) {
                title = "Error!";
            }
            data = [];
            data.push('<div id="error-message" title="', title, '">');
            if ($.isArray(message)) {
                data.push('<p>');
                data.push(message.join('<p/><p>'));
                data.push('<p>');
            } else {
                data.push(message);
            }
            data.push('</div>');
            var div = $(data.join(''));
            var close = function () {
                div.remove();
                if (onClose) {
                    onClose();
                }
            };
            var dialog = div.dialog({
                modal: true,
                resizable: false,
                dialogClass: 'alert',
                close: close,
                zIndex: 10000
            });
            this._fixDialog(dialog);
        },
        confirm: function (title, message, onYes, onNo) {
            var _this = this;
            var div = this._getConfirmHTML(title, message);
            var oldFocus = document.activeElement;
            var buttons = [{
                'class': 'yes-button',
                text: _this._get('ok'),
                click: function () {
                    onNo = null;
                    onYes();
                    $(this).dialog("close");
                    return false;
                }
            }, {
                'class': 'no-button',
                text: _this._get('cancel'),
                click: function () {
                    $(this).dialog("close");
                    oldFocus.focus();
                    return false;
                }
            }];
            this.showDialog(div, buttons, onNo);
        },
        showDialog: function (div, buttons, onClose, customizations) {
            var configuration = {
                resizable: false,
                modal: true,
                buttons: buttons,
                width: 300,
                height: 'auto',
                open: function () {
                    $('.yes-button').blur();
                    $(buttons[0]).blur();
                    $('.cancel-button, .no-button').focus();
                },
                close: function () {
                    if (onClose) {
                        onClose();
                    }
                    div.dialog('destroy').remove();
                }
            };
            $.extend(configuration, customizations);
            var dialog = div.dialog(configuration);
            this._fixDialog(dialog);
        },
        _getConfirmHTML: function (title, message) {
            var data = [];
            data.push('<div id="dialog-confirm" class="confirmation-dialog" title="', title, '">');
            data.push('<p>');
            if ($.isArray(message)) {
                data.push(message.join('<p/><p>'));
            } else {
                data.push(message);
            }
            data.push('<p>');
            data.push('</div>');
            return $(data.join(''));
        },
        _fixDialog: function (dialog) {
            if ($.browser.msie && $.browser.version <= 7) {
                var widget = dialog.dialog('widget');
                var contentPane = widget.find('.ui-dialog-content');
                dialog.dialog('option', 'width', contentPane.outerWidth());
                dialog.dialog('option', 'position', 'center');
                widget.find('.ui-dialog-titlebar-close').hide().show();
            }
        }
    }
    $.messagebox = new MessageBox();

    window.alert = function (msg) {
        $.messagebox.alert(msg, $.messagebox.defaults.alertTitle);
    };
    //    window.confirm = function (msg) {
    //        $.messagebox.confirm($.messagebox.defaults.confirmTitle, msg);
    //    };
})(jQuery);