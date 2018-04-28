;define(['blocks'], function (blocks) {

    var currentModule = new blocks.ui.module.pageModel();
    currentModule.controllers = {'Main': main};
    currentModule.viewModels = {'Main': mainViewModel};

    function main() {
        var viewModel;
        var view;
        var mainGrid;
        var _Blocks = blocks;
        this.events = {
            'init': function (v, vm) {
                view = v;
                viewModel = vm;
                window.test = vm;
                var citySelect = new blocks.ui.select({
                    viewObj: view.find("#city"),
                    data: [{id: 'china', text: 'china'}, {id: 'us', text: 'us'}],
                    isCombobox:false
                  //  url:"/api/services/BussnessWebModule/Combobox/GetComboboxList"
                });
                var comboboxSelect = new blocks.ui.select({
                    viewObj: view.find("#combobox"),
                    isRemote :true,
                   // isCombobox:false,
                    url:"/api/services/BussnessWebModule/Combobox/GetComboboxList"
                });
             
                var datepicker = new blocks.ui.datepicker({
                    viewObj: view.find("#registerTime"),
                });
            },
            'dispose': function () {
                return "返回值" +view.currentPage.passData;
            },
            'resize': function () {
                //blocks.ui.dialog.info({content: '123'});

            }
        };

        this.actions = {
            saveClick: function (event) {
                var postData = _Blocks.utility.extend({},viewModel);
           //     postData.registerTime = _Blocks.utility.dateConvert.toUtcDate(postData.registerTime);
                blocks.service.safePubAjax({
                    url: '/api/services/BussnessWebModule/MasterData/Add', data: blocks.utility.Json.stringify(postData),
                    onSuccessCallBack: function (result) {
                        blocks.ui.dialog.info({content:result.content, end:function () {
                                view.currentPage.close();
                            }});
                       
                    }
                });
            },
            cancelClick:function (event) {
                 view.currentPage.close();
            }
        };
    }


    function mainViewModel() {
        return {tenancyName: 'initvalue', city: '', isActive: true, comment: 'initcomment',combobox:'',registerTime:''};
    }

    return currentModule;
});     