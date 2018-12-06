; "use strict";
define(["jquery", "slimscroll", "waves", 'sweetalert', 'toastr', 'bootstrap',
    './Component/grid',
    'bootstrap_select',
    './Component/dialog',
    'blocks_utility',
    './Component/mvc', './Component/input', './Component/combobox', './Component/select', './Component/datepicker',
    './Component/validate_UI'
], function ($, scroll, waves, sweetalert, toastr, bootstrap, grid, bootstrap_select, dialog, utility, mvc, input, combobox, select, datepicker, validate) {

    return { 'grid': grid, 'dialog': dialog, 'module': mvc, 'combobox': combobox, 'select': select, 'datepicker': datepicker, 'validate': validate };
});