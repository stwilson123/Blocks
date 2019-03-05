;define(['blocks', 'zTree'], function (blocks,zTree) {

    var currentModule = new blocks.ui.module.pageModel();
    currentModule.controllers = {'Main': main};
    currentModule.viewModels = {'Main': mainViewModel};


    function main() {
        var viewModel;
        var view;
        var mainGrid;

        var combobox;
        var moduleInstance = this;
     

        this.events = {
            'init': function (v, vm) {
                view = v;
                viewModel = vm;
                 
                var colNamesArray = ['ID', moduleInstance.L('city'),'comboboxHideText', 'comboboxText', 
                        moduleInstance.L('registerTime'), moduleInstance.L('activation'), moduleInstance.L('comment')]; //数据列名称（数组） 
                mainGrid = new blocks.ui.grid({
                    // url: "/api/services/BussnessWebModule/MasterData/GetPageList",
                    gridObj: view.find("#gridInfo"),
                    colNames: colNamesArray,
                    colModel: [
                        {name: 'Id', hidden: true},
                        {
                            name: 'city',
                            displayType: { type: 'select' }, formatType: { type: 'select' }, dataSource: [{ id: 'chinaId', text: 'china' }, { id: 'usId', text: 'us' }],
                            editable:true,
                            editOptions:{ triggerEvent: [{type:'change',func:function () {
                                        console.log('change');
                                    }}]}
                        },
                        {
                            name: 'comboboxId', displayType: {type: 'select'}, formatType: {type: 'select'},
                            isRemote:true, url:"/api/services/BussnessWebModule/Combobox/GetComboboxList",
                            editable: true, displayTextCol: 'comboboxText'
                        },
                        {
                            name:"comboboxText", editable: true, editrules: { requried: true, number: true }
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
                    showPager: true,
                    sortname:'city'

                });
                window.mainGrid = mainGrid;
            //    mainGrid.reloadGrid({url: "/api/services/BussnessWebModule/MasterData/GetPageList"});

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

            },
            exceptionClick: function (event)
            {
                blocks.service.safePubAjax({
                    url: '/api/services/BussnessWebModule/MasterData/TestException', data: {},
                    onSuccessCallBack: function (result) {

                    }
                });
            },

            proxyClick: function (event) {
                blocks.service.safePubAjax({
                    url: '/api/services/BussnessWebModule/MasterData/ProxTest', data: {},
                    onSuccessCallBack: function (result) {

                    }
                });
            }

        };
    }


    function mainViewModel() {
        return {city: '', cityTest: ''};
    }

    return currentModule;
});  