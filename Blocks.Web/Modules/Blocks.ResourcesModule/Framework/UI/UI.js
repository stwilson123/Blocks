;"use strict";
define(["jquery", "slimscroll", "waves", 'sweetalert', 'toastr',
    'Blocks.ResourcesModule/Framework/UI/Component/grid',
    'bootstrap_select',
    'Blocks.ResourcesModule/Framework/UI/Component/dialog',
    'blocks_utility',
    'Blocks.ResourcesModule/Framework/UI/Component/mvc'], function ($, scroll, waves, sweetalert, toastr, grid, bootstrap_select,dialog, utility,mvc) {

  //  var a = require(['Blocks.ResourcesModule/Framework/UI/Component/dialog']);

   
     
    return {'grid': grid, 'dialog': dialog, 'module':mvc };
});