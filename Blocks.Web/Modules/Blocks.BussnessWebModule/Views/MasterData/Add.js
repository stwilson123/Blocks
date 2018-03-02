; define(["jquery",'blocks'], function ($,blocks) {

    var currentModule = new blocks.ui.module.model();
    currentModule.init = function (view) {
        blocks.utility.log.debug('module init');
    };
   
    
    return currentModule;
});     