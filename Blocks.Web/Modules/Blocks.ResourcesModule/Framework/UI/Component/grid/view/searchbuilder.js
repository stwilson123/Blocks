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
            var searchOptions =  gridObj.config.body.searchOptions;
            var colModel = options.colModel;
            for (var i = 0; i < colModel.length; i++) {
                var searchOpt = $.extend(true, {},searchOptions[colModel[i].displayType.type].default,
                    searchOptions[colModel[i].displayType.type][colModel[i].formatType.type], colModel[i].searchOptions);
                colModel[i].searchoptions = colModel[i].searchOptions = searchOpt;
                colModel[i].stype = colModel[i].stype ?  colModel[i].stype : gridObj.config.body.searchType[colModel[i].displayType.type];
            }
        }
    }

  
    return gridSearch;
});