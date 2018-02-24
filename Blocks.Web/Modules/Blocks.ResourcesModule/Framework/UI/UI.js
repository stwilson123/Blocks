; define(["jquery", "slimscroll", "waves", 'sweetalert', 'toastr', 
    '/Modules/Blocks.ResourcesModule/Framework/UI/Component/grid.js', 
    'bootstrap_select','/Modules/Blocks.ResourcesModule/Framework/UI/Component/msg_alert.js',
    '/Modules/Blocks.ResourcesModule/Framework/UI/Component/dialog.js'], function ($,scroll,waves,sweetalert,toastr,grid,bootstrap_select,msgAlert) {
     

        return { 'grid': grid,'message':msgAlert }
});