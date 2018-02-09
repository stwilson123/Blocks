/*-----------------------------------------------------------------
*版权所有： 南京比邻软件有限公司
*系统名称： R2E5.0
*文件名称： UI.js
*模块名称： Script框架
*作　　者： 何权洲
*创建日期： 2017-1-5
*依赖说：  
*-----------------------------------------------------------------

*-----------------------------------------------------------------
*修改人：  
*修改时间：
*修改内容：
----------------------------------------------------------------*/


define(['jquery'], function (jQuery) {

    (function ($) {

        $.fn.grid = function (option) {
            if (ValidateHelper.isNullOrEmpty($(this).attr('id'))) {
                alert("未指定ID的Grid对象"); return;
            }
            var gridID = $(this).attr("id");
            //列名创建
            var defColModel = { width: 100, align: 'left', sortable: true };

            if (!ValidateHelper.isNullOrEmpty(option.colModel)) {
                if (option.colModel.length !== option.colNames.length) {
                    alert("colModel与colNames长度不一致"); return;
                }

                for (var i = 0; i < option.colModel.length; i++) {
                    if (ValidateHelper.isNullOrEmpty(option.colModel[i].name)) {
                        alert("未指定name属性的colModel"); return;
                    }

                    if (ValidateHelper.isNullOrEmpty(option.colModel[i].index)) option.colModel[i].index = option.colModel[i].name;

                    if (ValidateHelper.isNullOrEmpty(option.colModel[i].width)) option.colModel[i].width = defColModel.width;

                    if (ValidateHelper.isNullOrEmpty(option.colModel[i].align)) option.colModel[i].align = defColModel.align;

                    //if (ValidateHelper.isNullOrEmpty(option.colNames[i].align)) option.colNames[i].align = defColModel.align;


                    if (option.colModel[i].sortable === undefined) option.colModel[i].sortable = defColModel.sortable;
                }
            } else {
                if (ValidateHelper.isNullOrEmpty(option.colModel) && ValidateHelper.isNullOrEmpty(option.colNames))
                    ;
                else { alert("未指定colModel属性的Grid对象"); return; }
            }
            $(this).attr('gridstate', 'visible');
            var defaults = {
                ajaxGridOptions: {
                    type: "POST",
                    cache: false,
                    async: true
                    //error: function () {
                    //   OpenTipWindowError("意外出错，请刷新页面重试");

                    //}
                },
                loadui: "block",
                datatype: "local",
                gridview: true,
                viewrecords: true,
                multiselect: true,
                rowNum: option.showPager || ValidateHelper.isNullOrEmpty(option.showPager) ? 10 : 999999,   //不显示分页栏时，默认显示全部,注意设-1时无法显示最后一行
                rowList: [10, 30, 50, 100, 200, 500],
                jsonReader: {
                    root: "content.rows",
                    total: "content.pagerInfo.total",
                    page: "content.pagerInfo.page",
                    records: "content.pagerInfo.records",
                    repeatitems: false,
                    id: "ID"
                },
                localReader: {
                    id: "ID"
                },
                loadError: function () { OpenTipWindowError("网络出错，请刷新页面重试"); },
                caption: "",
                sortorder: "desc",   //默认倒序,
                scrollOffset: 19,
                viewsortcols: [true],
                // sortname: "createDate", //默认排序字段
                //onRightClickRow: function (rowid, iRow, iCol, e) {
                //    if (!$.browser.msie) return;
                //    rMenu = $("#gridRightMenu");
                //    rMenu.css({ "top": e.clientY + "px", "left": e.clientX + "px", "visibility": "visible" });
                //    $("body").bind("mousedown", function RithtMenuMothDown(event) {//右键菜单
                //        if (!(event.target.id == 'gridRightMenu' || $(event.target).parents("#gridRightMenu").length > 0)) {
                //            rMenu.css({ "visibility": "hidden" });
                //        }
                //    });
                //    gridID = $(this).attr("id");
                //    e.preventDefault();
                //},
                onSelectHead: function (rowid, status, e) { },
                onSelectRow: function (rowid, status) {
                    var rowData = $('#' + gridID).jqGrid('getGridParam', 'selarrrow');
                    if (rowData.length === $('#' + gridID)[0].p.reccount) {
                        $('#cb_' + gridID).prop("checked", 'true');

                    }

                    if (!ValidateHelper.isNullOrEmpty(option.onSelectOnlyOneRow)) {
                        var lastsel = parseInt($(this).attr("lastsel"));
                        if (rowid && rowid !== lastsel) {
                            if (!isNaN(lastsel))
                                $gridObj.jqGrid("saveRow", lastsel, null, 'clientArray');
                            if (!ValidateHelper.isNullOrEmpty(options.editParams))
                                $gridObj.jqGrid('editRow', rowid, true, options.editParams.oneditfunc, null, 'clientArray', null, options.editParams.aftersavefunc);
                            else
                                $gridObj.jqGrid('editRow', rowid, true);

                            $(this).attr("lastsel", rowid);

                        }

                        option.onSelectOnlyOneRow(rowid, status);
                    }

                },
                onHeaderClick: function (status) {
                    //visible or hidden
                    $(this).attr("gridstate", status);
                    if (!ValidateHelper.isNullOrEmpty(option.onHeaderClick)) option.onHeaderClick(status);
                    $(window).resize();
                },

                colRDModel: { FormID: "", RDModel: [] },
                showPager: true,
                styleUI: 'Bootstrap',
                getGridWidth: function (width) { return width; },
                getGridHeight: function (height) { return height; },
                loadCompleteOnFaildCallBack: function () { },
                gridResize: null, //设置Grid的大小
                gridScroll: false, //是否显示Grid滚动条
                mtype: "POST" // add by 何权洲 jq默认使用POST方式提交到服务端
            };
            var options = $.extend(true, defaults, option);
            var $gridObj = $(this).eq(0);
            $(this).attr("lastsel", "");
            var customerLoadComplete = function (data) { ; };
            if (options.loadComplete !== undefined && $.type(options.loadComplete) === 'function') {
                customerLoadComplete = options.loadComplete;
            }
            var changeEventFun = function (e) {
                if (!ValidateHelper.isNullOrEmpty(option.onSelectHead) && $.isFunction(option.onSelectHead))
                    options.onSelectHead(gridID, false, e);

            };


            var loadCompleteFn = function (xhr) {
                //显示序号 
                if (options.rownumbers) {
                    $gridObj.jqGrid('setLabel', 0, '序号', 'labelstyle');
                }
                // $('.ui-jqgrid .ui-jqgrid-titlebar-close.HeaderButton').click(function () { $(window).trigger("resize"); })
                $("#" + $gridObj.attr('id') + "_pager_center").width("300px");
                //增加y轴滚动条
                //$gridObj.closest(".ui-jqgrid-bdiv").css({ 'overflow-y': 'scroll' });
                //var w2 = parseInt($('.ui-jqgrid-labels>th:eq(0)').css('width')) - 5;
                //$('.ui-jqgrid-lables>th:eq(0)').css('width', w2);
                //$('#'+$gridObj.attr("id") +' tr').find("td:eq(0)").each(function () {
                //    $(this).css('width', w2);
                //})



                $('#cb_' + gridID).unbind('change', changeEventFun);
                $('#cb_' + gridID).on('change', changeEventFun);
                //列表上下顺序拖拽
                if (options.dragSortable) {
                    $gridObj.tableDnD({
                        scrollAmount: 1,
                        onDrop: function (table, row) {
                            id = row.id;
                            getMKXH(id, uid);
                        },
                        onAllowDrop: function (draggedRow, row) {
                            uid = $(row).index();
                            return true;
                        }
                    });
                }
                var currenType = $gridObj.getGridParam('datatype');
                if (currenType === "json") {
                    if (xhr.code !== 100) {
                        OpenTipWindowError(MessageHelper.getRemoveSpecialChar(xhr.msg));
                        options.loadCompleteOnFaildCallBack(xhr);
                    }
                    //  ajaxSuccess(xhr, null, options.loadCompleteOnFaildCallBack);
                }
                customerLoadComplete(xhr);

            };
            //从服务器返回响应时执行
            options.loadComplete = loadCompleteFn;

            //ID字段名
            if (options.idKey !== undefined && options.idKey !== null) {
                options.jsonReader.id = options.idKey;
                options.localReader.id = options.idKey;

            }

            //是否显示滚动条 
            if (!options.gridScroll) {
                // options.autoWidth = true;
                //options.shrinkToFit = false;
            } else {
                options.shrinkToFit = false;
                options.autoScroll = true;
            }

            //创建分页区 
            if (options.showPager) {
                var pagerId = $gridObj.attr('id') + "_pager";
                $gridObj.siblings('div.gridpager[id="' + pagerId + '"]').remove();
                $gridObj.parent().append("<div id='" + pagerId + "' ></div>");
                options.pager = "#" + pagerId;



            }
            else {

            }


            //Grid数据
            if (!ValidateHelper.isNullOrEmpty(options.colRDModel.FormID)) {
                var $form = $("#" + options.colRDModel.FormID);
                if ($form.length !== 0) {
                    var cols = options.colRDModel.RDModel.toString();
                    var hidObj = "<input type='hidden' GridID='" + $gridObj.attr('id');
                    hidObj += "' class='____HIDGRIDDATA' id='hidGridData_'" + $gridObj.attr('id') + " name='hidGridData' value=''";
                    hidObj += " RDModel='" + cols + "'/>";
                    //   $form.find('input[type="hidden"][name="hidGridData"]').remove();
                    $form.append(hidObj);
                }
                //显示和隐藏列hidden
                var colTemp = [];
                for (var k = 0; k < option.colModel.length; k++) {
                    colTemp.push(option.colModel[k].name);
                }
                for (var j = 0; j < options.colRDModel.RDModel.length; j++) {
                    var value = options.colRDModel.RDModel[j];
                    if (!(colTemp.indexOf(value) > 0)) {
                        option.colNames.push(value);
                        var col = { name: value, index: value, hidden: true };
                        option.colModel.push(col);
                    }
                }
            }

            var rMenu = null;

            this.init = function () {
                if (options.datatype === "local") {
                    this.loadLocalData();
                } else {
                    this.loadJsonData();
                }
                //this.reload();
            }

            this.loadLocalData = function () {
                var firstOptions = {};
                firstOptions.datatype = "local";
                firstOptions = $.extend(defaults, firstOptions)
                $gridObj.jqGrid(firstOptions);
            };

            this.loadJsonData = function () {
                $gridObj.jqGrid("setGridParam", {
                    datatype: "json",
                    url: options.url,
                    postData: getPostData('QueryForm')
                });
            };

            this.reload = function () {
                $gridObj.jqGrid().trigger("reloadGrid");
            };


            //大小重置
            this.gridResize = function () {
                if ($.type(options.gridResize) === "function") {
                    options.gridResize.call();
                    $(window).resize(function () {
                        options.gridResize.call();
                    });
                } else {
                    setTimeout(function () {

                        $gridObj.setGridWidth(options.getGridWidth(GridContainerLengthFactory.GetGridContainerWidth($("#ConfigName").val())));
                        $gridObj.setGridHeight(options.getGridHeight(GridContainerLengthFactory.GetGridContainerHeight($("#ConfigName").val())));

                    }, 200);
                    $(window).resize(function () {
                        setTimeout(function () {

                            $gridObj.setGridWidth(options.getGridWidth(
                                GridContainerLengthFactory.GetGridContainerWidth($("#ConfigName").val())
                            ));
                            $gridObj.setGridHeight(options.getGridHeight(
                                GridContainerLengthFactory.GetGridContainerHeight($("#ConfigName").val())
                            ));
                        }, 200)

                    });
                }
            }

            this.init();

            this.gridResize();

            //$(window).resize();

            //$("#" + $gridObj.attr('id')).jqGrid('navGrid', options.pager,
            //    { edit: false, add: false, del: false },
            //    {},
            //    {},
            //    {},
            //    { multipleSearch: true, multipleGroup: true, showQuery: true }
            //);

        };
        $.fn.ReloadGrid = function (option) {
            var defaults = {
                url: "",
                container: $(this).attr('id'),
                formID: "SearchForm",
                datatype: "json",
                page: 1,
                ReloadType: "normal"
            };
            var options = $.extend(defaults, option);
            if (options.ReloadType == "normal") {
                if (ValidateHelper.isNullOrEmpty(option.url)) {
                    $("#" + options.container).jqGrid("clearGridData");
                    $("#" + options.container).jqGrid('setGridParam', { datatype: 'local', data: options.data }).trigger('reloadGrid', [{ page: options.page }]);
                }
                else {

                    options.url = UrlHelper.GetRandURL(options.url);
                    var postJsonData = FormHelper.getPostBodyToJson(options.formID);
                    postJsonData = $.extend(postJsonData, option.postData);
                    if ("nodata" === postJsonData) return;
                    $("#" + options.container).jqGrid('setGridParam', {
                        url: options.url,
                        datatype: options.datatype,
                        page: options.page,
                        postData: postJsonData
                    }).trigger("reloadGrid");
                }
            }
            else if (options.ReloadType == "dynamic") {
                var gridParam = $(this).jqGrid('getGridParam');
                options.url = UrlHelper.GetRandURL(options.url);
                var postJsonData = FormHelper.getPostBodyToJson(options.formID);
                postJsonData = $.extend(postJsonData, options.postData);
                postJsonData = $.extend(gridParam.postData, postJsonData);
                postJsonData.page = 1;
                postJsonData.sidx = "";
                var gridID = $(this).attr('id');
                AjaxHelper.submitJSONFormWithLoadDialog({
                    URL: options.url, postJsonData: postJsonData, onSuccessCallBack: function (msg, content, code) {
                        var currentGridParam = $.extend(true, {}, gridParam);
                        currentGridParam = $.extend(currentGridParam, {
                            colNames: content.colNames,
                            colModel: content.colModel,
                            url: null,
                            datatype: 'local',
                            data: [],
                            sortname: ''
                        });
                        //gridUnload(gridID);

                        // $.jgrid.GridDestroy(gridID);

                        $.jgrid.gridUnload(gridID);
                        //   $("#" + gridID).jqGrid('GridUnload'); 
                        $("#" + gridID).grid(currentGridParam);
                        var NewGridParam = content.pagerInfo;
                        NewGridParam.data = content.rows;
                        var localReader = {
                            //id: "id",//设置返回参数中，表格ID的名字为blackId  
                            rows: function (object) { return content.rows },
                            page: function (object) { return content.pagerInfo.page },
                            total: function (object) { return content.pagerInfo.total },
                            records: function (object) { return content.pagerInfo.records },
                            repeatitems: false
                        }
                        $("#" + gridID).jqGrid('setGridParam', { data: content.rows, localReader: localReader }).trigger('reloadGrid', [{ page: options.page }]);
                        $("#" + options.container).jqGrid('setGridParam', {
                            url: options.url,
                            datatype: options.datatype,
                            page: options.page,
                            postData: postJsonData
                        })

                    }
                });


            }

        };
        $.fn.delRowData = function (rowsId) {
            if (ValidateHelper.isNullOrEmpty(rowsId) || !$.isArray(rowsId)) {
                throw "请输入合适的id数组";
            }
            while (rowsId.length > 0) {
                $(this).jqGrid('delRowData', rowsId[0]);
            }
            var rowData = $(this).jqGrid('getGridParam', 'selarrrow');
            if (rowData.length === 0) {
                $('#cb_' + $(this).attr("id")).prop("checked", false);

            }
        };
        $.fn.addRowData = function (gridData) {
            if (ValidateHelper.isNullOrEmpty(gridData)) {
                throw "请输入合适的Json数组";
            }
            var lastIndex = $(this).jqGrid('getDataIDs').length;
            var result = 0;
            var idKey = $(this).jqGrid("getGridParam", "idKey");
            for (var i = 0; i < gridData.length; i++) {
                if (ValidateHelper.isNullOrEmpty(gridData[i][idKey])) {
                    throw "Grid数据中不包含列是" + idKey;
                }
                result = $(this).jqGrid('addRowData', gridData[i][idKey], gridData[i], "After", lastIndex++) ? result + 1 : result;

            }
        };
        $.fn.saveRow = function (gridIds) {
            //if (ValidateHelper.isNullOrEmpty(gridIds)) {
            //    throw "请输入合适的IDs数组";
            //}

            var GridIds = ValidateHelper.isNullOrEmpty(gridIds) ? $(this).jqGrid("getDataIDs") : gridIds;
            var result = 0;
            for (var i = 0; i < GridIds.length; i++) {
                result = $(this).jqGrid("saveRow", GridIds[i], null, 'clientArray') ? result + 1 : result;
            }
            //重置最后选择项目
            $(this).attr("lastsel", "");
            return result;
        };
        $.fn.getRowData = function (gridIds, colNames) {
            var rowDataResult = [];

            var isRownum = $(this).jqGrid("getGridParam", "rownumbers");
            var isInputGirdIds = !ValidateHelper.isNullOrEmpty(gridIds);
            isInputGirdIds = Array.isArray(gridIds) && gridIds.length < 1 ? true : isInputGirdIds;
            var gridData = $(this).jqGrid("getRowData");
            var gridLength = isInputGirdIds ? gridIds.length : gridData.length;
            for (var i = 0; i < gridLength; i++) {
                var rowData = isInputGirdIds ? $(this).jqGrid("getRowData", gridIds[i]) : gridData[i];
                if (isRownum) // TODO gridRowNum 计算不准确
                    rowData.gridRowNum = i + 1;
                if (ValidateHelper.isNullOrEmpty(colNames)) {
                    rowDataResult.push(rowData);
                    continue;
                }
                var jsonObj = {};
                for (var j = 0; j < colNames.length; j++) {
                    var v = rowData[colNames[j]]
                    if (ValidateHelper.isNullOrEmpty(v)) {
                        v = "";
                    }
                    jsonObj[colNames[j]] = v;
                }
                rowDataResult.push(jsonObj);
            }


            return rowDataResult;
        };

        $.fn.getGridState = function () {
            //TODO 如果已非默认的方式打开grid，可能导致错误，
            return $(this).attr('gridstate');
        };

    })(jQuery, ValidateHelper, GridContainerLengthFactory);
})

