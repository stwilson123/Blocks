define(['./gridbase','./view/headbuilder','./view/bodybuilder','./view/pagebuilder','./view/searchbuilder','../../../Design/decorator'],function (grid,headBuilder,bodyBuilder,pageBuilder,searchBuilder,decoratorPattern) {

    var viewDirector = function (gridObj) {
        var head = new headBuilder(gridObj);
        var body = new bodyBuilder(gridObj);
        var page = new pageBuilder(gridObj);
        var search = new searchBuilder(gridObj);
        
        gridObj.head = head;
        gridObj.body = body;
        gridObj.page = page;
        gridObj.search = search;
        
        {
            var $gridTop = gridObj.getTopObj();
            gridObj.on('resizeStop',function () {
                $gridTop.find('.ui-jqgrid-htable').width('100%');
                $gridTop.find('.ui-jqgrid-btable').width('100%');
            });
            gridObj.on('loadComplete',function () {
                $gridTop.find('.ui-jqgrid-btable').width('100%');
            });
        }
    };

    decoratorPattern.func.call(grid.prototype,'init',function () {
        if (this._options.datatype === "local" || (!this._options.url)) {
            this.loadLocalData();
        } else {
            this.loadJsonData();
        }
        //initDynamicSelect.call(this, this._options);
    });
    decoratorPattern.func.call(grid.prototype,'afterInit',function () {
        //TODO auto width 
        {
            var $gridTop = this.getTopObj();
            $gridTop.find('.jqgrid-overlay').width('100%');
            $gridTop.find('.ui-jqgrid-view').width('100%');
            $gridTop.find('.ui-jqgrid-hdiv').width('100%');
            $gridTop.find('.ui-jqgrid-htable').width('100%');
            $gridTop.find('.ui-jqgrid-bdiv').width('100%').css('overflow-y', 'scroll');
            $gridTop.find('.ui-jqgrid-btable').width('100%');
            $gridTop.find('.ui-jqgrid-pager').width('100%');
            //TODO want to set 100%,but always plus 2% 
            $gridTop.width('98%');
            $gridTop.find('#' + this._options.gridObj.attr('id') + '_cb').css('text-align', 'center');
        }
    });
    grid.prototype.getGridHeightWithoutBdiv = function () {
        var jqGridObj = this._options.gridObj;
        var jqGridTopObj = jqGridObj.parent().parent().parent().parent();
        var hdiv = jqGridTopObj.find(".ui-jqgrid-hdiv").filter(function (index, htmlObj) {
            return $(htmlObj).css('display') != 'none'
        });
        var pagerDiv = jqGridTopObj.find(".ui-jqgrid-pager").filter(function (index, htmlObj) {
            return $(htmlObj).css('display') != 'none'
        });
        var captionDiv = jqGridTopObj.find(".ui-jqgrid-caption").filter(function (index, htmlObj) {
            return $(htmlObj).css('display') != 'none'
        });

        return hdiv.length * hdiv.outerHeight(true) + pagerDiv.length * pagerDiv.outerHeight(true) + captionDiv.length * captionDiv.outerHeight(true) + 2;//grid border is 2 //$(".ui-jqgrid-hdiv").length * $(".ui-jqgrid-hdiv").outerHeight() - $(".ui-jqgrid-pager").length * $(".ui-jqgrid-pager").outerHeight()
    };

    grid.prototype.setGridWidth = function (width) {
        this._options.gridObj.setGridWidth(width); //gird border is 2
    };
    grid.prototype.setGridHeight = function (height) {
        this._options.gridObj.setGridHeight(height - this.getGridHeightWithoutBdiv());
    };

    grid.prototype.getTopObj = function () {
        return this._options.gridObj.parent().parent().parent().parent();
    };
    grid.prototype.saveRow = function (rowIds) {
        //if (validate.isNullOrEmpty(gridIds)) {
        //    throw "请输入合适的IDs数组";
        //}
        var jqObj = this._options.gridObj;
        var RowIds = rowIds ?  rowIds : jqObj.jqGrid("getDataIDs");
        var result = 0;
        for (var i = 0; i < RowIds.length; i++) {
            result = jqObj.jqGrid("saveRow", RowIds[i], null, 'clientArray') ? result + 1 : result;
        }
        //重置最后选择项目
       // jqObj.attr("lastsel", "");
        this.lastsel = '';
        return result;
    };
    grid.prototype.getGridState = function () {
        //TODO 如果已非默认的方式打开grid，可能导致错误，
        var jqObj = this._options.gridObj;
        return jqObj.attr('gridstate');
    };

    return viewDirector;
});