define(['jquery','../../datepicker','blocks_utility'],function ($,datepicker,utility) {

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
                closeAfterSearch: true
            }
        },
        'eventsStore': {
            'loadComplete': [], 'onSelectRow': []
        },
        'body': {
            'default': {
                'colModel': {width: 100, align: 'left', sortable: true, datatype: {type: 'string', format: ''}},
                'multiselectEdit': false
            },
            'datetype': {'date': {type: 'date', format: 'yyyy-MM-DD'}},
            'searchoptions': {
                'date': {
                    dataInit: function (elem) {
                        new datepicker({viewObj: $(elem)});
                    }, attr: {title: 'Select Date'}, sopt: ['ge', 'le']
                },
                'string': {
                    sopt: ['eq', 'ne']
                },
                'number': { 
                    sopt: ['ge', 'le']
                },
                'select': {
                    sopt: ['cn']
                },
            }
        }
    }
    
    
     return config;
});