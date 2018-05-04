define(['./gridbase','blocks_utility'], function (grid,utility) {
    var validate = utility.validate;
    var dataBuilder = function (gridObj) {
        initReader(gridObj);
        initDataColumn(gridObj);
        initEditColumn(gridObj);
    };

    grid.prototype.loadLocalData = function () {
        var $gridObj = this._options.gridObj;
        var firstOptions = $.extend(true,{}, this._options, {
            datatype: 'local'
        });
        $gridObj.jqGrid(firstOptions);
    };

    grid.prototype.loadJsonData = function () {
        var $gridObj = this._options.gridObj;
        $gridObj.jqGrid("setGridParam", {
            datatype: "json",
            url: this._options.url,
            postData: getPostData('QueryForm')
        });


    };
 

    function initReader(gridObj) {
        var options = gridObj._options;
        //ID字段名
        if (options.idKey !== undefined && options.idKey !== null) {
            options.jsonReader.id = options.idKey;
            options.localReader.id = options.idKey;

        }
    };
    function initDataColumn(gridObj) {
        var options = gridObj._options;
        var gridDisplayOptionsConfig = gridObj.config.body.gridDisplayOptions;
        for (var i = 0; i < options.colModel.length; i++) {
            var curColModel = options.colModel[i],gridDisplayOpt;
            curColModel.formatType = $.extend(true, {}, gridObj.config.body.formatType[curColModel.formatType.type], curColModel.formatType);
            if (curColModel.gridDisplayOptions && curColModel.gridDisplayOptions.type)
            {
                gridDisplayOpt = curColModel.gridDisplayOptions = $.extend(true, {},gridDisplayOptionsConfig[curColModel.displayType.type].default,
                    gridDisplayOptionsConfig[curColModel.displayType.type][curColModel.formatType.type],  curColModel.gridDisplayOptions);
            
            }
            else
            {
                gridDisplayOpt = curColModel.gridDisplayOptions = $.extend(true, {},gridDisplayOptionsConfig['default'].default,
                    gridDisplayOptionsConfig['default'][curColModel.formatType.type], curColModel.gridDisplayOptions);
            }
             
            if (gridDisplayOpt && !curColModel.formatter) {
                curColModel.formatter = gridDisplayOpt.formatter;
            }
            if (gridDisplayOpt && !curColModel.unformat) {
                curColModel.unformat = gridDisplayOpt.unformatter;
            }
            if (!curColModel.dataSource && gridObj.config.data.dataSource.hasOwnProperty(curColModel.formatType.type))
                  curColModel.dataSource =  gridObj.config.data.dataSource[curColModel.formatType.type];
        }
    }
    function initEditColumn(gridObj) {
        var options = gridObj._options;
        var editOptions =  gridObj.config.body.editOptions;
        var colModel = options.colModel;
        for (var i = 0; i < colModel.length; i++) {
            var editOpt = $.extend(true, {},editOptions[colModel[i].displayType.type].default,
                editOptions[colModel[i].displayType.type][colModel[i].formatType.type], colModel[i].editOptions);
            colModel[i].editoptions = colModel[i].editOptions = editOpt;
            colModel[i].edittype = colModel[i].editType = colModel[i].editType ?  colModel[i].editType : gridObj.config.body.editType[colModel[i].displayType.type];
        }
    }
    grid.prototype.reloadGrid = function (option) {
        var defaults = {
            url: "",
            //   container: grid.Id,
            formID: "SearchForm",
            datatype: "json",
            page: 1,
            ReloadType: "normal"
        };
        //option.showPager || validate.isNullOrEmpty() ? 10 : 999999,   //不显示分页栏时，默认显示全部,注意设-1时无法显示最后一行
        var gridObj = this._options.gridObj;
        var options = $.extend(defaults, option);
        if (options.ReloadType == "normal") {
            if (validate.isNullOrEmpty(option.url)) {
                gridObj.jqGrid("clearGridData");
                gridObj.jqGrid('setGridParam', {
                    datatype: 'local',
                    data: options.data,
                    rowNum:this._options.showPager === false ? 999999 : this._options.rowNum
                }).trigger('reloadGrid', [{page: options.page}]);
            }
            else {

                // options.url = UrlHelper.GetRandURL(options.url);
                // var postJsonData = FormHelper.getPostBodyToJson(options.formID);
                // postJsonData = $.extend(postJsonData, option.postData);
                var postJsonData = option.postData;
                if ("nodata" === postJsonData) return;
                gridObj.jqGrid('setGridParam', {
                    url: options.url,
                    datatype: options.datatype,
                    page: options.page,
                    postData: postJsonData,
                    rowNum:this._options.showPager === false ? -1 : this._options.rowNum
                }).trigger("reloadGrid");
            }
        }
        // else if (options.ReloadType == "dynamic") {
        //     var gridParam = $("#" + gridId).jqGrid('getGridParam');
        //     options.url = UrlHelper.GetRandURL(options.url);
        //     var postJsonData = FormHelper.getPostBodyToJson(options.formID);
        //     postJsonData = $.extend(postJsonData, options.postData);
        //     postJsonData = $.extend(gridParam.postData, postJsonData);
        //     postJsonData.page = 1;
        //     postJsonData.sidx = "";
        //     var gridID = grid.Id;
        //     AjaxHelper.submitJSONFormWithLoadDialog({
        //         URL: options.url, postJsonData: postJsonData, onSuccessCallBack: function (msg, content, code) {
        //             var currentGridParam = $.extend(true, {}, gridParam);
        //             currentGridParam = $.extend(currentGridParam, {
        //                 colNames: content.colNames,
        //                 colModel: content.colModel,
        //                 url: null,
        //                 datatype: 'local',
        //                 data: [],
        //                 sortname: ''
        //             });
        //             //gridUnload(gridID);
        //
        //             // $.jgrid.GridDestroy(gridID);
        //
        //             $.jgrid.gridUnload(gridID);
        //             //   $("#" + gridID).jqGrid('GridUnload'); 
        //             $("#" + gridID).grid(currentGridParam);
        //             var NewGridParam = content.pagerInfo;
        //             NewGridParam.data = content.rows;
        //             var localReader = {
        //                 //id: "id",//设置返回参数中，表格ID的名字为blackId  
        //                 rows: function (object) {
        //                     return content.rows
        //                 },
        //                 page: function (object) {
        //                     return content.pagerInfo.page
        //                 },
        //                 total: function (object) {
        //                     return content.pagerInfo.total
        //                 },
        //                 records: function (object) {
        //                     return content.pagerInfo.records
        //                 },
        //                 repeatitems: false
        //             }
        //             $("#" + gridID).jqGrid('setGridParam', {
        //                 data: content.rows,
        //                 localReader: localReader
        //             }).trigger('reloadGrid', [{page: options.page}]);
        //             $("#" + gridID).jqGrid('setGridParam', {
        //                 url: options.url,
        //                 datatype: options.datatype,
        //                 page: options.page,
        //                 postData: postJsonData
        //             })
        //
        //         }
        //     });
        //
        //
        // }

    };
    grid.prototype.dynamicConditionLoad = function (option) {
        var defaults = {};
        var gridObj = this._options.gridObj;
        if (this._options.dynamicConditionQuery.active === true) {
            this._options.gridObj.jqGrid('setGridParam', {
                url: option.url,
                postData: option.postData,
                datatype: "json",
            });
            this.getTopObj().find('#search_' + this._options.gridObj.attr('Id')).click();
        }


    };
    grid.prototype.delRowData = function (rowIds) {
        var jqObj = this._options.gridObj;
        if (validate.isNullOrEmpty(rowIds) || !$.isArray(rowIds)) {
            throw new Error("请输入合适的id数组");
            
        }
        for(var i = 0; i < rowIds.length; i++)
        {
            jqObj.jqGrid('delRowData', rowIds[i]);
        }
        if (this.getSelectRowIds().length === 0) {
            $('#cb_' + jqObj.attr("id")).prop("checked", false);
        }
    };
    grid.prototype.addRowData = function (rowDatas) {
        var jqObj = this._options.gridObj;
        if (validate.isNullOrEmpty(rowDatas)) {
            throw new Error("请输入合适的Json数组");
        }
        var lastIndex = jqObj.jqGrid('getDataIDs').length;
        var result = 0;
        var idKey = jqObj.jqGrid("getGridParam", "idKey");
        for (var i = 0; i < rowDatas.length; i++) {
            if (validate.isNullOrEmpty(rowDatas[i][idKey])) {
                throw new Error("Grid数据中不包含列是" + idKey);
            }
            result = jqObj.jqGrid('addRowData', rowDatas[i][idKey], rowDatas[i], "After", lastIndex++) ? result + 1 : result;

        }
    };
    grid.prototype.getRowData = function (rowIds, colNames) {
        var isFitercol = false;
        var isFilterrow = false;
        if (rowIds){ isFilterrow = true; validate.mustArray(rowIds);}
        if (colNames) {isFitercol = true;validate.mustArray(colNames);}
        var rowDataResult = [];
        var jqObj = this._options.gridObj;
        var isRownum = jqObj.jqGrid("getGridParam", "rownumbers");
        var actRowIds = isFilterrow ? rowIds : jqObj.jqGrid("getDataIDs");
        var length = isFilterrow ? rowIds.length : jqObj.getGridParam("records");
        for (var i = 0; i < length; i++) {
            var rowData = jqObj.jqGrid("getRowData",actRowIds[i]);
            if (validate.isObjectNull(rowData)) continue;
            if (isRownum) // TODO gridRowNum 计算不准确
                rowData.gridRowNum = i + 1;

            if (!isFitercol) {
                rowDataResult.push(rowData);
                continue;
            }
            rowDataResult.push(utility.obj.filter(rowData,colNames));
        }
     
        return rowDataResult;
    };
    grid.prototype.getOrginData = function (rowIds, colNames) {
        var isFitercol = false;
        var isFilterrow = false;
        if (rowIds){ isFilterrow = true; validate.mustArray(rowIds);}
        if (colNames) {isFitercol = true;validate.mustArray(colNames);}
        var rowDataResult = [];
        var jqObj = this._options.gridObj;
        var isRownum = jqObj.jqGrid("getGridParam", "rownumbers");
        var idKey =  jqObj.jqGrid("getGridParam", "idKey");
        var actRowIds = isFilterrow ? rowIds : jqObj.jqGrid("getDataIDs");
        var rowDatas =  $("#gridInfo").jqGrid('getGridParam','userData');

        var length = isFilterrow ? rowIds.length : rowDatas.length;
        for (var i = 0; i < length; i++) {
            var rowData = rowDatas.filter(function (arrayItem) {
               return actRowIds[i] === arrayItem[idKey];
            });
            if (validate.isObjectNull(rowData)) continue;
            if (isRownum) // TODO gridRowNum 计算不准确
                rowData.gridRowNum = i + 1;

            if (!isFitercol) {
                rowDataResult.push(rowData);
                continue;
            }
            rowDataResult.push(utility.obj.filter(rowData,colNames));
        }

        return rowDataResult;
    };
    grid.prototype.setCell = function(e,t,i,r,o,a){
        var jqObj = this._options.gridObj;
        return jqObj.jqGrid("setCell", e, t, i, r, o, a);
    };
    return dataBuilder;
});