//unload grid
//function gridUnload(jqGridId) {
//    if (!jqGridId) { return; }
//    jqGridId = $.trim(jqGridId);
//    if (jqGridId.indexOf("#") === 0) {
//        jqGridId = jqGridId.substring(1);
//    }

//    var $t = $("#" + $.jgrid.jqID(jqGridId))[0];
//    if (!$t.grid) { return; }
//    var defgrid = { id: $($t).attr('id'), cl: $($t).attr('class') };
//    if ($t.p.pager) {
//        $($t.p.pager).unbind().empty().removeClass("ui-state-default ui-jqgrid-pager ui-corner-bottom");
//    }
//    var newtable = document.createElement('table');
//    newtable.className = defgrid.cl;
//    var gid = $.jgrid.jqID($t.id);
//    $(newtable).removeClass("ui-jqgrid-btable").insertBefore("#gbox_" + gid);
//    if ($($t.p.pager).parents("#gbox_" + gid).length === 1) {
//        $($t.p.pager).insertBefore("#gbox_" + gid);
//    }
//    clearBeforeUnload(jqGridId);

//    $("#alertmod_" + $.jgrid.jqID(jqGridId)).remove();
//    $("#gbox_" + gid).remove();

//    $(newtable).attr({ id: defgrid.id });

