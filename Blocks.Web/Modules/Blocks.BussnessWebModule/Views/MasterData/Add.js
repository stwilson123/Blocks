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

                 combobox = new blocks.ui.combobox({viewObj:view.find("#city"),data:
                     { content:[{'ID':'123','Text':'111'}]},minChars:0
                 });


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