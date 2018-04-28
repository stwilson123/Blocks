define(['../gridbase', 'blocks_utility','../../../../Design/decorator'], function (grid, utility,decoratorPattern) {
    var gridSearch = function (gridObj) {
        initoptions.call(this, gridObj);
    };
    decoratorPattern.func.call(grid.prototype,'afterInit',function () {
        var options = this._options;
        var $gridObj = options.gridObj;
        $gridObj.jqGrid('navGrid', options.pager,
            {edit: false, add: false, del: false},
            {},
            {},
            {},
            options.dynamicConditionQuery
        );
        this.getTopObj().find('#search_' + $gridObj.attr('Id')).hide();
    })
    function initoptions(gridObj) {
        var options = gridObj._options;
        if (options.dynamicConditionQuery && options.dynamicConditionQuery.active === true) {
            var searchoptions =  gridObj.config.body.searchoptions;
            var colModel = options.colModel;
            for (var i = 0; i < colModel.length; i++) {
                var searchOpt = $.extend(true, {},searchoptions[colModel[i].displaytype.type].default,
                    searchoptions[colModel[i].displaytype.type][colModel[i].datatype.type], colModel[i].searchoptions);
                colModel[i].searchoptions = searchOpt;
                colModel[i].stype = colModel[i].stype ?  colModel[i].stype : gridObj.config.body.searchType[colModel[i].displaytype.type];
            }
        }
    }

  
    return gridSearch;
});