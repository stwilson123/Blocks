;define(['blocks'], function (blocks) {

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
                var colNamesArray = ['ID', '站点号', '名称', '工序', '工序类型', '车间', '加工中心', '加工类型', '采集类型', '状态', '描述']; //数据列名称（数组） 
                mainGrid = new blocks.ui.grid({
                    // url: "/api/services/BussnessWebModule/MasterData/GetPageList",
                    gridObj: view.find("#gridInfo"),
                    colNames: colNamesArray,
                    colModel: [
                        {name: 'ID', hidden: true},
                        {name: 'CollectStationNo', index: 'CollectStationNo'},
                        {name: 'CollectStationName', index: 'COLLECT_STATION_NAME'},
                        {name: 'ProcedureName', index: 'BDTA_WORKPROCEDURE.WORKPROCEDURE_NAME'},
                        {
                            name: 'ProcedureType',
                            index: 'BDTA_WORKPROCEDURE.WORKPROCEDURE_TYPE',
                            formatter: 'select',
                            editoptions: {value: {'1': '装配', '2': '一般', '3': '检验', '4': '装配检验', '5': 'OQC', '6': '返工'}}
                        },
                        {name: 'WorkshopName', index: 'BDTA_WORKSHOP.WORKSHOP_NAME'},//Index是排序时回传到后台的字段，注意要跟数据库表和字段对应
                        {name: 'MachineCenterName', index: 'BDTA_MACHING_CENTER.MACHING_CENTER_NAME'},
                        {name: 'MachineCenterWorkType', index: 'MACHING_CENTER_WORK_TYPE'},
                        {
                            name: 'CollectType',
                            index: 'COLLECT_TYPE',
                            formatter: 'select',
                            editoptions: {value: {'1': '一次', '2': '上下线'}}
                        },

                        //{ name: 'StationName', index: 'BDTA_WORKSECTION.WORKSECTION_NAME' },
                        {name: 'CollectStationState', index: 'COLLECT_STATION_STATE'},
                        {name: 'Desc', index: 'COLLECT_STATION_DESC', sortable: false}
                    ],
                    // caption: "",
                    idKey: "ID",
                    dynamicConditionQuery: {active: true}
                });

                mainGrid.reloadGrid({url: "/api/services/BussnessWebModule/MasterData/GetPageList"});


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
                blocks.ui.dialog.dialog({url: 'Add'});
            },
            queryClick: function (event) {
                mainGrid.dynamicConditionLoad();
            }
        };
    }


    function mainViewModel() {
        return {value: ''};
    }

    return currentModule;
});  