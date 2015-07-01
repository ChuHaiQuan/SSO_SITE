/******************* 
数据加载
**************** */
(function ($) {

    var dataLoader = function (option, t) {
        // return this.each(function () {
        if (t.p) { return; }
        var p = $.extend(true, {
            url: "",
            height: 150,
            page: 1,
            rowNum: 20,
            records: 0,
            loadtext: "",
            datatype: "json",
            mtype: "get",
            postData: {},
            loadtext: '数据加载中...',
            loadui: "block"
        }, option);


        t.p = p;
        var gv = $("<div class='ui-jqgrid-view'></div>"), ii,
		isMSIE = $.browser.msie ? true : false,
		isSafari = $.browser.safari ? true : false,
        ts = t;
        $(gv).insertBefore(ts);
        $(ts).appendTo(gv).removeClass("scroll");

        var eg = $("<div class='ui-jqgrid ui-widget ui-widget-content ui-corner-all'></div>");
        $(eg).insertBefore(gv).attr({ "id": "gbox_" + ts.id });
        $(gv).appendTo(eg).attr("id", "gview_" + ts.id);
        if (isMSIE && $.browser.version <= 6) {
            ii = '<iframe style="display:block;position:absolute;z-index:-1;filter:Alpha(Opacity=\'0\');" src="javascript:false;"></iframe>';
        } else { ii = ""; }
        $("<div class='ui-widget-overlay jqgrid-overlay' id='lui_" + ts.id + "'></div>").append(ii).insertBefore(gv);
        $("<div class='loading ui-state-default ui-state-active' id='load_" + ts.id + "'>" + ts.p.loadtext + "</div>").insertBefore(gv);

        var populate = function (npage) {
            var prm = {}, dt, dstr, pN = ts.p.prmNames; ;
            //if (pN.search !== null) prm[pN.search] = ts.p.search; if (pN.nd != null) prm[pN.nd] = new Date().getTime();
            //if (pN.rows !== null) prm[pN.rows] = ts.p.rowNum; if (pN.page !== null) prm[pN.page] = ts.p.page;
            //if (pN.sort !== null) prm[pN.sort] = ts.p.sortname; if (pN.order !== null) prm[pN.order] = ts.p.sortorder;
//            npage = npage || 1;
//            if (npage > 1) {
//                if (pN.npage != null) {
//                    prm[pN.npage] = npage;
//                    adjust = npage - 1;
//                    npage = 1;
//                } else {
//                    lc = function (req) {
//                        if (lcf) {
//                            ts.p.loadComplete.call(ts, req);
//                        }
//                        ts.grid.hDiv.loading = false;
//                        ts.p.page++;
//                        populate(npage - 1);
//                    }
//                }
//            }
            dt = ts.p.datatype.toLowerCase();
            $.ajax($.extend({
                url: ts.p.url,
                type: ts.p.mtype,
                //dataType: dt,
                data: ts.p.postData,
                complete: function (req, st) {
                    if (st == "success" || (req.statusText == "OK" && req.status == "200")) {
                        $(ts).empty().append(req.responseText);
                    }
                    req = null;
                    endReq();
                },
                error: function (xhr, st, err) {
                    if ($.isFunction(ts.p.loadError)) ts.p.loadError.call(ts, xhr, st, err);
                    endReq();
                    xhr = null;
                },
                beforeSend: function (xhr) {
                    beginReq();
                    if ($.isFunction(ts.p.loadBeforeSend)) ts.p.loadBeforeSend.call(this, xhr);
                }
            }, ts.p.ajaxGridOptions));
        },
              beginReq = function () {
                  //ts.p.loadui = true;
                  switch (ts.p.loadui) {
                      case "disable":
                          break;
                      case "enable":
                          $("#load_" + ts.id).show();
                          break;
                      case "block":
                          $("#lui_" + ts.id).show();
                          $("#load_" + ts.id).show();
                          break;
                  }
              },
		endReq = function () {
		   // ts.p.loadui = false;
		    switch (ts.p.loadui) {
		        case "disable":
		            break;
		        case "enable":
		            $("#load_" + ts.id).hide();
		            break;
		        case "block":
		            $("#lui_" + ts.id).hide();
		            $("#load_" + ts.id).hide();
		            break;
		    }
		};

        // });
        t.loader = populate;
    }

    $.fn.dataLoader = function (data) {
        this.each(function () {
            if (this.p) {
                $.extend(this.p, data);
            }
            else
                dataLoader(data, this);
            this.loader();
        });
    };


})(jQuery);
