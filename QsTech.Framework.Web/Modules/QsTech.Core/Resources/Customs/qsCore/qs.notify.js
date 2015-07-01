(function ($, undefined) {
    function Notify() {
        this._defaults = {

        };
        this.regional = [];
        this.regional[''] = {
            boxTitle: '操作过程中发生错误',
            boxContentCaption: '错误消息',
            boxContentCaption2: '错误详细信息'
        };
        $.extend(this._defaults, this.regional['']);
    }

    Notify.prototype = {
        setDefaults: function (settings) {
            $.extend(this._defaults, settings || {});
            return this;
        },
        _get: function(name) {
            return this._defaults[name] !== undefined ?
			    this._defaults[name] : name;
        },
        exception: function (jsonError) {
            if ($.messagebox && $.messagebox.exception) {
                $.messagebox.exception(jsonError, this._get('boxTitle'));
            } else {
                var data = [];
                data.push(this._get('boxContentCaption') + '：');
                data.push(jsonError.m);
                data.push(this._get('boxContentCaption2') + '：');
                data.push(jsonError.stackTrace);
                window.alert(data.join(''));
            }
        }
    };
    $.notifier = new Notify();
})(jQuery);
(function ($, undefined) {
    $.ajaxSetup({
        error: function (xhr, textStatus) {
            var s = xhr.responseText,
                jsonError;
            try {
                jsonError = $.parseJSON(s);
            } catch(e) {
                jsonError = {
                    m: s,
                    stackTrace: s
                };
            }
            $.notifier.exception(jsonError);
        }
    });
})(jQuery);