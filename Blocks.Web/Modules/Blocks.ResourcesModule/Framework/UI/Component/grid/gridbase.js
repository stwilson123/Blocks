'use strict';
;define(['jquery', 'blocks_utility', 'jqGrid', '../dialog', './config/config', '../viewStruct/viewEvent', '../../../../lib/jqGrid/script/i18n/grid.locale-cn'], function ($, utility, jqGrid, dialog, gridConfig, viewEvent) {


    var validate = utility.validate;
    var grid = function (option) {
        validate.mustUseNew(grid);

        viewEvent.call(this, {
            eventsStore: $.extend(true, {}, this.config.eventsStore),
            options: $.extend(true, {}, this.config.default, option)
        });

    };
    utility.obj.inherit(viewEvent,grid);

    grid.prototype.config = gridConfig;

    grid.prototype.init = function () {
    };

    grid.prototype.afterInit = function () {
    };
    return grid;

});