//}

//function clearBeforeUnload(jqGridId) {
//    var $t = $("#" + $.jgrid.jqID(jqGridId))[0], grid;

//    if (!$t.grid) { return; }
//    grid = $t.grid;
//    if ($.isFunction(grid.emptyRows)) {
//        grid.emptyRows.call($t, true, true); // this work quick enough and reduce the size of memory leaks if we have someone
//    }

//    $(document).unbind("mouseup.jqGrid" + $t.p.id);
//    $(document).unbind("mouseup");
//    $(window).unbind("resize");
//    $(grid.hDiv).unbind("mousemove"); // TODO add namespace
//    $("body").unbind("mousedown")
//    $($t).unbind();

//    $($t)["init"] = null;
//    $($t)["loadLocalData"] = null;
//    $($t)["loadJsonData"] = null;
//    $($t)["reload"] = null;
//    $($t)["gridResize"] = null;


//    $(window)["resize"] = null;

//    grid.dragEnd = null;
//    grid.dragMove = null;
//    grid.dragStart = null;
//    grid.emptyRows = null;
//    grid.populate = null;
//    grid.populateVisible = null;
//    grid.scrollGrid = null;
//    grid.selectionPreserver = null;

//    grid.bDiv = null;
//    grid.cDiv = null;
//    grid.hDiv = null;
//    grid.cols = null;

