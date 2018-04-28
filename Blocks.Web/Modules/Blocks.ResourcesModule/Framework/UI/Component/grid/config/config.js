define(['jquery', '../../datepicker', 'blocks_utility', '../../dialog','../../select'], function ($, datepicker, utility, dialog,select) {
    var utility = utility;
    var config = {
        'default': {
            ajaxGridOptions: {
                type: "POST",
                cache: false,
                async: true,
                contentType: 'application/json',
                error: function () {
                    dialog.error({content: "意外出错，请刷新页面重试"});
                }
            },
            loadui: "block",
            datatype: "json",
            gridview: true,
            viewrecords: true,
            multiselect: true,
            multiselectEdit: false,
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
            prmNames: {
                rows: "pageSize",
            },
            localReader: {
                id: "ID"
            },
            loadError: function () {
                dialog.error({content: "网络出错，请刷新页面重试"});
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
            // onSelectRow: function (rowid, status) {
            //
            // },
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
            // loadCompleteOnFaildCallBack: function () {
            // },
            gridResize: null, //设置Grid的大小
            gridScroll: false, //是否显示Grid滚动条
            mtype: "POST", // add by 何权洲 jq默认使用POST方式提交到服务端
            serializeGridData: function (postData) {
                var postDataWrapper = {page: {}};
                var gridPrmNames = $(this).jqGrid("getGridParam", "prmNames");
                gridPrmNames['filters'] = 'filters';
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
                if (postDataWrapper.page.hasOwnProperty('filters')) {
                    postDataWrapper.page['filters'] = utility.Json.parse(postDataWrapper.page.filters);
                    //  postDataWrapper.page['filters'].source = 
                }
                return utility.Json.stringify(postDataWrapper);
            },

            // loadComplete: function (xhr) {
            //    
            // },
            dynamicConditionQuery: {
                active: false, multipleSearch: true, multipleGroup: false, showQuery: false,
                width: '60%',
                closeAfterSearch: true
            }
        },
        'eventsStore': {
            'loadComplete': [], 'onSelectRow': [], 'resizeStop': [], 'beforeShowSearch': []
        },
        'body': {
            'default': {
                'colModel': {
                    width: 100, align: 'left', sortable: true, datatype: {type: 'string'},
                    displaytype: {type: 'text'}, datasource: undefined
                },
                'multiselectEdit': false
            },
            'searchoptions': {
                'date': {
                    'default': {
                        dataInit: function (elem) {
                            var picker = new datepicker({viewObj: $(elem)});
                            picker.on('onpicked oncleared', function () {
                                $($dp.el).trigger('change');
                            });
                            picker.on('onhided', function () {
                                $(this.el).trigger('change');
                            });
                        }, sopt: ['ge', 'le']
                    }
                },
                'text': {
                    'default': {
                        sopt: ['eq', 'ne']
                    },
                    'number': {sopt: ['ge', 'le']}
                },
                'select': {
                    'default': {
                        sopt: ['cn']
                    }
                },
                'checkbox': {
                    'default': {
                        dataInit: function (elem) {
                            var $elem = $(elem);
                            $elem.removeClass('form-control');
                            var $checkbox = $elem.children();
                            $("<label for='" + $checkbox.attr("id") + "'></label>").insertAfter($checkbox)
                        }, sopt: ['eq'], custom_element: function (t, elem, type) {
                            return '<input type="checkbox" >';
                        }, custom_value: function (elem, type) {
                            return $(elem).prop('checked') === true ? 1 : 0;
                        }
                    }
                }
            },
            'searchType': {
                'checkbox': 'custom'
            },
            'edittype': {
                'date': 'text',
                'checkbox': 'custom',
                'select': 'custom'
            },
            'editoptions': {
                'date': {
                    'default': {
                        dataInit: function (elem, cellModel) {
                            var picker = new datepicker({viewObj: $(elem)});
                            picker.on('onpicked oncleared', function () {
                                $($dp.el).trigger('change');
                            });

                            picker.on('onhided', function () {
                                $(this.el).trigger('change');
                            });

                            var curModel = $(this).jqGrid('getGridParam', 'colModel').filter(function (colmodel) {
                                return colmodel.name === cellModel.name;
                            });
                            if (!curModel || curModel.length === 0)
                                throw new Error('Not found curModel ' + cellModel.name + '.Please check Colmodel');
                            var oldVal = $(elem).val();

                            $(elem).val(utility.dateConvert.format(oldVal, curModel[0].datatype.desformat))
                        }, custom_element: function (elem) {
                            return '<input >';
                        }, custom_value: function (a, b, c) {

                        }
                    }
                },
                'text': {
                    'default': {
                        sopt: ['eq', 'ne']
                    },
                    'number': {sopt: ['ge', 'le']}
                },
                'select': {
                    'default': {
                        dataInit: function (elem, cellModel) {
                            
                            // var oldVal = $(elem).val();
                            // selectComponent.val("XXXXX").trigger("change");
                        
                            // 
                            //
                            // $(elem).val(utility.dateConvert.format(oldVal, curModel[0].datatype.desformat))
                        }, custom_element: function (value, cellModel) {

                            var curModel = $(this).jqGrid('getGridParam', 'colModel').filter(function (colmodel) {
                                return colmodel.name === cellModel.name;
                            });
                            if (!curModel || curModel.length === 0)
                                throw new Error('Not found curModel ' + cellModel.name + '.Please check Colmodel');
                            var sourceObj = $('<select id="'+ cellModel.id+'" />');
                            var selectComponent = new select({
                                viewObj:sourceObj ,
                                data: curModel[0].datasource,
                                isCombobox:false
                                //  url:"/api/services/BussnessWebModule/Combobox/GetComboboxList"
                            });
                            sourceObj.val(value).trigger("change");
                            var parentObj = $("<div />");
                            parentObj.append(sourceObj.next());
                            parentObj.append(sourceObj);
                            return parentObj;
                        }, custom_value: function (a, b, c) {

                        }
                    }
                },
                'checkbox': {
                    'default': {
                        dataInit: function (elem) {
                            var $elem = $(elem);
                            $elem.removeClass('form-control');
                            var $checkbox = $elem.children();
                            $("<label for='" + $checkbox.attr("id") + "'></label>").insertAfter($checkbox)
                        }, custom_element: function (t, elem, type) {
                            return '<input type="checkbox" >';

                        }, custom_value: function (elem, type) {
                            return $(elem).prop('checked') === true ? 1 : 0;
                        }
                    }
                }
            },
        },
        'data': {
            'default': {},
            'dataFormat': {
                'date': {
                    srcformat: 'yyyy/MM/DD HH:mm:ss',
                    desformat: 'yyyy/MM/DD HH:mm:ss',
                    formatter: function (cellvalue, options, rowObject) {
                        return utility.dateConvert.format(cellvalue, options.colModel.datatype.desformat);
                    },
                    unformatter: function (cellvalue, options, rowObject) {
                        return utility.dateConvert.toUtcDate(cellvalue);
                    }
                },
                'bool': {
                  //  'format': {1: '是', 0: '否'}, 'unFormat': {'是': 1, '否': 0},
                    formatter: function (cellvalue, options, rowObject) {
                        
                        var curModelDatasource = options.colModel.datasource;
                        utility.validate.mustArray(curModelDatasource,'colModel.datasource')
                        var curCell = curModelDatasource.filter(function (v) {
                            return v.id === cellvalue;
                        });
                        if (!curCell || curCell.length === 0)
                            throw new Error('Cellvalue [' + cellvalue + '] not found in datasource');
                        return curCell[0].Text;
                    }, unformatter: function (cellvalue, options, rowObject) {

                        var curModelDatasource = options.colModel.datasource;
                        utility.validate.mustArray(curModelDatasource,'colModel.datasource')
                        var curCell = curModelDatasource.filter(function (v) {
                            return v.Text === cellvalue;
                        });
                        if (!curCell|| curCell.length === 0)
                            throw new Error('Cellvalue [' + cellvalue + '] not found in datasource');
                        return curCell[0].Id;
                    }
                },

            },
            'dataSource': {
                'bool': [{ id:1, Text:'是'},{ id:0, Text:'否'}],
            }

        }

    }


    return config;
});