/*
 * 帮助方法
 * create 2011/4/1
*/
(function ($) {
    var g = {
        searchText: function (t) {
            $(t).focus(function () {
                var txt_value = $(this).val();
                if (txt_value == this.defaultValue) {
                    $(this).val("");
                }
            });
            $(t).blur(function () {
                if (this.value == "") {
                    $(this).val(this.defaultValue);
                }
            });
        },
        toolBarLink: function (t) {
            $(t).click(function () {
                $(this).addClass("current").siblings().removeClass("current");
            })
        }
    };

    $.fn.qsSearchText = function (p) {
        this.each(function () {
            g.searchText(this);
        });
    };
    $.fn.qsToolBarLink = function (p) {
        this.each(function () {
            g.toolBarLink(this);
        });
    };
})(jQuery);