//    $t.formatCol = null;
//    $t.sortData = null;
//    $t.updatepager = null;
//    $t.refreshIndex = null;
//    $t.setHeadCheckBox = null;
//    $t.constructTr = null;
//    $t.formatter = null;
//    $t.addXmlData = null;
//    $t.addJSONData = null;

//    var i, l = grid.headers.length,
//    removevents = ['formatCol', 'sortData', 'updatepager', 'refreshIndex', 'setHeadCheckBox', 'constructTr', 'formatter', 'addXmlData', 'addJSONData', 'grid', 'p'];
//    for (i = 0; i < l; i++) {
//        grid.headers[i].el = null;
//    }

//    for (i in grid) {
//        if (grid.hasOwnProperty(i)) {
//            grid[i] = null;
//        }
//    }
//    // experimental 
//    for (i in $t.p) {
//        if ($t.p.hasOwnProperty(i)) {
//            $t.p[i] = $.isArray($t.p[i]) ? [] : null;
//        }
//    }
//    for (i in $t.grid) {
//        if ($t.grid.hasOwnProperty(i)) {
//            $t.grid[i] = $.isArray($t.grid[i]) ? [] : null;
//        }
//    }
//    this.p = null;
//    l = removevents.length;
//    for (i = 0; i < l; i++) {
//        $t[removevents[i]] = null;
//        delete ($t[removevents[i]]);
//    }
//}

