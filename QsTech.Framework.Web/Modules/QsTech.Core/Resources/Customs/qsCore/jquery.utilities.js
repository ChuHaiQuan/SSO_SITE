if (jQuery) {
    (function ($, undefined) {
        $.fn.extend({
            enterTo: function (selector, eventType) {
                eventType = eventType || 'click';
                return this.each(function () {
                    $(this).bind('keypress.enterTo', function (event) {
                        if (event.keyCode === 13) {
                            if (typeof eventType === 'string') {
                                $(selector).trigger(eventType, arguments);
                            } else if ($.isFunction(eventType)) {
                                eventType(this);
                            }
                        }
                    });
                });
            },
            unEnterTo: function () {
                return this.each(function () {
                    $(this).unbind('.enterTo');
                });
            }
        });
        $.extend({
           jumpTo: function (url) {
                window.location = '' + url;
           }
        });
        $.fn.extend({
            jumpTo: function (url) {
                return this.each(function () {
                    $(this).bind('click.jumpTo', function () { $.jumpTo(url); });
                });
            },
            unjumpTo: function () {
                return this.each(function () {
                    $(this).unbind('.jumpTo');
                });
            }
        });
    })(jQuery);
    /*
    * 基于jquery ;根据时间，显示星期几
    * create 2011/3/31
    * 
    */
    (function ($) {
        var qsDate = {
            getWeekDay: function (datestr) {
                datestr = datestr.replace(/-/g, "/");
                var date = new Date(datestr);
                if (date != "undefined") {
                    var weekDay = date.getDay();
                    return this.parseWeekString(weekDay);
                }
                else
                    return "";
            },
            setSpan: function (sid, datestr) {
                var day = this.getWeekDay(datestr);
                $(sid).text(day);
            },
            parseWeekString: function (d) {
                var keys = ['sunday', 'monday', 'tuesday', 'wednesday', 'thursday', 'friday', 'saturday'];
                return '[' + this[keys[d]] + ']';
            }
        };
        var regional = [];
        regional[''] = {
            sunday: '周日',
            monday: '周一',
            tuesday: '周二',
            wednesday: '周三',
            thursday: '周四',
            firday: '周五',
            saturday: '周六'
        };
        $.extend(qsDate, regional['']);
        $.qsWeekDay = qsDate;
        $.qsWeekDay.setDefaults = function (settings) {
            $.extend(this, settings || {});
        };

        $.fn.qsWeekDay = function (s) {
            this.each(function () {
                $(this).change(function () {
                    var datestr = $(this).val();
                    qsDate.setSpan(s, datestr);
                });
                var datestr = $(this).val();
                qsDate.setSpan(s, datestr);

            });
        };

        $.fn.qsSpanWeekDay = function () {
            this.each(function () {
                var datestr = $(this).text();
                qsDate.setSpan(this, datestr);

            });
        };
    })(jQuery);

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

    /*
    * object对表单赋值
    * create 
    */
    (function ($) {
        var setformval = function (form, data) {
            $('input, select, textarea', form).each(function () {
                var n = this.name, t = this.type, tag = this.tagName.toLowerCase();
                if (typeof data[n] == "undefined") return true;
                var obj = this;
                if (tag == "input" || tag == "textarea") {
                    if (t == "text" || t == "hidden" || t == "textarea") {
                        $(this).val(data[n]);
                    }
                    else if (t == "radio") {
                        if ($(this).val() == data[n])
                            $(this).attr("checked", true);
                        else
                            $(this).attr("checked", false);
                    }
                    else if (t == "checkbox") {
                        var v = this.value;
                        if ($.isArray(data[n])) {
                            $.each(data[n], function (i, cbv) {
                                if (v == cbv)
                                    $(obj).attr("checked", true);
                            });
                        }
                        else {
                            if (v == data[n])
                                $(obj).attr("checked", true);
                        }
                    }
                }
                else if (tag == "select") {
                    $(this).val(data[n]);
                }
            });
            $('span[title=qs],div[title=qs]', form).each(function () {
                var n = this.id;
                if (typeof data[n] == "undefined") return true;
                $(this).html(data[n]);
            });
        }
        $.fn.qsSetForm = function (data) {
            this.each(function () {
                setformval(this, data || {});
            });
        };




        /*===========
        * 模拟表单post提交; add by andy 2012-5-22
        *=========
        */
        $.qsFormSubmit = function (action, p) {
            var cform = document.createElement('form');
            $("body").append(cform);
            $(cform).attr("action", action);
            $(cform).attr("method", "post");
            for (var key in p) {
                $("<input>", {
                    type: 'hidden',
                    val: p[key],
                    name: key
                }).appendTo(cform);
            }
            $(cform).submit();
        }
        /*===========
        * Json转字符串; add by andy 2012-6-8
        *=========
        */
        $.JsonToString = function (s) {
            var arr = [];
                var type = typeof s;
                if ('object' == type) {
                    if (Array == s.constructor)
                        type = 'array';
                    else
                        type = 'object';
                }
                if (type == 'object' && s != null)
                {
                    for (var i in s) arr.push("'" + i + "':" + $.JsonToString(s[i]));
                  return '{' + arr.join(',') + '}';
                }
              else if (type == 'array') {
                    var results = [];
                    for (var i = 0; i < s.length; i++) {
                        var value = $.JsonToString(s[i]);
                        if (value !== undefined) results.push(value);
                    }
                    return '[' + results.join(',') + ']';
                }
                return /^(string)$/.test(typeof s) ? "'" + s + "'" : s;
        }
        })(jQuery);

         /*===========
        * Date格式化显示 add by andy 2012-7-13
        *========= */
        Date.prototype.format = function (format) {
            var o = {
                "M+": this.getMonth() + 1, //month 
                "d+": this.getDate(), //day 
                "h+": this.getHours(), //hour 
                "m+": this.getMinutes(), //minute 
                "s+": this.getSeconds(), //second 
                "q+": Math.floor((this.getMonth() + 3) / 3), //quarter 
                "S": this.getMilliseconds() //millisecond 
            }
            if (/(y+)/.test(format)) {
                format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
            }
            for (var k in o) {
                if (new RegExp("(" + k + ")").test(format)) {
                    format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
                }
            }
            return format;
        } 
}