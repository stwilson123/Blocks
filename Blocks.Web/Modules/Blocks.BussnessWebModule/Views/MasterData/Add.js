;define(['blocks'], function (blocks) {

    var currentModule = new blocks.ui.module.pageModel();
    currentModule.controllers = {'Main': main};
    currentModule.viewModels = {'Main': mainViewModel};
     
    function main() {
        var viewModel;


        var view;
        var mainGrid;
        var _Blocks = blocks;
        var formValidate;
        var moduleInstance = this;
        this.events = {
            'init': function (v, vm) {
                view = v;
                viewModel = vm;
                window.testViewModel = viewModel;
               
                var citySelect = new blocks.ui.select({
                    viewObj: view.find("#city"),
                    data: [{id: 'chinaId', text: 'china'}, {id: 'usId', text: 'us'}],
                    isCombobox:false
                  //  url:"/api/services/BussnessWebModule/Combobox/GetComboboxList"
                });

                var a = moduleInstance.L("city");
                var comboboxSelect = new blocks.ui.select({
                    viewObj: view.find("#combobox"),
                    isRemote :true,
                    // isCombobox:false,
                   // data: [{id: '1', text: '11'}, {id: '2', text: '22'}],
                    url:"/api/services/BussnessWebModule/Combobox/GetComboboxList"
                });
                comboboxSelect.on('change',function () {
                   console.log(1); 
                });
                var datepicker = new blocks.ui.datepicker({
                    viewObj: view.find("#registerTime"),
                });

                //viewModel.city = 'chinaId';
                //viewModel.combobox = '1';

                formValidate = view.find("form[name='tenantCreateForm']").validate({
                    rules: {
                        TenancyName: { required: true, number:true },
                        city:{required:true}
                    },
                    messages:{
                        TenancyName: "必须填入数字！"
                    }
                });

                
                //window.setTimeout(function () {
                //    viewModel.combobox = '1';
                //},1000);

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
                postData.registerTime = _Blocks.utility.dateConvert.toUtcDate(postData.registerTime);
                if (formValidate.form())
                {
                    blocks.service.safePubAjax({
                        url: '/api/services/BussnessWebModule/MasterData/Add', data: blocks.utility.Json.stringify(postData),
                        onSuccessCallBack: function (result) {
                            blocks.ui.dialog.info({
                                content: result.content, end: function () {
                                    view.currentPage.close();
                                }
                            });

                        }
                    });
                }
              
            },
            cancelClick:function (event) {
                 view.currentPage.close();
            }
        };
    }


    function mainViewModel() {
        return {tenancyName: 'initvalue', city: '', isActive: true, comment: 'initcomment',combobox:'',registerTime:'',email:''};
    }

    return currentModule;
});     