//var gridID = null;
//function RightMenuCopy() {
//    if (gridID === null) return;

//    var selids = $("#" + gridID).jqGrid('getGridParam', 'selarrrow');
//    var rows = [];
//    for (var i = 0; i < selids.length; i++) {
//        var row = $("#" + gridID).jqGrid('getRowData', selids[i]);
//        rows.push(row);
//    }

//    if (rows.length === 0) {
//        jqAlert("请勾选数据！");
//        return;
//    }

//    var result = '';
//    for (i = 0; i < rows.length; i++) {
//        for (var item in rows[i]) {
//            result += rows[i][item].replace(/<[^>]+>/g, "") + "\t";
//        }
//        result += "\r\n";
//    }

//    if (window.clipboardData) {
//        if (window.clipboardData.setData("Text", result))
//            jqAlert("数据已经复制到剪切板！");
//    }
//};


/*
my97 datePicker 

1、默认日期格式: yyyy-MM-dd 其他格式请指定 DateFmt；
2、单个日期控件: 必要参数 pickerID：控件id ; 
非必要参数 startDate： 默认起始日期
minDate： 静态限制作用的最小日期
maxDate： 静态限制作用的最大日期 ；

3、两个日期控件: 必要参数 start、 end;
非必要参数 minDate：起始控件最小日期
maxDate：结束控件最大日期

*/
(function ($) {
    $.DataPicker = function (settings) {

        settings = jQuery.extend({
            skin: 'default',
            dateFmt: 'yyyy-MM-dd HH:mm:ss'
        }, settings);

        var pickerSettings = {
            skin: settings.skin,
            dateFmt: settings.dateFmt,
            readOnly: settings.readOnly,
            onpicked: settings.onpicked
        };

        //单个日期控件
        var isSingle = !ValidateHelper.isNullOrEmpty(settings.pickerID);
        if (isSingle) {

            var picker = $('#' + settings.pickerID);

            picker.attr('tabindex', '-1');
            if (picker.length === 0) {
                alert("请指定正确的控件ID [" + settings.pickerID + "] !");
                return;
            };

            if (!picker.hasClass('Wdate')) picker.addClass('Wdate');

            if (!ValidateHelper.isNullOrEmpty(settings.startDate)) {
                pickerSettings.startDate = settings.startDate;
            }

            if (!ValidateHelper.isNullOrEmpty(settings.minDate)) {
                pickerSettings.minDate = settings.minDate;
            }

            if (!ValidateHelper.isNullOrEmpty(settings.maxDate)) {
                pickerSettings.maxDate = settings.maxDate;
            }
            //picker.click(function () {
            //    WdatePicker(pickerSettings);
            //});
            picker.focus(function () {
                WdatePicker(pickerSettings);
            });
        } else {

            //两个日期控件 动态限制
            if (!ValidateHelper.isNullOrEmpty(settings.start) && !ValidateHelper.isNullOrEmpty(settings.end)) {

                var startPicker = $('#' + settings.start);

                if (startPicker.length === 0) {
                    alert("请指定正确的控件ID [" + settings.start + "] !");
                   
                    return
                };
                var endPicker = $('#' + settings.end);

                if (endPicker.length === 0) {
                   
                    alert("请指定正确的控件ID [" + settings.end + "] !");
                    return
                };

                startPicker.focus(function () { });
                endPicker.focus(function () { });

                startPicker.attr('tabindex', '-1');
                endPicker.attr('tabindex', '-1');
                if (!startPicker.hasClass('Wdate')) startPicker.addClass('Wdate');
                if (!endPicker.hasClass('Wdate')) endPicker.addClass('Wdate');

                var startPickerMaxDate = '#F{$dp.$D(\'' + settings.end + '\')';
                if (!ValidateHelper.isNullOrEmpty(settings.maxDate)) {
                    startPickerMaxDate += '||\'' + settings.maxDate + '\'';
                }
                startPickerMaxDate += "}";

                var startPickerSettings = jQuery.extend({
                    maxDate: startPickerMaxDate
                }, pickerSettings);

                if (!ValidateHelper.isNullOrEmpty(settings.minDate)) {
                    startPickerSettings.minDate = settings.minDate;
                }

                var endPickerMinDate = '#F{$dp.$D(\'' + settings.start + '\')}';

                var endPickerSettings = jQuery.extend({
                    minDate: endPickerMinDate
                }, pickerSettings);

                if (!ValidateHelper.isNullOrEmpty(settings.maxDate)) {
                    endPickerSettings.maxDate = settings.maxDate;
                }

                //修改单击文本框日期面板不弹出的问题
                endPicker.click(function () {
                    WdatePicker(endPickerSettings);
                });

                startPicker.click(function () {
                    WdatePicker(startPickerSettings);
                });

            }
        }

        function getPickerArgs(o) {
            var endArgs = '';
            for (var item in endPickerSettings) {
                endArgs += item;
                endArgs += ":\"";
                endArgs += endPickerSettings[item];
                endArgs += "\" ";
                endArgs += ",";
            }
            endArgs = endArgs.substring(0, endArgs.length - 1);

            return endArgs;
        }
    }
})(jQuery, ValidateHelper);





