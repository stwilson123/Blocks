;define(['jquery', 'blocks_utility','./grid/gridbase','./grid/viewbuilder','./grid/databuilder'], function ($, utility,gridbase, viewBuilder,dataBuilder) {
    var grid = gridbase;
    var gridFace = function (setting) {
       
        grid.apply(this,arguments);
        viewBuilder(this);
        dataBuilder(this);
        this.init();
        this.afterInit();
    };
    utility.obj.inherit(grid,gridFace);
    return gridFace;
});