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
            for (var i = 0; i < options.colModel.length; i++) {
                var searchOpt = $.extend({}, gridObj.config.body.searchoptions[options.colModel[i].datatype.type], options.colModel[i].searchoptions);
                options.colModel[i].searchoptions = searchOpt;
            }
        }
    }

   
    return gridSearch;
});