/*
 Layer 控件扩展
*/
//(function ($) {
//    $.Dialog = function (settings) {
//        if (ValidateHelper.isNullOrEmpty(settings.dialogType))
//        {
//            settings = jQuery.extend({
//                type: 2,
//                title: "",
//                offset: "auto",
//                isMaxmin: true,
//                area: ['100%', '100%'],
//                content: settings.url,
//                cancel: function () {
//                    return true;
//                },
//                end: function () {
//                    if (!ValidateHelper.isNullOrEmpty(settings.onEnd)) {
//                        var returnData = ValidateHelper.isNullOrEmpty(returnValueFunction) ? null : returnValueFunction;
//                        settings.onEnd(returnData);
//                    }

//                },
//                resize: false
//            }, settings);

//            if (!ValidateHelper.isNullOrEmpty(settings.onEnd)) {
//                if ("undefined" === typeof (returnValueFunction) && "undefined" === typeof (setReturnValueFunction)) {
//                    var script = document.createElement('script');
//                    script.type = 'text/javascript';
//                    script.innerText = "var returnValueFunction;                                        \
//                           function setReturnValueFunction(returnValueInput){                        \
//                                if(Array.isArray(returnValueInput))                               \
//                                {                                                                     \
//                                    var arrayResult = [];                                             \
//                                    for (var i = 0; i < returnValueInput.length; i++) {                \
//                                        arrayResult.push($.extend(true, {}, returnValueInput[i]));      \
//                                    }                                                                 \
//                                    returnValueFunction = arrayResult;                                \
//                                    return;                                                            \
//                                }                                                                       \
//                                returnValueFunction = $.extend(true,{},returnValueInput);                \
//                            }";

