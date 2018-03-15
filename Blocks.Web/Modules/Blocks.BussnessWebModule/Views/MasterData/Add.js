; define(['blocks'], function (blocks) {

    var currentModule = new blocks.ui.module.pageModel();
    currentModule.controllers = {'Main': main};
    currentModule.viewModels = {'Main': mainViewModel};

    function main() {
        var viewModel;
        var view;
        var mainGrid;
        this.events = {
            'init': function (v, vm) {
                view = v;
                viewModel = vm;
                 


            },
            'dispose': function () {

            },
            'resize': function () {
                blocks.ui.dialog.info({content:'123'});

            }
        };

        this.actions = {
            
        };
    }


    function mainViewModel() {
        return { };
    }

    return currentModule;
});     