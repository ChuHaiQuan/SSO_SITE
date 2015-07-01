/*
* 多选显示框
* Date：2011/5/23
**/
(function ($) {
    Array.prototype.remove = function (obj) {
        for (var i = 0; i < this.length; ++i) {
            if (this[i] == obj) {
                if (i > this.length / 2) {
                    for (var j = i; j < this.length - 1; ++j) {
                        this[j] = this[j + 1];
                    }
                    this.pop();
                }
                else {
                    for (var j = i; j > 0; --j) {
                        this[j] = this[j - 1];
                    }
                    this.shift();
                }
                break;
            }
        }
    };
    var selectorBox = function (t, p) {
        if (t.selector) return false; //如果已经存在,则返回
        p = $.extend({
            height: 23,
            width: 300,
            valueTxtId: '',
            nameArray: [],
            idArray: [],
            splitString: ','
        }, p);
        var nodeArray = new Array(), idArray = new Array();
        var g = {
            addNode: function () {
            },
            addData: function (names, ids, splits) {
                var splitstr = splits;
                if (!splitstr)
                    splitstr = p.splitString;
                var newNames = [], newIds = [];
                if ($.isArray(names)) newNames = names;
                else newNames = names.split(splitstr);
                if ($.isArray(ids)) newIds = ids;
                else newIds = ids.split(splitstr);
               
                $(newNames).each(function (i, n) {
                    var newId = newIds[i];
                    var idexist = t.selector.isExist(newId);
                    if (idexist) return true;
                    var li = document.createElement('li'), innerhtml = '';
                    innerhtml = "<span title='" + n + "' class='outerbox_person'>" + n + "</span>";
                    li.innerHTML = innerhtml;
                    var span = document.createElement('span');
                    span.innerHTML = "×";
                    $("ul", t).append(li);
                    $("span", li).append(span);
                    idArray.push(newId);
                    t.selector.setvalueTxt();
                    $(span).bind("click", { sender: li, id: newId }, t.selector.removeNode);
                });
            },
            removeNode: function (event) {
                $(event.data.sender, t).remove();
                idArray.remove(event.data.id);
                t.selector.setvalueTxt();
            },
            setvalueTxt: function () {
                var txtId = "#" + p.valueTxtId;
                if (txtId != "#" && (txtId).length > 0) {
                    $(txtId).val(idArray.join(p.splitString));
                }
            },
            isExist: function (id) {
                var Atindex = $.inArray(id, idArray);
                if (Atindex != -1) return true;
                else return false;
            },
            init: function () {
                var ul = document.createElement('ul');
                ul.id = t.id + "_item";
                $(ul).addClass('item_list');
                $(t).append(ul);
            }

        };
        t.selector = g;
        t.p = p;
        g.init();
    };
    $.fn.qsSelectorBox = function (p) {
        return this.each(function () {
            selectorBox(this, p);
        });
    }
    $.fn.addQsBox = function (names, ids, split) {
        return this.each(function () {
            if (this.selector) this.selector.addData(names, ids, split);
        });
    };
})(jQuery);