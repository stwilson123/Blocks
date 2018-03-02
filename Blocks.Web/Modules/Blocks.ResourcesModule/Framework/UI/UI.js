;define(["jquery", "slimscroll", "waves", 'sweetalert', 'toastr',
    'Blocks.ResourcesModule/Framework/UI/Component/grid',
    'bootstrap_select',
    'Blocks.ResourcesModule/Framework/UI/Component/dialog',
    'blocks_utility'], function ($, scroll, waves, sweetalert, toastr, grid, bootstrap_select,dialog, utility) {

  //  var a = require(['Blocks.ResourcesModule/Framework/UI/Component/dialog']);
    var module = function () {
        this.init = function (view) {
            utility.log.debug('please implement init method!');
        };

        this.dispose = function () {
            utility.log.debug('please implement dispose method!');
        };
    };
    return {'grid': grid, 'dialog': dialog, 'module': { model: module}};
});