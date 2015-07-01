/*
* qsGrid;
* Date: 2011-3-5
*/
(function ($) {
    var setqsGrid = function (t, p) {
        var defp = {
            delActionSplitToLast: true,     // 删除列拆分到最后
            gridType: 'inplace', //grid的类型
            readonly: false, //是否只读
            datatype: "json",
            mtype: "get",
            colNames: [],
            colModel: [],
            rowNum: 10,
            rowList: [10, 20, 30],
            viewrecords: true,
            jsonReader: { repeatitems: false },
            autowidth: true,
            shrinkToFit: false,
            height: '100%',
            mainList: false,
            //height: '280px',
            minHeight: 60,
            maxHeight: 280,
            multiselect: false,
            rownumbers: true,
            saveurl: '',
            addurl: '',
            addData: function (id, rowData) { return {}; },
            saveData: function (id, rowData) { return {}; },
            saveSuccess: function (id, rowData) { },
            delurl: '', //删除
            delmessage: '确定要删除当前行吗?',
            delSuccess: function (id, rowData) { },
            delData: function (id, rowData) { return {}; },
            key: 'ID',
            loadComplete: function () { },
            gridComplete: function () { },
            ondblClickRow: function (id) { },
            onSelectRow: function (id) { },
            cellEdit: false,
            cellsubmit: 'remote',
            cellurl: '',
            beforeSubmit: function (rowid, rowData, cellname, value) { return {}; },
            //rowid:当前行；rowData：当前行数据,cellname；编辑单位name，value:当前值
            beforeSave: function (rowid, rowData) { return true; },
            aferSave: function (rowid, rowData, cellname, value) { },
            formatCell: function (rowid, cellname, value, iRow, iCol) { },
            onSelectCell: function (rowid, cellname, value, iRow, iCol) { },
            showEditBtn: function (rowid, rowData) { return true; }, //是否显示编辑按钮
            isEdit: false,
            trColor: function (rowid, rowData) { return ""; },
            footerrow: false,
            userDataOnFooter: false,
            afterEditCell: function (rowid, cellname, value, iRow, iCol) { },
            showDefaultDeleteBtn: function (rowid, rowData) { return true; },
			showDefaultEditBtn: function (rowid, rowData) { return true; },
			showDefaultCancelBtn: function (rowid, rowData) { return true; },
			showDefaultSaveBtn: function (rowid, rowData) { return true; }
        };
        $.extend(defp, p);

        var gridName = t.id;
        var colNames = [], colUrl = []; //urlData={url:'',target:'',col;'ID',param:''}
        var colKey;
        if (!defp.readonly && defp.gridType != "cell" && defp.gridType != "list") {
            var act = { head: '操作', name: 'act', width: 50, align: 'center' };
            var gridmethod = { head: 'm', name: 'qsgridmethod', hidden: true };
            defp.colModel.unshift(act);
            defp.colModel.push(gridmethod);
            if (defp.delActionSplitToLast) {
                defp.colModel.push({
                    head: '操作', name: 'del_act', width: 30, align: 'center'
                });
            }
        }

        var newRowData = defp.newData || {};
        var btnArray = new Array(), colorArray = new Array(); //按钮数组;颜色数组
        $.each(defp.colModel, function (i, n) {
            n.sortable = false;
            colNames.push(n.head);
            if (n.name != 'act' && n.name != 'del_act')
                newRowData[n.name] = '';
            if (n.urlData) {
                var urld = { name: n.name, url: '', col: 'ID', colparam: '', param: '' };
                urld = $.extend(urld, n.urlData);
                colUrl.push(urld);
            }
            if (defp.gridType == "cell" && n.qskey) {
                colKey = n.name;
            }
            if (n.qsButton) {
                if (!defp.readonly)//btnArray.push(n);  //text:'',type:'',click:,showFn
                    btnArray.push(n);
            }
            if (n.qsColor) {
                if (!defp.readonly)
                    colorArray.push(n);
            }
        });
        defp.colNames = colNames;

        if (defp.gridType == "cell" && !defp.readonly) {
            defp.cellEdit = true;
        }
        if (defp.mainList) {//外面的列表界面,显示25条 
            defp.rowNum = 25;
            defp.rowList = [25, 50];
            defp.height = 620;
        }
        if (defp.onlyShowPageInfo) {
            defp.rowNum = 1000;
            $.extend(defp, {
                pgbuttons: false,
                pgtext: false,
                pginput: false,
                rowList: []
            });
        }
        function getBtnName(id, tp) {
            return gridName + "_" + tp + "e_" + id;
        }
        function getBtnId(id, tp) {
            return gridName + "_" + tp + "e_" + id;
        }
        function setBtnshow(id, tp) {
            var d_btid = getBtnName(id, 'd');
            var e_btid = getBtnName(id, 'e');
            var s_btid = getBtnName(id, 's');
            var c_btid = getBtnName(id, 'c');
            if (tp == 'edit') {
                $("#" + d_btid).hide(); $("#" + e_btid).hide();
                $("#" + s_btid).show(); $("#" + c_btid).show();
            }
            else if (tp == "cancel") {
                $("#" + d_btid).show(); $("#" + e_btid).show();
                $("#" + s_btid).hide(); $("#" + c_btid).hide();
            }
        }

        function setGridHeight() {
            if (defp.height != "100%")
                return;
            var height = $(t).height();
            if (height < defp.minHeight) $(t).setGridHeight(defp.minHeight + 10);
            else {
                if (height > defp.maxHeight) $(t).setGridHeight(defp.maxHeight);
                else
                    $(t).setGridHeight("100%");
            }
        }

        function getqsData(id) {
            return $(t).getRowData(id);
        }
        function aftersaveFn(rowid, data) {
            var datastr = eval('(' + data.responseText + ')');
            datastr.qsgridmethod = "edit";
            $(t).setRowData(rowid, datastr);
            setBtnshow(rowid, "cancel");
            //edit by andy 3-16
            g.refreshRow(rowid);
            defp.saveSuccess(rowid, $(t).getRowData(rowid));
        }

        function afterdelFn(rowid, data) {
            alert("已成功删除。");
            var rowData = $(t).getRowData(rowid);
            $(t).delRowData(rowid);
            defp.delSuccess(rowid, rowData);
        }
        function afterRestoreFn(rowid, data) {
            setBtnshow(rowid, 'cancel');
        }
        function aftersaveAllFn(rowid, data) {
            var datastr = eval('(' + data.responseText + ')');
            datastr.qsgridmethod = "edit";
            $(t).setRowData(rowid, datastr);
            setBtnshow(rowid, "cancel");
            //edit by andy 3-16
            // g.refreshRow(rowid);
            defp.saveSuccess(rowid, $(t).getRowData(rowid));
            //保存下一条
            if (g.currentIndex == g.editrows.length - 1) {
                if ($.isFunction(g.saveAllSuccess))
                    g.saveAllSuccess();
            }
            else {
                g.currentIndex++;
                g.saveAllFn(g.editrows[g.currentIndex]);
            }
        }

        function afterAddRow(cl) {
            var rowData = $(t).getRowData(cl);
            var d_btid = getBtnId(cl, 'd');
            var e_btid = getBtnId(cl, 'e');
            var s_btid = getBtnId(cl, 's');
            var c_btid = getBtnId(cl, 'c');
            if (defp.gridType == 'inplace') {
                var isedit = false;
                if ($("#" + d_btid).css("display") == "none")
                    isedit = true;
                var actHtml = g.setDefaultBtnHtml(e_btid, "eebtn", "编辑");
                actHtml += g.setDefaultBtnHtml(s_btid, "sebtn", "保存");
                actHtml += g.setDefaultBtnHtml(c_btid, "cebtn", "撤销");
                var delactHtml = g.setDefaultBtnHtml(d_btid, "debtn", "删除");
                if (defp.delActionSplitToLast) {
                    $(t).setRowData(cl, { del_act: delactHtml });
                    $(t).setRowData(cl, { act: actHtml });
                } else {
                    actHtml += delactHtml;
                    $(t).setRowData(cl, { act: actHtml });
                }
                if (isedit) { $("#" + d_btid).hide(); $("#" + e_btid).hide(); }
                else { $("#" + c_btid).hide(); $("#" + s_btid).hide(); }
                g.setDefaultBtnEvent(d_btid, cl, g.delrowFn);
                g.setDefaultBtnEvent(e_btid, cl, g.editrowFn);
                g.setDefaultBtnEvent(s_btid, cl, g.saveFn);
                g.setDefaultBtnEvent(c_btid, cl, g.cancelFn);
                var colname = gridName + "_act";
                $("td[aria-describedby=" + colname + "]", t).each(function () {
                    $(this).removeAttr("title");
                });
                if (defp.delActionSplitToLast) {
                    colname = gridName + "_del_act";
                    $("td[aria-describedby=" + colname + "]", t).each(function () {
                        $(this).removeAttr("title");
                    });
                }
            }
            $.each(btnArray, function (b, n) {
                var btnstr = "";
                var btn_id = gridName + "_" + n.name + "_" + cl;
                //edit by andy
                if (n.qsButton.text.constructor == Array) {
                    $.each(n.qsButton.text, function (k, m) {
                        btnstr += g.setButtonHtml(btnstr, cl, n, k);
                    });
                } else
                    btnstr += g.setButtonHtml(btnstr, cl, n, false);
                var data = {};
                var actname = n.name;
                data[actname] = btnstr;
                $(t).setRowData(cl, data);
                if (n.qsButton.text.constructor == Array) {
                    $.each(n.qsButton.text, function (k, m) {
                        g.setButtonEvent(cl, n, k);
                    });
                } else g.setButtonEvent(cl, n, false);
                var colname = gridName + "_" + n.name;
                $("td[aria-describedby=" + colname + "]", t).each(function () {
                    $(this).removeAttr("title");
                });
            });
            $.each(colUrl, function (j, n) {
                var target = "_self", link = "";
                if (n.target)
                    target = n.target;
                if (n.col != '') {
                    var parstr = n.col;
                    if (n.colparam != '')
                        parstr = n.colparam;
                    if (n.param != "")
                        link = "<a href='" + n.url + "?" + parstr + "=" + rowData[n.col] + "&" + n.param + "' target=" + target + ">" + rowData[n.name] + "</a>";
                    else
                        link = "<a href='" + n.url + "?" + parstr + "=" + rowData[n.col] + "' target=" + target + ">" + rowData[n.name] + "</a>";
                    var actname = n.name;
                    var data = {};
                    data[actname] = link;
                    $(t).jqGrid('setRowData', cl, data);
                }
                else {
                    if (n.param != "")
                        link = "<a href='" + n.url + "?" + n.param + "'>" + rowData[n.name] + "</a>";
                    var actname = n.name;
                    var data = {};
                    data[actname] = link;
                    $(t).jqGrid('setRowData', cl, data);
                }
            }); //end each

        }


        var g = {
            editrowFn: function (event) {
                var id = event.data.id;
                setBtnshow(id, 'edit');
                $(t).editRow(id, false);
            },
            delrowFn: function (event) {
                if (defp.delurl == "") {
                    alert("请设置删除的url!");
                    return;
                }
                var id;
                if (defp.gridType == "list")
                    id = $(t).getGridParam("selrow");
                else
                    id = event.data.id;
                if (!id) {
                    alert("请选择要删除的数据！");
                    return;
                }
                var rowData = $(t).getRowData(id);
                if (defp.gridType == "inplace") {
                    if (rowData.qsgridmethod == "add") {
                        $(t).delRowData(id);
                        setGridHeight();
                        return;
                    }
                }
                if (confirm(defp.delmessage)) {
                    $(t).editRow(id, false);
                    var data;
                    if ($.isFunction(defp.delData)) {
                        data = defp.delData(id, rowData);
                    }
                    else
                        data = defp.delData;
                    $(t).saveRow(id, null, defp.delurl, data, afterdelFn);
                }
            },
            cancelFn: function (event) {
                var id = event.data.id;
                //setBtnshow(id, 'cancel');
                var data = getqsData(id);
                if (data.qsgridmethod == "add") {
                    $(t).delRowData(id);
                    setGridHeight();
                }
                else {
                    setBtnshow(id, 'cancel');
                    $(t).restoreRow(id);
                }
            },
            saveFn: function (event) {
                if (defp.saveurl == "") {
                    alert("请设置保存的url!");
                    return;
                }
                var id = event.data.id;
                var rowData = $(t).getRowData(id);
                var rowId = rowData.ID;
                var data;
                var seturl = '';
                if (rowData.qsgridmethod == "add") {
                    seturl = defp.addurl;
                    if ($.isFunction(defp.addData)) {
                        data = defp.addData(id, rowData);
                    }
                    else
                        data = defp.addData;
                }
                else {
                    if ($.isFunction(defp.saveData)) {
                        data = defp.saveData(id, rowData);
                    }
                    else
                        data = defp.saveData;
                    seturl = defp.saveurl;
                }
                var canSave = defp.beforeSave(id, rowData);
                if (!canSave) return;
                $(t).saveRow(id, null, seturl, data, aftersaveFn, null, afterRestoreFn);
            },
            addRow: function (data, position, readonly) { //增加行
                if (defp.readonly)
                    return;
                var selectedIds = $(t).jqGrid("getDataIDs");
                var newid;
                if (selectedIds.length == 0)
                    newid = 1;
                else {
                    selectedIds.sort(function (a, b) { return b - a; });
                    newid = parseInt(selectedIds[0]) + 1;
                }
                data = $.extend(newRowData, data, { qsgridmethod: 'add' });
                var posi = "first";
                if (position) posi = position;
                $(t).addRowData("" + newid + "", data, posi);

                afterAddRow(newid);

                setGridHeight();
                setBtnshow(newid, 'edit');
                if (!readonly) {
                    $(t).editRow(newid, false);
                }

            },
            afterSubmit: function (responseText, rowid, rowData, cellname, value) {
                var newData = eval('(' + responseText + ')');
                $(t).setRowData(rowid, newData);
                defp.aferSave(rowid, rowData, cellname, value);
            },
            setParam: function (rowid, rowData) {//设置传递的参数（默认添加，qskey为ID） 
                if (colKey) {
                    return { ID: rowData[colKey] };
                }
                else return {};
            },
            refreshRow: function (id) {
                var cl = id;
                var rowData = getqsData(id);
                if (defp.gridType == "inplace") {
                    var d_btid = getBtnId(id, 'd');
                    var e_btid = getBtnId(id, 'e');
                    var s_btid = getBtnId(id, 's');
                    var c_btid = getBtnId(id, 'c');
                    var editshow = defp.showEditBtn(id, rowData);
                    if (typeof editshow != 'boolean')
                        editshow = true;
                    if (!editshow) {
                        $("#" + d_btid).hide(); $("#" + e_btid).hide(); $("#" + c_btid).hide(); $("#" + s_btid).hide();
                    }else {
						if(!defp.showDefaultDeleteBtn(id, rowData))
							$("#" + d_btid).hide();
						if(!defp.showDefaultEditBtn(id, rowData))
							$("#" + e_btid).hide();
						if(!defp.showDefaultCancelBtn(id, rowData))
							$("#" + c_btid).hide();
						if(!defp.showDefaultSaveBtn(id, rowData))
							$("#" + s_btid).hide();
					}
                }
                $.each(btnArray, function (i, n) {
                    if ($.isArray(n.qsButton.showFn)) {
                        $.each(n.qsButton.showFn, function (k, m) {
                            var showval = m(id, rowData);
                            if (typeof showval != "boolean")
                                showval = true;
                            var btn_id = gridName + "_" + n.name + "_" + id + "_" + k;
                            if (!showval) {
                                $("#" + btn_id).hide();
                            } else $("#" + btn_id).show();
                        });
                    }
                    else {
                        var showfn = n.qsButton.showFn;
                        if (showfn) {
                            var showval = showfn(id, rowData);
                            if (typeof showval != "boolean")
                                showval = true;
                            if ($.isArray(n.qsButton.text)) {
                                $.each(n.qsButton.text, function (k, m) {
                                    var btn_id = gridName + "_" + n.name + "_" + id + "_" + k;
                                    if (!showval)
                                        $("#" + btn_id).hide();
                                    else $("#" + btn_id).show();
                                });
                            } else {
                                var btn_id = gridName + "_" + n.name + "_" + id;
                                if (!showval)
                                    $("#" + btn_id).hide();
                                else $("#" + btn_id).show();
                            }
                        } //end if (showfn) 
                    }
                });
                var trcssval = defp.trColor(id, rowData);
                $("#" + id, t).each(function () {
                    $(this).addClass(trcssval);
                });
                $.each(colorArray, function (i, n) {//加颜色
                    var colorcssfn = n.qsColor;
                    if (colorcssfn) {
                        var cssval = colorcssfn(id, rowData);
                        var colname = gridName + "_" + n.name;
                        $("#" + id, t).each(function () {
                            $("td[aria-describedby=" + colname + "]", this).each(function () {
                                $(this).removeClass();
                                $(this).addClass(cssval);
                            });
                        });
                    }
                });
            },
            setButtonHtml: function (btnstr, rowid, n, i) {
                var btntxt = "", btn_id = "";
                var btnObj = n.qsButton;
                if (typeof i == "number") {
                    btntxt = btnObj.text[i]; btn_id = gridName + "_" + n.name + "_" + rowid + "_" + i;
                } else { btntxt = btnObj.text; btn_id = gridName + "_" + n.name + "_" + rowid; }
                if (btnObj.type == 'button')
                    btnstr = "<input style='margin: 0 2px' type='button' value=" + btntxt + "   id=" + btn_id + " />";
                else
                    btnstr = "<a style='margin: 0 2px' href='javascript:void(0);' id=" + btn_id + ">" + btntxt + "</a>";
                return btnstr;
            },
            setDefaultBtnHtml: function (btnid, btncss, tit) {
                return "<input style='margin: 0 2px' type='button' value='' id=" + btnid + " class=" + btncss + " title=" + tit + " />";
            },
            setDefaultBtnEvent: function (btnid, rowid, fun) {
                $("#" + btnid).bind('click', { id: rowid }, fun);
            },
            setButtonEvent: function (rowid, n, i) {
                var btnfn = "", btn_id = "";
                var btnObj = n.qsButton;
                if (typeof i == "number") { btnfn = btnObj.click[i]; btn_id = gridName + "_" + n.name + "_" + rowid + "_" + i; }
                else { btnfn = btnObj.click; btn_id = gridName + "_" + n.name + "_" + rowid; }
                $("#" + btn_id).bind('click', { id: rowid }, function (event) { btnfn(event); return false; });
            },
            saveAll: function (sucess) {//保存全部
                this.editrows = [];
                if (sucess)
                    this.saveAllSuccess = sucess;
                var ids = $(t).jqGrid('getDataIDs');
                for (var i = 0; i < ids.length; i++) {
                    var cl = ids[i];
                    var e_btid = getBtnName(cl, 'e');
                    if ($("#" + e_btid).is(":hidden ")) {
                        this.editrows.push(cl);
                    }
                }
                if (this.editrows.length == 0) {
                    if ($.isFunction(this.saveAllSuccess))
                        this.saveAllSuccess();
                }
                else {
                    this.currentIndex = 0; //
                    this.saveAllFn(this.editrows[0]);
                }
            },
            saveAllFn: function (rowid) {
                if (defp.saveurl == "") {
                    alert("请设置保存的url!");
                    return;
                }
                var id = rowid;
                var rowData = $(t).getRowData(id);
                var rowId = rowData.ID;
                var data;
                var seturl = '';
                if (rowData.qsgridmethod == "add") {
                    seturl = defp.addurl;
                    if ($.isFunction(defp.addData)) {
                        data = defp.addData(id, rowData);
                    }
                    else
                        data = defp.addData;
                }
                else {
                    if ($.isFunction(defp.saveData)) {
                        data = defp.saveData(id, rowData);
                    }
                    else
                        data = defp.saveData;
                    seturl = defp.saveurl;
                }
                var canSave = defp.beforeSave(id, rowData);
                if (!canSave) return;
                $(t).saveRow(id, null, seturl, data, aftersaveAllFn, null, afterRestoreFn);
            },
            saveCell: function (sucess) {
                this.saveCellSuccess = sucess;
                if (t.p.savedRow.length > 0) {
                    t.p.savedRow[0].v = "";
                    var irow = this.cellRowId;
                    var icol = this.cellColId;
                    $(t).jqGrid('saveCell', irow, icol);
                }
                else
                    this.saveCellSuccess();
            }
        }; //参数
        t.gridm = g;
        $(t).jqGrid({
            footerrow: defp.footerrow,
            userDataOnFooter: defp.userDataOnFooter,
            url: defp.url,
            datatype: defp.datatype,
            mtype: defp.mtype,
            postData: defp.postData || {},
            colNames: defp.colNames,
            colModel: defp.colModel,
            rowNum: defp.rowNum,
            rowList: defp.rowList,
            pager: defp.pager,
            viewrecords: defp.viewrecords,
            caption: defp.caption,
            jsonReader: defp.jsonReader,
            autowidth: defp.autowidth,
            width: defp.width,
            height: defp.height,
            multiselect: defp.multiselect,
            rownumbers: defp.rownumbers,
            pgbuttons: defp.pgbuttons,
            pgtext: defp.pgtext,
            pginput: defp.pginput,
            shrinkToFit: defp.shrinkToFit,
            ondblClickRow: function (id) {
                defp.ondblClickRow(id);
            },
            onSelectRow: function (id) {
                defp.onSelectRow(id);
            },
            loadComplete: function () {

                var ids = $(t).jqGrid('getDataIDs');
                for (var i = 0; i < ids.length; i++) {
                    var cl = ids[i];
                    var rowData = $(t).getRowData(cl);
                    afterAddRow(cl);

                    if (defp.isEdit)
                        $(t).editRow(ids[i], false);
                    g.refreshRow(cl);


                }
                // var ids = $(t).jqGrid('getDataIDs');
                for (var i = 0; i < ids.length; i++) {
                    var cl = ids[i];
                    $(t).setRowData(ids[i], { qsgridmethod: 'edit' });
                }
                setGridHeight();
                defp.loadComplete();
                $(t).bind('mouseover.gridcell', function (e) {
                    var td = e.target,
                        ptr = $(td, t.rows).closest("tr.jqgrow");
                    if (!ptr) return this;
                    if (td.tagName == 'A' || ((td.tagName == 'INPUT' || td.tagName == 'TEXTAREA' || td.tagName == 'OPTION' || td.tagName == 'SELECT'))) { return this; }
                    try {
                        var ci = $.jgrid.getCellIndex(td),
                        ch = (t.p.colModel[ci] || {}).hover;

                        if (!ch) return this;
                        var rd = $(t).getRowData(ptr[0].id);
                        if (typeof ch === 'function') {
                            ch.call(t, ptr[0].id, rd, td);
                        } else {
                            if ($.isFunction(ch.over)) {
                                ch.over.call(t, ptr[0].id, rd, td);
                            }
                        }
                    } catch (e) { }
                }).bind('mouseout.gridcell', function (e) {
                    var td = e.target,
                        ptr = $(td, t.rows).closest("tr.jqgrow");
                    if (!ptr) return this;
                    if (td.tagName == 'A' || ((td.tagName == 'INPUT' || td.tagName == 'TEXTAREA' || td.tagName == 'OPTION' || td.tagName == 'SELECT'))) { return this; }
                    try {
                        var ci = $.jgrid.getCellIndex(td),
                        ch = (t.p.colModel[ci] || {}).hover;

                        if (!ch) return this;
                        var rd = $(t).getRowData(ptr[0].id);
                        if (typeof ch === 'function') return this;
                        if ($.isFunction(ch.out)) {
                            ch.out.call(t, ptr[0].id, rd, td);
                        }
                    } catch (e) { }
                });
            },
            gridComplete: function () {
                defp.gridComplete();
            },
            cellEdit: defp.cellEdit,
            cellsubmit: defp.cellsubmit,
            cellurl: defp.cellurl,
            afterEditCell: function (rowid, cellname, value, iRow, iCol) {
                g.cellRowId = iRow;
                g.cellColId = iCol;
                defp.afterEditCell(rowid, cellname, value, iRow, iCol);
            },
            beforeSubmitCell: function (rowid, cellname, value, iRow, iCol) {
                var rowData = $(t).getRowData(rowid);
                var defData = g.setParam(rowid, rowData);
                var custData = defp.beforeSubmit(rowid, rowData, cellname, value);
                defData = $.extend(defData, custData);
                return defData;
            },
            afterSaveCell: function (serverresponse, rowid, cellname, value, iRow, iCol) {
                //var rowData = $(t).getRowData(rowid);
                var datastr = serverresponse.responseText;
                var rowData = $(t).getRowData(rowid);
                g.afterSubmit(datastr, rowid, rowData, cellname, value);
                //defp.aferSave(rowid, rowData, cellname, value);
                g.cellRowId = undefined;
                g.cellColId = undefined;
                if (g.saveCellSuccess)
                    g.saveCellSuccess();
            },
            errorCell: function (serverresponse, status) {
                alert(serverresponse.responseText);
            },
            afterSubmitCell: function (serverresponse, rowid, cellname, value, iRow, iCol) {
                //                var datastr = serverresponse.responseText;
                //                var rowData = $(t).getRowData(rowid);
                //                g.afterSubmit(datastr, rowid, rowData, cellname, value);
                return [true, ''];
            },
            afterSpliceCell: function (rowid, cellname, value, iRow, iCol) {
                if ($(t)[0].p.savedRow.length > 0) {
                    var irow = $(t)[0].p.savedRow[0].id, icol = $(t)[0].p.savedRow[0].ic;
                    if (iRow == irow) {
                        $(t)[0].p.savedRow.splice(0, 1);
                        $(t).jqGrid("editCell", irow, icol, true);
                    }
                }
            },
            formatCell: defp.formatCell,
            onSelectCell: defp.onSelectCell,
            ajaxGridOptions: {
                error: function (xhr, status, error) {
                    if (xhr.responseText != "")
                        alert(xhr.responseText);
                }
            },
            setCellHover: function () {

            }
        });
    }; //end 

    $.fn.qsGrid = function (p) {
        return this.each(function () {
            setqsGrid(this, p);
        });
    }; //end qsimGrid
    $.fn.qsAddRow = function (data, position, readonly) {
        this.each(function () {
            data = $.extend({}, data);
            this.gridm.addRow(data, position, readonly);
        });
    }; //end addimGrid
    $.fn.qsDelRow = function (data) {
        this.each(function () {
            data = $.extend({}, data);
            this.gridm.delrowFn(data);
        });
    }; //$.fn.qsdelRow
    $.fn.qsRefreshRow = function (rowid) {
        this.each(function () {
            this.gridm.refreshRow(rowid);
        });
    }; //end qsRefreshRow

    function SetSaveall(index, grids, sucess) {
        var len = grids.length;
        if (len > index + 1) {
            grids[index].gridm.saveAll(function () {
                index++;
                SetSaveall(index, grids, sucess);
            });
        }
        else
            grids[index].gridm.saveAll(sucess);
    }

    $.fn.qsSaveAll = function (sucess) {
        SetSaveall(0, this, sucess);
    }; //end qsSaveAll

    function SetSaveCellAll(index, grids, sucess) {
        var len = grids.length;
        if (len > index + 1) {
            grids[index].gridm.saveCell(function () {
                index++;
                SetSaveCellAll(index, grids, sucess);
            });
        }
        else
            grids[index].gridm.saveCell(sucess);
    }
    $.fn.qsSaveCell = function (sucess) {
        SetSaveCellAll(0, this, sucess);
    }; //end qsSaveCell
})(jQuery);