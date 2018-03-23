;define(['blocks', 'select2'], function (blocks) {

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

                // combobox = new blocks.ui.combobox({viewObj:view.find("#city"),data:
                //     { content:[{'ID':'123','Text':'111'}]},minChars:0
                // });

                var citySelect = new blocks.ui.select({
                    viewObj: view.find("#city"),
                    data: [{id: 'china', text: 'china'}, {id: 'us', text: 'us'}],
                });

            },
            'dispose': function () {

            },
            'resize': function () {
                blocks.ui.dialog.info({content: '123'});

            }
        };

        this.actions = {
            saveClick: function (event) {
                blocks.service.safePubAjax({
                    url: '/api/services/BussnessWebModule/MasterData/Add', data: blocks.utility.Json.stringify(viewModel),
                    onSuccessCallBack: function (content) {
                        blocks.ui.dialog.info(content);
                    }
                });
            }
        };
    }


    function mainViewModel() {
        return {tenancyName: 'initvalue', city: '', isActive: true, comment: 'initcomment'};
    }

    return currentModule;
});     