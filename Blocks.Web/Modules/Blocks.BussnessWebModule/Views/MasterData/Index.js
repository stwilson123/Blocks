;define(['blocks', 'zTree'], function (blocks) {

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
                var colNamesArray = ['ID', '城市', 'comboboxText', '注册时间', '激活', '备注']; //数据列名称（数组） 
                mainGrid = new blocks.ui.grid({
                    // url: "/api/services/BussnessWebModule/MasterData/GetPageList",
                    gridObj: view.find("#gridInfo"),
                    colNames: colNamesArray,
                    colModel: [
                        {name: 'Id', hidden: true},
                        {
                            name: 'city',
                            displayType: { type: 'select' }, formatType: { type: 'select' }, dataSource: [{ id: 'chinaId', text: 'china' }, { id: 'usId', text: 'us' }],editable:true
                        },
                        {
                            name: 'comboboxText'
                        },
                        {
                            name: 'registerTime', formatType: {type: 'date'}, displayType: {type: 'datepicker'},editable:true
                        },
                        {
                            name: 'isActive',
                            formatType: {type: 'checkbox'},
                            displayType:{type:'checkbox'},
                            editable:true
                            // formatter: 'select',
                            // editoptions: {value: {'1': 'OK', '0': 'NO'}}
                        },
                        {name: 'comment', sortable: false}
                    ],
                    // caption: "",
                    idKey: "Id",
                    dynamicConditionQuery: {active: true},
                    rownumbers: true,
                    showPager: true

                });
                colNamesArray = ['ID', '城市', 'comboboxText', '注册时间', '激活', '备注']; //数据列名称（数组） 
                window.mainGrid = mainGrid;
                mainGrid.reloadGrid({url: "/api/services/BussnessWebModule/MasterData/GetPageList"});

            },
            'dispose': function () {

            },
            'resize': function () {
                // mainGrid.setGridWidth(view.children().width());
                // // blocks.utility.log.info('height' + view.children().height());
                // // blocks.utility.log.info('width' + view.children().width());
                mainGrid.setGridHeight(view.children().height() - view.find('#query').height());


            }
        };

        this.actions = {
            addClick: function (event) {
                blocks.ui.dialog.dialog({
                    url: 'Add', passData: {isAdd: true}, title: 'title', end: function (result) {
                        blocks.ui.dialog.info({content: result});
                    }
                });
            },
            queryClick: function (event) {
                // mainGrid.reloadGrid({url: "/api/services/BussnessWebModule/MasterData/GetPageList"});
                mainGrid.dynamicConditionLoad({url: "/api/services/BussnessWebModule/MasterData/GetPageList"});

            }
        };
    }


    function mainViewModel() {
        return {city: '', cityTest: ''};
    }

    return currentModule;
});  