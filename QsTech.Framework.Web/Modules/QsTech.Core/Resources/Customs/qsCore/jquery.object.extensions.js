/*
 * jQuery 对象扩展
 * author: zhujianjun
 */
if (jQuery) {
    (function ($, undefined) {
        /*
        * 根据传入的数组对象，转换为数组字符串
        * e.g. ['a', 'b'...]
        */
        $.extend({
            toArrayString: function (array) {
                if (typeof (array) == 'undefined')
                    return null;
                if ($.isArray(array) && array.length > 0) {
                    var target = array;
                    $.each(array, function (i, n) {
                        target[i] = "'" + n + "'";
                    });
                    return "[" + target.join(',') + "]";
                }
                return null;
            }
        });
    })(jQuery);
}