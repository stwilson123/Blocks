;"use strict";
define(["jquery", "slimscroll", "waves", 'sweetalert', 'toastr',
    './Component/grid',
    'bootstrap_select',
    './Component/dialog',
    'blocks_utility',
    './Component/mvc', './Component/input', './Component/combobox', './Component/select','./Component/datepicker'], function ($, scroll, waves, sweetalert, toastr, grid, bootstrap_select, dialog, utility, mvc, input, combobox, select,datepicker) {

    //  var a = require(['Blocks.ResourcesModule/Framework/UI/Component/dialog']);


    return {'grid': grid, 'dialog': dialog, 'module': mvc, 'combobox': combobox, 'select': select,'datepicker':datepicker};
});