//                    document.body.appendChild(script);
//                }
//                else {
//                    returnValueFunction = null;
//                }


//            }
//            /********弹出窗口***************/
//            return layer.open(settings);
//        }
//        else if (settings.dialogType == "Confirm")
//        {
//            settings = jQuery.extend({
               
//            }, settings);
//            layer.confirm(settings.msg, {
//                btn: ['确定', '取消'], //可以无限个按钮
//                icon: 3, title: settings.title
//            }, function (index) {
//                //按钮【确定】的回调
//                layer.close(index);
//                if (!ValidateHelper.isNullOrEmpty(settings.yesFun) && DataType.isFunction(settings.yesFun))
//                    settings.yesFun(index);
//            }, function (index) {
//                //按钮【取消】的回调
//                if (!ValidateHelper.isNullOrEmpty(settings.cancelFun) && DataType.isFunction(settings.cancelFun))
//                    settings.cancelFun(index);

//            });
//        }
       
//    }
//})(jQuery, ValidateHelper);



//(function ($) {

//    $.fn.Tree = {};
//    $.fn.Tree.Init = function (domObj, setting, Nodes) {

//        return $.fn.zTree.init(domObj, setting, Nodes);
//    };
//    $.fn.Tree.GetTreeObj = function (id) {

//        return $.fn.zTree.getZTreeObj(id);
//    };

//})(jQuery, ValidateHelper);


//(function ($) {

//    $.fn.Select = $.fn.Select | {};
//    $.fn.Select = function (setting) {
//        if (undefined !== setting.data) {
//            $(this).html("");
//            $(this).append($("<option value=\"\">请选择..</option>"));
//            var htmlId = $(this).attr("id");
//            $.each(setting.data, function (key, val) {
//                $("#" + htmlId).append($("<option value=\"" + key + "\">" + val + " </option>"));
//            });

//        }

//        $(this).change(function (e) {
//            if (undefined !== setting.onSelect)
//                setting.onSelect(e, e.target.selectedOptions);
//        });
//        if (undefined !== setting.selectid) {
//            $(this).val(setting.selectid);
//        }
//    }


//})(jQuery);



//(function ($) {

//    $.fn.Check = $.fn.Check | {};
//    $.fn.Check = function (setting) {
//        //if (!$(this).is('div'))
//        //    throw '元素容器必须是DIV'
//        if ('undefined' === setting) {
//            return;
//        }
//        var FunctionMethod = function (option) {
//            var divId = option.htmlid;
//            var FindOption = function (input, state, regular) {
//                if (input.data('iCheck')) {
//                    return input.data('iCheck').o[state + (regular ? '' : 'Class')];
//                }
//            };
//            switch (option.methodName) {
//                case 'getCheckedID':
//                    var checkVal = [];
//                    $("#" + divId + ' input:checked').each(function (index, obj) {
//                        checkVal.push(obj.id);
//                    });
//                    return checkVal;
                     
