;define(['blocks', 'select2'], function (blocks) {

    var currentModule = new blocks.ui.module.pageModel();
    currentModule.controllers = {'Main': main};
    currentModule.viewModels = {'Main': mainViewModel};

    function main() {
        var viewModel;
        var view;
        var mainGrid;
        var combobox;
        this.events = {
            'init': function (v, vm) {
                view = v;
                viewModel = vm;
                var colNamesArray = ['ID', '城市', 'comboboxText', '激活', '备注']; //数据列名称（数组） 
                mainGrid = new blocks.ui.grid({
                    // url: "/api/services/BussnessWebModule/MasterData/GetPageList",
                    gridObj: view.find("#gridInfo"),
                    colNames: colNamesArray,
                    colModel: [
                        {name: 'ID', hidden: true},
                        {name: 'city'},
                        {name: 'comboboxText'},

                        //{ name: 'StationName', index: 'BDTA_WORKSECTION.WORKSECTION_NAME' },
                        {name: 'isActive', formatter: 'select', editoptions: {value: {'1': 'OK', '0': 'NO'}}},
                        {name: 'comment', sortable: false}
                    ],
                    // caption: "",
                    idKey: "ID",
                    dynamicConditionQuery: {active: true}
                });

                // mainGrid.reloadGrid({url: "/api/services/BussnessWebModule/MasterData/GetPageList"});


            },
            'dispose': function () {

            },
            'resize': function () {
                mainGrid.setGridWidth(view.children().width());
                blocks.utility.log.info('height' + view.children().height());
                blocks.utility.log.info('width' + view.children().width());
                mainGrid.setGridHeight(view.children().height() - view.find('#query').height());


            }
        };

        this.actions = {
            addClick: function (event) {
                blocks.ui.dialog.dialog({
                    url: 'Add', title: 'title', end: function (result) {
                        blocks.ui.dialog.info({content: result});
                    }
                });
            },
            queryClick: function (event) {
                //   mainGrid.reloadGrid({url: "/api/services/BussnessWebModule/MasterData/GetPageList"});
                mainGrid.dynamicConditionLoad({url: "/api/services/BussnessWebModule/MasterData/GetPageList"});
                var a = viewModel.cityTest;
                viewModel.city = a;
            }
        };
    }


    function mainViewModel() {
        return {city: '', cityTest: ''};
    }

    return currentModule;
});  