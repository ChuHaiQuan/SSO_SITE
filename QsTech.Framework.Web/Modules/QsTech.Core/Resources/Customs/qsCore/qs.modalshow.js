/*******************
客户选择界面
需要jquery ui 
JsonSubmit
**************** */
(function ($) { $.fn.bgIframe = $.fn.bgiframe = function (s) { if ($.browser.msie && /6.0/.test(navigator.userAgent)) { s = $.extend({ top: 'auto', left: 'auto', width: 'auto', height: 'auto', opacity: true, src: 'javascript:false;' }, s || {}); var prop = function (n) { return n && n.constructor == Number ? n + 'px' : n; }, html = '<iframe class="bgiframe"frameborder="0"tabindex="-1"src="' + s.src + '"' + 'style="display:block;position:absolute;z-index:-1;' + (s.opacity !== false ? 'filter:Alpha(Opacity=\'0\');' : '') + 'top:' + (s.top == 'auto' ? 'expression(((parseInt(this.parentNode.currentStyle.borderTopWidth)||0)*-1)+\'px\')' : prop(s.top)) + ';' + 'left:' + (s.left == 'auto' ? 'expression(((parseInt(this.parentNode.currentStyle.borderLeftWidth)||0)*-1)+\'px\')' : prop(s.left)) + ';' + 'width:' + (s.width == 'auto' ? 'expression(this.parentNode.offsetWidth+\'px\')' : prop(s.width)) + ';' + 'height:' + (s.height == 'auto' ? 'expression(this.parentNode.offsetHeight+\'px\')' : prop(s.height)) + ';' + '"/>'; return this.each(function () { if ($('> iframe.bgiframe', this).length == 0) this.insertBefore(document.createElement(html), this.firstChild); }); } return this; }; })(jQuery);
(function ($) {
    var addselectModal = function (t, p) {
        if (t.modaldiv) return false;
        p = $.extend({
            height: 'auto',
            width: "auto", //auto width,
            maxHeight: 'false',
            maxWidth: 'false',
            url: '',
            //title: '',
            data: {},
            method: 'get',
            dataType: 'json',
            confirmHandler: function () { },
            beforeShow: function () { return true; },
            afterShow: function () { }, //显示模态窗口后的执行function
            loadSuccess: function () { },
            loadComplete: function () { },
            cancelHandler: function () { },
            reLoad: true
        }, p);
        var modalLoaded = false;

        var showDialog = function () {
//            var vtitle = $(t).attr("title");
//            if (p.title != "")
//                vtitle = p.title;
           $(t).parent().bgiframe();
            $(t).dialog({
                height: p.height,
                width: p.width,
                maxHeight: p.maxHeight,
                maxWidth: p.maxWidth,
                modal: true,
                //title: vtitle,
                resizable: false,
                close: function () {
                    modalConfirm.outStack();
                    modalCancel.outStack();
                }
            });
            modalConfirm.addListener(p.confirmHandler, p.scope);
            modalCancel.addListener(p.cancelHandler, p.scope);
        }
        var div = {
            show: function () {
                if (!p.beforeShow()) return;
                if (p.reLoad)
                    modalLoaded = false;
                if (!modalLoaded) {
                    $(t).empty();
                    $.AjaxSubmit({ url: p.url, data: p.data,type:p.method,success: function (data) {
                        if (data != "") {
                            modalLoaded = true;
                            $(t).append(data);
                            showDialog();
                            p.loadSuccess();
                        }
                        p.loadComplete();
                    }});
                }
                else {
                    showDialog();
                    p.loadComplete();
                }
            },
            hide: function () {
                 $(t).empty();
                $(t).dialog("close");
            }
        }
        t.p = p;
        t.modaldiv = div;
    };

    var docloaded = false;
    $(function () { docloaded = true });
    var selectModal = function (divmodal, p) {
        addselectModal(divmodal, p);
    };

    $.fn.modalShow = function (data) {
        this.each(function () {
            if (this.p) {
                $.extend(this.p, data);
            }
            else
                selectModal(this, data);
            this.modaldiv.show();
        });
    };

    $.fn.modalHide = function (data) {
        this.each(function () {
            if (this.p) {
                $.extend(this.p, data);
            }
            else
                selectModal(this, data);
            this.modaldiv.hide();
        });
    }

})(jQuery);