//                case 'toggle':
//                    var htmlObj = $("#" + divId),
//                        node = htmlObj[0],
//                        parent = htmlObj.parent(),
//                        state = !node['checked'];
//                    node['checked'] = state;
//                    // set state class
//                    if (state)
//                        parent['addClass'](FindOption(htmlObj, 'checked') || '');
//                    else
//                        parent['removeClass'](FindOption(htmlObj, 'checked') || '');

//                    break;
//                case 'unLoad':
                   
//                    $('#' + divId + ' input').iCheck('destroy'); 
//                    $('#' + divId).empty();
//                    break;
//            }
//        };



//        if (DataType.isString(setting)) {
//            return FunctionMethod({ methodName: setting, htmlid: $(this).attr('id') });
//        }
//        $(this).html("");
//        $(this).addClass("demo");
//        var options = $.extend({
//            title: '',
//            colnum: 1

//        }, setting);
//        if ('' !== options.title) {
//            $(this).append('<h5>' + setting.title + '</h5>');
//        }
//        var dataDivHtml = "<div class='demo-list clear' style='" + (options.width ? "width:" + options.width + ";" : "") +
//            (options.height ? "height:" + options.height + ";" : "") + "' />";
//        var dataDiv = $(dataDivHtml);
//        $(this).append(dataDiv);
//        if ('undefined' !== setting.data && !Array.isArray(options.data))
//            throw '参数中的data不是一个数组'
//        var dataIndex = 0;
//        var arrayData = setting.data;
//        while (dataIndex < setting.data.length) {
//            var ulHtml = "<ul class='list'>";
//            for (var i = dataIndex; i < dataIndex + options.colnum && i < arrayData.length; i++) {
//                var liHtml = "<li>  \
//                    <input type='checkbox' id='" + arrayData[i].key + "' />" +
//                     "<label title='" + arrayData[i].value + "'for='" + arrayData[i].key + "'> " + arrayData[i].value + " </label>" +
//                "</li>"
//                ulHtml += liHtml;

//            }
//            dataIndex += options.colnum;
//            ulHtml += "</ul>";
//            dataDiv.append(ulHtml);
//        }
//        $('#' + $(this).attr('id') + ' input').iCheck({
//            checkboxClass: 'icheckbox_square-blue',
//            increaseArea: '20%'
//        });



//    }

//    $.fn.On = function (eventName, callback) {
//        var divID = $(this).attr('id');
//        $('#' + $(this).attr('id') + ' input').on(eventName, function (event) {
//            callback({ id: event.target.id, value: $("#" + divID + " Label[for='" + event.target.id + "']").text() }, event);
//            return false;
//        });
//    }
//})(jQuery);

//(function ($) {
//    $.fn.Combobox = $.fn.Combobox | {};
//    $.fn.Combobox = function (setting) {
//        $(this).attr('uiComponent', 'combobox');
//        var defaults = {
//            resultsField: 'content',
//            placeholder: '请选择',
//            dataUrlParams: setting.postData,
//            data: setting.url,
//            queryParam: 'comboxQ',
//            valueField: 'ID',
//            maxSuggestions: 10,
//            displayField: 'Text',
//            //expandOnFocus: true,
//            invalidCls: 'ms-inv',
//            minChars: 3,
//            noSuggestionText: '没有找到 {{queryParam}}',
//            minCharsRenderer: function (v) {
//                return '请输入超过' + 3 + '位字符';
//            },
//        };
//        var option = $.extend(defaults,setting);
//        option.dataUrlParams = $.extend({ comboxTopNum: option.maxSuggestions }, option.dataUrlParams );
//        var ms = $(this).magicSuggest(option);
//        var htmlId = $(this).attr("id");
//        $(ms).on('selectionchange', function (e, m) {
           
//            var sels = this.getSelection();
//            if (sels.length > 0)
//            {
//                var currentSel = sels[sels.length - 1];
//                var isExist = false;
//                $.each(this.getData(), function (index, value) {
//                    if (value.ID === currentSel.ID)
//                    {
//                        isExist = true;
//                        return false;
//                    }
//                });  
//                if (!isExist)
//                    this.removeFromSelection([currentSel], true);
//            }
//            $("#" + htmlId +" .ms-sel-ctn input:eq(0)").focus()
//        });
//        var sourceValid = ms.isValid;
//        ms.isValid = function()
//        {
//            var result = sourceValid();
//            if (option.required && !result)
//                ms.container.addClass(option.invalidCls);
//            return result;
//        }
        
       
//        return ms;

//    }

//})(jQuery);
