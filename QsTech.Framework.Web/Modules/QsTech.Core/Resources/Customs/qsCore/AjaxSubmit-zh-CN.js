;(function ($) {
    $.ajaxSubmit.regional['zh-CN'] = {
        typedTexts: {
            loading: '数据正在加载，请稍等...',
            save: '数据正在保存，请稍等...',
            submit: '数据正在提交，请稍等...',
            remove: '数据正在删除，请稍等...'
        }
    };
    $.ajaxSubmit.setDefaults($.ajaxSubmit.regional['zh-CN']);
})(jQuery);