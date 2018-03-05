﻿;define(['jquery', 'blocks_utility', 'jqGrid', 'Blocks.ResourcesModule/Framework/UI/Component/msg_alert','Blocks.ResourcesModule/lib/jqGrid/script/i18n/grid.locale-cn'], function ($, utility, jqGrid, msgAlert) {
    var validate = utility.validate;


    var grid = function (option) {
        if (! this instanceof grid)
            return null;
      
    
       
        var gridID = option.Id;
      
        initLoadCompleted(option);

        this._options = $.extend(true, this.config.default, option);
        var jqObj = option.gridObj;
        initGridObject(option);
        initColmodel(this._options,this.config.default.colModelTemplate);
        initReader(this._options);
        var $gridObj = option.gridObj;

    
        
       

       
        initGridView(this._options);
        initGridEvent.call(this,this._options);

        //Grid数据
        // if (!validate.isNullOrEmpty(options.colRDModel.FormID)) {
        //     var $form = $("#" + options.colRDModel.FormID);
        //     if ($form.length !== 0) {
        //         var cols = options.colRDModel.RDModel.toString();
        //         var hidObj = "<input type='hidden' GridID='" + $gridObj.attr('id');
        //         hidObj += "' class='____HIDGRIDDATA' id='hidGridData_'" + $gridObj.attr('id') + " name='hidGridData' value=''";
        //         hidObj += " RDModel='" + cols + "'/>";
        //         //   $form.find('input[type="hidden"][name="hidGridData"]').remove();
        //         $form.append(hidObj);
        //     }
        //     //显示和隐藏列hidden
        //     var colTemp = [];
        //     for (var k = 0; k < option.colModel.length; k++) {
        //         colTemp.push(option.colModel[k].name);
        //     }
        //     for (var j = 0; j < options.colRDModel.RDModel.length; j++) {
        //         var value = options.colRDModel.RDModel[j];
        //         if (!(colTemp.indexOf(value) > 0)) {
        //             option.colNames.push(value);
        //             var col = {name: value, index: value, hidden: true};
        //             option.colModel.push(col);
        //         }
        //     }
        // }


        this.init = function () {
            if (this._options.datatype === "local") {
                this.loadLocalData();
            } else {
                this.loadJsonData();
            }
            //this.reload();
        };

        this.loadLocalData = function () {
            var firstOptions = {};
            firstOptions.datatype = "local";
            firstOptions = $.extend(this.config.default, firstOptions);
            $gridObj.jqGrid(firstOptions);
        };

        this.loadJsonData = function () {
            $gridObj.jqGrid("setGridParam", {
                datatype: "json",
                url: _options.url,
                postData: getPostData('QueryForm')
            });
        };

        this.reload = function () {
            $gridObj.jqGrid().trigger("reloadGrid");
        };

       

        //大小重置
        this.gridResize = function () {
            var option = this._options;
            if ($.type(option.gridResize) === "function") {
                option.gridResize.call();
                $(window).resize(function () {
                    option.gridResize.call();
                });
            } else {
                setTimeout(function () {
                  //  $("#gbox_gridInfo").parent().width()
                    var parentContainer = $gridObj.parents('#gbox_'+$gridObj.attr('id')).parent(); 
                    $gridObj.setGridWidth(parentContainer.width());
                    $gridObj.setGridHeight(parentContainer.height()- grid.prototype.getGridHeightWithoutBdiv());
                    //$gridObj.setGridWidth(options.getGridWidth(GridContainerLengthFactory.GetGridContainerWidth($("#ConfigName").val())));
                    //$gridObj.setGridHeight(options.getGridHeight(GridContainerLengthFactory.GetGridContainerHeight($("#ConfigName").val())));

                }, 200);
                $(window).resize(function () {
                    setTimeout(function () {
                        var parentContainer = $gridObj.parents('#gbox_'+$gridObj.attr('id')).parent();
                        $gridObj.setGridWidth(parentContainer.width());
                        $gridObj.setGridHeight(parentContainer.height()- grid.prototype.getGridHeightWithoutBdiv());
                        //$gridObj.setGridWidth(options.getGridWidth(
                        //    GridContainerLengthFactory.GetGridContainerWidth($("#ConfigName").val())
                        //));
                        //$gridObj.setGridHeight(options.getGridHeight(
                        //    GridContainerLengthFactory.GetGridContainerHeight($("#ConfigName").val())
                        //));
                    }, 200)

                });
            }
        };

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

    function initColmodel(option,defColModel) {
        if (validate.isNullOrEmpty(option.colModel) || validate.isNullOrEmpty(option.colNames))
            throw("Grid对象必须含有colModel和colNames属性");
 
        if (option.colModel.length !== option.colNames.length) {
            throw "colModel与colNames长度不一致";
        }

        for (var i = 0; i < option.colModel.length; i++) {
            if (validate.isNullOrEmpty(option.colModel[i].name)) {
                throw "colModel必须含有name属性";
            }
            
            if (validate.isNullOrEmpty(option.colModel[i].index)) option.colModel[i].index = option.colModel[i].name;
            $.extend(option.colModel[i], defColModel);
        }

    }
    function initGridObject(option) {
        if (validate.isNullOrEmpty(option) || !option.gridObj ||  validate.isNullOrEmpty(option.gridObj.attr('id'))) {
            throw new Error("未指定的Grid对象");
        }
        option.gridObj.attr('gridstate', 'visible');
        option.gridObj.attr("lastsel", "");
    }


    function initLoadCompleted(options) {
        var loadCompleteFn = function (xhr) {
            
            // $('.ui-jqgrid .ui-jqgrid-titlebar-close.HeaderButton').click(function () { $(window).trigger("resize"); })
            $gridObj.find("#" + $gridObj.attr('id') + "_pager_center").width("300px");
            //增加y轴滚动条
            //$gridObj.closest(".ui-jqgrid-bdiv").css({ 'overflow-y': 'scroll' });
            //var w2 = parseInt($('.ui-jqgrid-labels>th:eq(0)').css('width')) - 5;
            //$('.ui-jqgrid-lables>th:eq(0)').css('width', w2);
            //$('#'+$gridObj.attr("id") +' tr').find("td:eq(0)").each(function () {
            //    $(this).css('width', w2);
            //})


          
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
                if (xhr.code !== '200') {
                    msgAlert.error({message: xhr.msg});

                    options.loadCompleteOnFaildCallBack(xhr);
                }
                //  ajaxSuccess(xhr, null, options.loadCompleteOnFaildCallBack);
            }
            customerLoadComplete(xhr);

        };
        //从服务器返回响应时执行
        if (!validate.isNullOrEmpty(options.loadComplete))
            this.loadComplete(options.loadComplete);
        options.loadComplete = null;

    }
    function initGridView(options) {
        var $gridObj= options.gridObj;
        //创建分页区 
        if (options.showPager) {
            var pagerId = $gridObj.attr('id') + "_pager";
            $gridObj.siblings('div.gridpager[id="' + pagerId + '"]').remove();
            $gridObj.parent().append("<div id='" + pagerId + "' ></div>");
            options.pager = "#" + pagerId;
        }


        //显示列序号 
        if (options.rownumbers) {
            this.loadComplete(function () {
                $gridObj.jqGrid('setLabel', 0, '序号', 'labelstyle');
            });
           
        }

    }
    function initGridEvent(options) {
        var changeEventFun = function (e) {
            if (!validate.isNullOrEmpty(options.onSelectHead) && $.isFunction(options.onSelectHead))
                options.onSelectHead(gridID, false, e);

        };
        this.loadComplete(function () {
            options.gridObj.find('#cb_' + gridID).unbind('change', changeEventFun);
            options.gridObj.find('#cb_' + gridID).on('change', changeEventFun);
        });
    }


    function initReader(options) {
        //ID字段名
        if (options.idKey !== undefined && options.idKey !== null) {
            options.jsonReader.id = options.idKey;
            options.localReader.id = options.idKey;

        }
    }

    grid.prototype.config = {

        'default':{

            ajaxGridOptions: {
                type: "POST",
                cache: false,
                async: true,
                contentType: 'application/json',
                error: function () {
                    msgAlert.error({message: "意外出错，请刷新页面重试"});
                }
            },
            loadui: "block",
            datatype: "local",
            gridview: true,
            viewrecords: true,
            multiselect: true,
            rowNum: 10, //option.showPager || validate.isNullOrEmpty(option.showPager) ? 10 : 999999,   //不显示分页栏时，默认显示全部,注意设-1时无法显示最后一行
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
            loadError: function () {
                msgAlert.error({message: "网络出错，请刷新页面重试"});
            },
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
            onSelectHead: function (rowid, status, e) {
            },
            onSelectRow: function (rowid, status) {
                var rowData = $('#' + gridID).jqGrid('getGridParam', 'selarrrow');
                if (rowData.length === $('#' + gridID)[0].p.reccount) {
                    $('#cb_' + gridID).prop("checked", 'true');

                }

                if (!validate.isNullOrEmpty(option.onSelectOnlyOneRow)) {
                    var lastsel = parseInt($(this).attr("lastsel"));
                    if (rowid && rowid !== lastsel) {
                        if (!isNaN(lastsel))
                            $gridObj.jqGrid("saveRow", lastsel, null, 'clientArray');
                        if (!validate.isNullOrEmpty(options.editParams))
                            $gridObj.jqGrid('editRow', rowid, true, options.editParams.oneditfunc, null, 'clientArray', null, options.editParams.aftersavefunc);
                        else
                            $gridObj.jqGrid('editRow', rowid, true);

                        jqObj.attr("lastsel", rowid);

                    }

                    option.onSelectOnlyOneRow(rowid, status);
                }

            },
            onHeaderClick: function (status) {
                //visible or hidden
                jqObj.attr("gridstate", status);
                if (!validate.isNullOrEmpty(option.onHeaderClick)) option.onHeaderClick(status);
                $(window).resize();
            },

            colRDModel: {FormID: "", RDModel: []},
            showPager: true,
            styleUI: 'Bootstrap',
            getGridWidth: function (width) {
                return width;
            },
            getGridHeight: function (height) {
                return height;
            },
            loadCompleteOnFaildCallBack: function () {
            },
            gridResize: null, //设置Grid的大小
            gridScroll: false, //是否显示Grid滚动条
            mtype: "POST", // add by 何权洲 jq默认使用POST方式提交到服务端
            serializeGridData: function (postData) {
                var postDataWrapper = {page: {}};
                var gridPrmNames = jqObj.jqGrid("getGridParam", "prmNames");
                $.each(postData, function (k, val) {
                    var isExists = false;
                    $.each(gridPrmNames, function (gK, gVal) {
                        if (gVal === k) {
                            isExists = true;
                            return false;
                        }
                    });

                    if (!isExists) {
                        postDataWrapper[k] = val;
                        return true;
                    }
                    postDataWrapper.page[k] = val;
                });

                return JSON.stringify(postDataWrapper);
            },
            colModelTemplate : {width: 100, align: 'left', sortable: true},
            eventsStore:{ 'loadComplete':[]},
            loadComplete:function (xhr) {

                $.each(eventsStore['loadComplete'], function (index, val) {
                    val(xhr);
                });
            }
        }
    };
    grid.prototype.getGridHeightWithoutBdiv = function () {
        var hdiv = $(".ui-jqgrid-hdiv").filter(function (index, htmlObj) {
            return $(htmlObj).css('display') != 'none'
        });
        var pagerDiv = $(".ui-jqgrid-pager").filter(function (index, htmlObj) {
            return $(htmlObj).css('display') != 'none'
        });
        var captionDiv = $(".ui-jqgrid-caption").filter(function (index, htmlObj) {
            return $(htmlObj).css('display') != 'none'
        });
        return hdiv.length * hdiv.outerHeight(true) + pagerDiv.length * pagerDiv.outerHeight(true) + captionDiv.length * captionDiv.outerHeight(true);//$(".ui-jqgrid-hdiv").length * $(".ui-jqgrid-hdiv").outerHeight() - $(".ui-jqgrid-pager").length * $(".ui-jqgrid-pager").outerHeight()
    };
    grid.prototype.reloadGrid = function (option) {
        var defaults = {
            url: "",
            //   container: grid.Id,
            formID: "SearchForm",
            datatype: "json",
            page: 1,
            ReloadType: "normal"
        };
        var gridId = this.Id;
        var options = $.extend(defaults, option);
        if (options.ReloadType == "normal") {
            if (validate.isNullOrEmpty(option.url)) {
                $("#" + gridId).jqGrid("clearGridData");
                $("#" + gridId).jqGrid('setGridParam', {
                    datatype: 'local',
                    data: options.data
                }).trigger('reloadGrid', [{page: options.page}]);
            }
            else {

                // options.url = UrlHelper.GetRandURL(options.url);
                // var postJsonData = FormHelper.getPostBodyToJson(options.formID);
                // postJsonData = $.extend(postJsonData, option.postData);
                var postJsonData = option.postData;
                if ("nodata" === postJsonData) return;
                $("#" + gridId).jqGrid('setGridParam', {
                    url: options.url,
                    datatype: options.datatype,
                    page: options.page,
                    postData: postJsonData
                }).trigger("reloadGrid");
            }
        }
        else if (options.ReloadType == "dynamic") {
            var gridParam = $("#" + gridId).jqGrid('getGridParam');
            options.url = UrlHelper.GetRandURL(options.url);
            var postJsonData = FormHelper.getPostBodyToJson(options.formID);
            postJsonData = $.extend(postJsonData, options.postData);
            postJsonData = $.extend(gridParam.postData, postJsonData);
            postJsonData.page = 1;
            postJsonData.sidx = "";
            var gridID = grid.Id;
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
                        rows: function (object) {
                            return content.rows
                        },
                        page: function (object) {
                            return content.pagerInfo.page
                        },
                        total: function (object) {
                            return content.pagerInfo.total
                        },
                        records: function (object) {
                            return content.pagerInfo.records
                        },
                        repeatitems: false
                    }
                    $("#" + gridID).jqGrid('setGridParam', {
                        data: content.rows,
                        localReader: localReader
                    }).trigger('reloadGrid', [{page: options.page}]);
                    $("#" + gridID).jqGrid('setGridParam', {
                        url: options.url,
                        datatype: options.datatype,
                        page: options.page,
                        postData: postJsonData
                    })

                }
            });


        }

    };
    grid.prototype.delRowData = function (rowsId) {

        if (validate.isNullOrEmpty(rowsId) || !$.isArray(rowsId)) {
            throw new Error("请输入合适的id数组");
        }
        while (rowsId.length > 0) {
            $(grid.Id).jqGrid('delRowData', rowsId[0]);
        }
        var rowData = $(this.Id).jqGrid('getGridParam', 'selarrrow');
        if (rowData.length === 0) {
            $('#cb_' + $(this.Id).attr("id")).prop("checked", false);

        }
    };
    grid.prototype.addRowData = function (gridData) {
        var jqObj = $(this.Id);
        if (validate.isNullOrEmpty(gridData)) {
            throw new Error("请输入合适的Json数组");
        }
        var lastIndex = jqObj.jqGrid('getDataIDs').length;
        var result = 0;
        var idKey = jqObj.jqGrid("getGridParam", "idKey");
        for (var i = 0; i < gridData.length; i++) {
            if (validate.isNullOrEmpty(gridData[i][idKey])) {
                throw new Error("Grid数据中不包含列是" + idKey);
            }
            result = jqObj.jqGrid('addRowData', gridData[i][idKey], gridData[i], "After", lastIndex++) ? result + 1 : result;

        }
    };
    grid.prototype.saveRow = function (gridIds) {
        //if (validate.isNullOrEmpty(gridIds)) {
        //    throw "请输入合适的IDs数组";
        //}
        var jqObj = $(this.Id);
        var GridIds = validate.isNullOrEmpty(gridIds) ? jqObj.jqGrid("getDataIDs") : gridIds;
        var result = 0;
        for (var i = 0; i < GridIds.length; i++) {
            result = jqObj.jqGrid("saveRow", GridIds[i], null, 'clientArray') ? result + 1 : result;
        }
        //重置最后选择项目
        jqObj.attr("lastsel", "");
        return result;
    };
    grid.prototype.getRowData = function (gridIds, colNames) {
        var rowDataResult = [];
        var jqObj = $(this.Id);
        var isRownum = jqObj.jqGrid("getGridParam", "rownumbers");
        var isInputGirdIds = !validate.isNullOrEmpty(gridIds);
        isInputGirdIds = Array.isArray(gridIds) && gridIds.length < 1 ? true : isInputGirdIds;
        var gridData = jqObj.jqGrid("getRowData");
        var gridLength = isInputGirdIds ? gridIds.length : gridData.length;
        for (var i = 0; i < gridLength; i++) {
            var rowData = isInputGirdIds ? jqObj.jqGrid("getRowData", gridIds[i]) : gridData[i];
            if (isRownum) // TODO gridRowNum 计算不准确
                rowData.gridRowNum = i + 1;
            if (validate.isNullOrEmpty(colNames)) {
                rowDataResult.push(rowData);
                continue;
            }
            var jsonObj = {};
            for (var j = 0; j < colNames.length; j++) {
                var v = rowData[colNames[j]]
                if (validate.isNullOrEmpty(v)) {
                    v = "";
                }
                jsonObj[colNames[j]] = v;
            }
            rowDataResult.push(jsonObj);
        }


        return rowDataResult;
    };

    grid.prototype.getGridState = function () {
        //TODO 如果已非默认的方式打开grid，可能导致错误，
        var jqObj = $(this.Id);
        return jqObj.attr('gridstate');
    };


    grid.prototype.loadComplete = function (loadEventFun) {
        if ($.type(loadEventFun) !== 'function')
            throw new Error('loadEventFun must be function');
        this._options.eventsStore['loadComplete'].push(loadEventFun);
    };

    return grid;
});