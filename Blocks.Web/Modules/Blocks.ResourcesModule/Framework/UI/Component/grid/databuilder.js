define(['./gridbase','blocks_utility','jquery','./extensions/checkboxPlugin'], function (grid,utility,$,checkboxHanlder) {
    var validate = utility.validate;
    var dataBuilder = function (gridObj) {
        initReader(gridObj);
        initDataColumn(gridObj);
        initEditColumn(gridObj);
        
        initCellValidate(gridObj);
      //  gridObj._options.dialogParentSelector = "dia_jq" + gridObj._options.gridObj.attr('id') + ~~(Math.random() * 1000000);
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
            var curColModel = options.colModel[i], gridDisplayOpt;
            curColModel.formatType = $.extend(true, {}, gridObj.config.body.formatType[curColModel.formatType.type], curColModel.formatType);
            if (curColModel.gridDisplayOptions && curColModel.gridDisplayOptions.type)
            {
                gridDisplayOpt = curColModel.gridDisplayOptions = $.extend(true, {   },gridDisplayOptionsConfig[curColModel.displayType.type].default,
                    gridDisplayOptionsConfig[curColModel.displayType.type][curColModel.formatType.type],
                    {isRemote:curColModel.isRemote,url:curColModel.url},
                    curColModel.gridDisplayOptions);
            
            }
            else
            {
                gridDisplayOpt = curColModel.gridDisplayOptions = $.extend(true, {  },gridDisplayOptionsConfig['default'].default,
                   gridDisplayOptionsConfig['default'][curColModel.formatType.type],
                    {isRemote:curColModel.isRemote,url:curColModel.url },
                    curColModel.gridDisplayOptions);
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
                editOptions[colModel[i].displayType.type][colModel[i].formatType.type],
                {isRemote:colModel[i].isRemote,url:colModel[i].url },
                colModel[i].editOptions);
           colModel[i].editoptions =  colModel[i].editOptions = editOpt;
            colModel[i].edittype = colModel[i].editType = colModel[i].editType ?  colModel[i].editType : gridObj.config.body.editType[colModel[i].displayType.type];
        }
    }
    
    function initCellValidate(gridObj){
        var options = gridObj._options;
        
        options.validationCell = function(cellObj,msg,rowIndex,cellIndex){
            var p = this;
            var x= $.jgrid.getRegional(p, "errors");
            var _= $.jgrid.getRegional(p, "edit");
            var row = p.p.data[rowIndex - 1];
            var rowId = row["ID"] ? row["ID"] : (row["id"] ? row["id"] : "") ;
            var C =$(p).jqGrid("getGridRowById", rowId), F =$.jgrid.findPos(C);

            var b = $(p).parents("*[role=dialog]").filter(":first").css("z-index");
            
            $.jgrid.info_dialog(x.errcap, msg, _.bClose, {
                left: F[0],
                top: F[1] + $(C).outerHeight(),
                styleUI: p.p.styleUI,
                zIndex:b ? parseInt(b, 10) + 2 : 950,
                onClose: function() {
                    cellIndex >= 0 && $(this).find("#" + rowId + "_" + p.p.colModel[cellIndex].name).focus()
                }
            })
            
            
        };
        
        
        
        
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
                datatype: "json"
            });
            var navGridData = gridObj.data('navGrid');
            //if ($("#" + this._options.dialogParentSelector).length < 1)
            //{ 
            //    $('body').append('<div id="' + this._options.dialogParentSelector + '"></div>');
            //}

          //  var searchOptions = $.extend(true, {}, this._options.dynamicConditionQuery, { layer: this._options.dialogParentSelector} );
            var searchOptions = this._options.dynamicConditionQuery;

            if ($.isFunction(navGridData.searchfunc))
                navGridData.searchfunc.call(gridObj, searchOptions);
            else
            {
               // var jqGridTopObj = this.getTopObj();
                //searchGrid.apply(gridObj, [searchOptions, jqGridTopObj]);
                gridObj.jqGrid("searchGrid", searchOptions);

            }
          //  this.getTopObj().find('#search_' + this._options.gridObj.attr('Id')).click();
        }


    };

    //function searchGrid(p,$parentView) {
    //    var regional = $.jgrid.getRegional(this[0], 'search');
    //    p = $.extend(true, {
    //        recreateFilter: false,
    //        drag: true,
    //        sField: 'searchField',
    //        sValue: 'searchString',
    //        sOper: 'searchOper',
    //        sFilter: 'filters',
    //        loadDefaults: true, // this options activates loading of default filters from grid's postData for Multipe Search only.
    //        beforeShowSearch: null,
    //        afterShowSearch: null,
    //        onInitializeSearch: null,
    //        afterRedraw: null,
    //        afterChange: null,
    //        sortStrategy: null,
    //        closeAfterSearch: false,
    //        closeAfterReset: false,
    //        closeOnEscape: false,
    //        searchOnEnter: false,
    //        multipleSearch: false,
    //        multipleGroup: false,
    //        //cloneSearchRowOnAdd: true,
    //        top: 0,
    //        left: 0,
    //        jqModal: true,
    //        modal: false,
    //        resize: true,
    //        width: 450,
    //        height: 'auto',
    //        dataheight: 'auto',
    //        showQuery: false,
    //        errorcheck: true,
    //        sopt: null,
    //        stringResult: undefined,
    //        onClose: null,
    //        onSearch: null,
    //        onReset: null,
    //        toTop: true,
    //        overlay: 30,
    //        columns: [],
    //        tmplNames: null,
    //        tmplFilters: null,
    //        tmplLabel: ' Template: ',
    //        showOnLoad: false,
    //        layer: null,
    //        splitSelect: ",",
    //        groupOpSelect: "OR",
    //        operands: { "eq": "=", "ne": "<>", "lt": "<", "le": "<=", "gt": ">", "ge": ">=", "bw": "LIKE", "bn": "NOT LIKE", "in": "IN", "ni": "NOT IN", "ew": "LIKE", "en": "NOT LIKE", "cn": "LIKE", "nc": "NOT LIKE", "nu": "IS NULL", "nn": "ISNOT NULL" },
    //        buttons: []
    //    }, regional, p || {});
    //    return this.each(function () {
    //        var $t = this;
    //        if (!$t.grid) { return; }
    //        var fid = "fbox_" + $t.p.id,
    //            showFrm = true,
    //            mustReload = true,
    //            IDs = { themodal: 'searchmod' + fid, modalhead: 'searchhd' + fid, modalcontent: 'searchcnt' + fid, scrollelm: fid },
    //            defaultFilters = ($.isPlainObject($t.p_savedFilter) && !$.isEmptyObject($t.p_savedFilter)) ? $t.p_savedFilter : $t.p.postData[p.sFilter],
    //            fl,
    //            classes = $.jgrid.styleUI[($t.p.styleUI || 'jQueryUI')].filter,
    //            common = $.jgrid.styleUI[($t.p.styleUI || 'jQueryUI')].common;
    //        p.styleUI = $t.p.styleUI;
    //        if (typeof defaultFilters === "string") {
    //            defaultFilters = $.jgrid.parse(defaultFilters);
    //        }
    //        if (p.recreateFilter === true) {
    //            $parentView.find("#" + $.jgrid.jqID(IDs.themodal)).each(function (n, i) {
    //                $(n).remove();
    //            });
    //        }
    //        function showFilter(_filter) {
    //            showFrm = $($t).triggerHandler("jqGridFilterBeforeShow", [_filter]);
    //            if (showFrm === undefined) {
    //                showFrm = true;
    //            }
    //            if (showFrm && $.isFunction(p.beforeShowSearch)) {
    //                showFrm = p.beforeShowSearch.call($t, _filter);
    //            }
    //            if (showFrm) {
    //                $.jgrid.viewModal($parentView.find("#" + $.jgrid.jqID(IDs.themodal)), { gbox: "#gbox_" + $.jgrid.jqID($t.p.id), jqm: p.jqModal, modal: p.modal, overlay: p.overlay, toTop: p.toTop });
    //                $($t).triggerHandler("jqGridFilterAfterShow", [_filter]);
    //                if ($.isFunction(p.afterShowSearch)) {
    //                    p.afterShowSearch.call($t, _filter);
    //                }
    //            }
    //        }
    //        if ($parentView.find("#" + $.jgrid.jqID(IDs.themodal))[0] !== undefined) {
    //            showFilter($parentView.find("#fbox_" + $.jgrid.jqID($t.p.id)));
    //        } else {
    //            var fil = $("<div><div id='" + fid + "' class='searchFilter' style='overflow:auto'></div></div>").insertBefore("#gview_" + $.jgrid.jqID($t.p.id)),
    //                align = "left", butleft = "";
    //            if ($t.p.direction === "rtl") {
    //                align = "right";
    //                butleft = " style='text-align:left'";
    //                fil.attr("dir", "rtl");
    //            }
    //            var columns = $.extend([], $t.p.colModel),
    //                bS = "<a id='" + fid + "_search' class='fm-button " + common.button + " fm-button-icon-right ui-search'><span class='" + common.icon_base + " " + classes.icon_search + "'></span>" + p.Find + "</a>",
    //                bC = "<a id='" + fid + "_reset' class='fm-button " + common.button + " fm-button-icon-left ui-reset'><span class='" + common.icon_base + " " + classes.icon_reset + "'></span>" + p.Reset + "</a>",
    //                bQ = "", tmpl = "", colnm, found = false, bt, cmi = -1, ms = false, ssfield = [];
    //            if (p.showQuery) {
    //                bQ = "<a id='" + fid + "_query' class='fm-button " + common.button + " fm-button-icon-left'><span class='" + common.icon_base + " " + classes.icon_query + "'></span>Query</a>";
    //            }
    //            var user_buttons = $.jgrid.buildButtons(p.buttons, bQ + bS, common);
    //            if (!p.columns.length) {
    //                $.each(columns, function (i, n) {
    //                    if (!n.label) {
    //                        n.label = $t.p.colNames[i];
    //                    }
    //                    // find first searchable column and set it if no default filter
    //                    if (!found) {
    //                        var searchable = (n.search === undefined) ? true : n.search,
    //                            hidden = (n.hidden === true),
    //                            ignoreHiding = (n.searchoptions && n.searchoptions.searchhidden === true);
    //                        if ((ignoreHiding && searchable) || (searchable && !hidden)) {
    //                            found = true;
    //                            colnm = n.index || n.name;
    //                            cmi = i;
    //                        }
    //                    }
    //                    if (n.stype === "select" && n.searchoptions && n.searchoptions.multiple) {
    //                        ms = true;
    //                        ssfield.push(n.index || n.name);
    //                    }
    //                });
    //            } else {
    //                columns = p.columns;
    //                cmi = 0;
    //                colnm = columns[0].index || columns[0].name;
    //            }
    //            // old behaviour
    //            if ((!defaultFilters && colnm) || p.multipleSearch === false) {
    //                var cmop = "eq";
    //                if (cmi >= 0 && columns[cmi].searchoptions && columns[cmi].searchoptions.sopt) {
    //                    cmop = columns[cmi].searchoptions.sopt[0];
    //                } else if (p.sopt && p.sopt.length) {
    //                    cmop = p.sopt[0];
    //                }
    //                defaultFilters = { groupOp: "AND", rules: [{ field: colnm, op: cmop, data: "" }] };
    //            }
    //            found = false;
    //            if (p.tmplNames && p.tmplNames.length) {
    //                found = true;
    //                tmpl = "<tr><td class='ui-search-label'>" + p.tmplLabel + "</td>";
    //                tmpl += "<td><select class='ui-template " + classes.srSelect + "'>";
    //                tmpl += "<option value='default'>Default</option>";
    //                $.each(p.tmplNames, function (i, n) {
    //                    tmpl += "<option value='" + i + "'>" + n + "</option>";
    //                });
    //                tmpl += "</select></td></tr>";
    //            }

    //            bt = "<table class='EditTable' style='border:0px none;margin-top:5px' id='" + fid + "_2'><tbody><tr><td colspan='2'><hr class='" + common.content + "' style='margin:1px'/></td></tr>" + tmpl + "<tr><td class='EditButton' style='text-align:" + align + "'>" + bC + "</td><td class='EditButton' " + butleft + ">" + user_buttons + "</td></tr></tbody></table>";
    //            fid = $.jgrid.jqID(fid);
    //            $parentView.find("#" + fid).jqFilter({
    //                columns: columns,
    //                sortStrategy: p.sortStrategy,
    //                filter: p.loadDefaults ? defaultFilters : null,
    //                showQuery: p.showQuery,
    //                errorcheck: p.errorcheck,
    //                sopt: p.sopt,
    //                groupButton: p.multipleGroup,
    //                ruleButtons: p.multipleSearch,
    //                uniqueSearchFields: p.uniqueSearchFields,
    //                afterRedraw: p.afterRedraw,
    //                ops: p.odata,
    //                operands: p.operands,
    //                ajaxSelectOptions: $t.p.ajaxSelectOptions,
    //                groupOps: p.groupOps,
    //                addsubgrup: p.addsubgrup,
    //                addrule: p.addrule,
    //                delgroup: p.delgroup,
    //                delrule: p.delrule,
    //                autoencode: $t.p.autoencode,
    //                onChange: function () {
    //                    if (this.p.showQuery) {
    //                        $('.query', this).html(this.toUserFriendlyString());
    //                    }
    //                    if ($.isFunction(p.afterChange)) {
    //                        p.afterChange.call($t, $("#" + fid), p);
    //                    }
    //                },
    //                direction: $t.p.direction,
    //                id: $t.p.id
    //            });
    //            fil.append(bt);
    //            $parentView.find("#" + fid + "_2").find("[data-index]").each(function () {
    //                var index = parseInt($(this).attr('data-index'), 10);
    //                if (index >= 0) {
    //                    $(this).on('click', function (e) {
    //                        p.buttons[index].click.call($t, $parentView.find("#" + fid), p, e);
    //                    });
    //                }
    //            });
    //            if (found && p.tmplFilters && p.tmplFilters.length) {
    //                $(".ui-template", fil).on('change', function () {
    //                    var curtempl = $(this).val();
    //                    if (curtempl === "default") {
    //                        $parentView.find("#" + fid).jqFilter('addFilter', defaultFilters);
    //                    } else {
    //                        $parentView.find("#" + fid).jqFilter('addFilter', p.tmplFilters[parseInt(curtempl, 10)]);
    //                    }
    //                    return false;
    //                });
    //            }
    //            if (p.multipleGroup === true) { p.multipleSearch = true; }
    //            $($t).triggerHandler("jqGridFilterInitialize", [$("#" + fid)]);
    //            if ($.isFunction(p.onInitializeSearch)) {
    //                p.onInitializeSearch.call($t, $("#" + fid));
    //            }
    //            p.gbox = $parentView.find("#gbox_" + fid);
    //            if (p.layer) {
    //                $.jgrid.createModal(IDs, fil, p, $parentView.find("#gview_" + $.jgrid.jqID($t.p.id)), $parentView.find("#gbox_" + $.jgrid.jqID($t.p.id))[0], (typeof p.layer === "string" ? "#" + $.jgrid.jqID(p.layer) : p.layer), (typeof p.layer === "string" ? { position: "relative" } : {}));
    //            } else {
    //                $.jgrid.createModal(IDs, fil, p, $parentView.find("#gview_" + $.jgrid.jqID($t.p.id)), $parentView.find("#gbox_" + $.jgrid.jqID($t.p.id))[0]);
    //            }
    //            if (p.searchOnEnter || p.closeOnEscape) {
    //                $("#" + $.jgrid.jqID(IDs.themodal)).keydown(function (e) {
    //                    var $target = $(e.target);
    //                    if (p.searchOnEnter && e.which === 13 && // 13 === $.ui.keyCode.ENTER
    //                        !$target.hasClass('add-group') && !$target.hasClass('add-rule') &&
    //                        !$target.hasClass('delete-group') && !$target.hasClass('delete-rule') &&
    //                        (!$target.hasClass("fm-button") || !$target.is("[id$=_query]"))) {
    //                        $("#" + fid + "_search").click();
    //                        return false;
    //                    }
    //                    if (p.closeOnEscape && e.which === 27) { // 27 === $.ui.keyCode.ESCAPE
    //                        $("#" + $.jgrid.jqID(IDs.modalhead)).find(".ui-jqdialog-titlebar-close").click();
    //                        return false;
    //                    }
    //                });
    //            }
    //            if (bQ) {
    //                $("#" + fid + "_query").on('click', function () {
    //                    $(".queryresult", fil).toggle();
    //                    return false;
    //                });
    //            }
    //            if (p.stringResult === undefined) {
    //                // to provide backward compatibility, inferring stringResult value from multipleSearch
    //                p.stringResult = p.multipleSearch;
    //            }
    //            $("#" + fid + "_search").on('click', function () {
    //                var sdata = {}, res, filters;
    //                fl = $("#" + fid);
    //                fl.find(".input-elm:focus").change();
    //                if (ms && p.multipleSearch) {
    //                    $t.p_savedFilter = {};
    //                    filters = $.jgrid.filterRefactor({
    //                        ruleGroup: $.extend(true, {}, fl.jqFilter('filterData')),
    //                        ssfield: ssfield,
    //                        splitSelect: p.splitSelect,
    //                        groupOpSelect: p.groupOpSelect
    //                    });
    //                    $t.p_savedFilter = $.extend(true, {}, fl.jqFilter('filterData'));
    //                } else {
    //                    filters = fl.jqFilter('filterData');
    //                }
    //                if (p.errorcheck) {
    //                    fl[0].hideError();
    //                    if (!p.showQuery) { fl.jqFilter('toSQLString'); }
    //                    if (fl[0].p.error) {
    //                        fl[0].showError();
    //                        return false;
    //                    }
    //                }

    //                if (p.stringResult) {
    //                    try {
    //                        res = JSON.stringify(filters);
    //                    } catch (e2) { }
    //                    if (typeof res === "string") {
    //                        sdata[p.sFilter] = res;
    //                        $.each([p.sField, p.sValue, p.sOper], function () { sdata[this] = ""; });
    //                    }
    //                } else {
    //                    if (p.multipleSearch) {
    //                        sdata[p.sFilter] = filters;
    //                        $.each([p.sField, p.sValue, p.sOper], function () { sdata[this] = ""; });
    //                    } else {
    //                        sdata[p.sField] = filters.rules[0].field;
    //                        sdata[p.sValue] = filters.rules[0].data;
    //                        sdata[p.sOper] = filters.rules[0].op;
    //                        sdata[p.sFilter] = "";
    //                    }
    //                }
    //                $t.p.search = true;
    //                $.extend($t.p.postData, sdata);
    //                mustReload = $($t).triggerHandler("jqGridFilterSearch");
    //                if (mustReload === undefined) {
    //                    mustReload = true;
    //                }
    //                if (mustReload && $.isFunction(p.onSearch)) {
    //                    mustReload = p.onSearch.call($t, $t.p.filters);
    //                }
    //                if (mustReload !== false) {
    //                    $($t).trigger("reloadGrid", [{ page: 1 }]);
    //                }
    //                if (p.closeAfterSearch) {
    //                    $.jgrid.hideModal("#" + $.jgrid.jqID(IDs.themodal), { gb: "#gbox_" + $.jgrid.jqID($t.p.id), jqm: p.jqModal, onClose: p.onClose });
    //                }
    //                return false;
    //            });
    //            $("#" + fid + "_reset").on('click', function () {
    //                var sdata = {},
    //                    fl = $("#" + fid);
    //                $t.p.search = false;
    //                $t.p.resetsearch = true;
    //                if (p.multipleSearch === false) {
    //                    sdata[p.sField] = sdata[p.sValue] = sdata[p.sOper] = "";
    //                } else {
    //                    sdata[p.sFilter] = "";
    //                }
    //                fl[0].resetFilter();
    //                if (found) {
    //                    $(".ui-template", fil).val("default");
    //                }
    //                $.extend($t.p.postData, sdata);
    //                mustReload = $($t).triggerHandler("jqGridFilterReset");
    //                if (mustReload === undefined) {
    //                    mustReload = true;
    //                }
    //                if (mustReload && $.isFunction(p.onReset)) {
    //                    mustReload = p.onReset.call($t);
    //                }
    //                if (mustReload !== false) {
    //                    $($t).trigger("reloadGrid", [{ page: 1 }]);
    //                }
    //                if (p.closeAfterReset) {
    //                    $.jgrid.hideModal("#" + $.jgrid.jqID(IDs.themodal), { gb: "#gbox_" + $.jgrid.jqID($t.p.id), jqm: p.jqModal, onClose: p.onClose });
    //                }
    //                return false;
    //            });
    //            showFilter($("#" + fid));
    //            $(".fm-button:not(." + common.disabled + ")", fil).hover(
    //                function () { $(this).addClass(common.hover); },
    //                function () { $(this).removeClass(common.hover); }
    //            );
    //        }
    //    });
    //}

    //function viewModal(selector, o) {
    //    o = $.extend({
    //        toTop: true,
    //        overlay: 10,
    //        modal: false,
    //        overlayClass: 'ui-widget-overlay', // to be fixed
    //        onShow: $.jgrid.showModal,
    //        onHide: $.jgrid.closeModal,
    //        gbox: '',
    //        jqm: true,
    //        jqM: true
    //    }, o || {});
    //    var style = "";
    //    if (o.gbox) {
    //        var grid = $("#" + o.gbox.substring(6))[0];
    //        try {
    //            style = $(grid).jqGrid('getStyleUI', grid.p.styleUI + '.common', 'overlay', false, 'jqgrid-overlay-modal');
    //            o.overlayClass = $(grid).jqGrid('getStyleUI', grid.p.styleUI + '.common', 'overlay', true);
    //        } catch (em) { }
    //    }
    //    if (o.focusField === undefined) {
    //        o.focusField = 0;
    //    }
    //    if (typeof o.focusField === "number" && o.focusField >= 0) {
    //        o.focusField = parseInt(o.focusField, 10);
    //    } else if (typeof o.focusField === "boolean" && !o.focusField) {
    //        o.focusField = false;
    //    } else {
    //        o.focusField = 0;
    //    }
    //    if ($.fn.jqm && o.jqm === true) {
    //        if (o.jqM) { $(selector).attr("aria-hidden", "false").jqm(o).jqmShow(); }
    //        else { $(selector).attr("aria-hidden", "false").jqmShow(); }
    //    } else {
    //        if (o.gbox !== '') {
    //            var zInd = parseInt($(selector).css("z-index")) - 1;
    //            if (o.modal) {
    //                if (!$(".jqgrid-overlay-modal")[0]) {
    //                    $('body').prepend("<div " + style + "></div>");
    //                }
    //                $(".jqgrid-overlay-modal").css("z-index", zInd).show();
    //            } else {
    //                $(".jqgrid-overlay:first", o.gbox).css("z-index", zInd).show();
    //                $(selector).data("gbox", o.gbox);
    //            }
    //        }
    //        $(selector).show().attr("aria-hidden", "false");
    //        if (o.focusField >= 0) {
    //            try { $(':input:visible', selector)[o.focusField].focus(); } catch (_) { }
    //        }
    //    }
    //}
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
        this.getTopObj().find("input:checkbox").each(function (i) {
            checkboxHanlder.call(this,i);

        })
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