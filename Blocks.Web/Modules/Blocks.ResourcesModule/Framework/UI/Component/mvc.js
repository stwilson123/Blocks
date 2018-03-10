define(['jquery','vueJS','blocks_utility'],function ($,vueJS,utility) {


   
    
    
    var DefaultController = function (containerView) {

    };
    DefaultController.prototype.actions = {
    };
    DefaultController.prototype.events = {
        init: function (view) {
            utility.log.debug('please implement init method!');
        },
        dispose: function () {
            utility.log.debug('please implement dispose method!');
        },
        resize: function () {
            utility.log.debug('please implement resize method!');
        }
    };

    var module = function () {
        this.controllers = {};
        this.viewModels = {};
        this._controllersCode = {};
        this._viewModelsCode = {};
    };
    module.prototype.createController = function (sourceController) {
        utility.validate.mustFunction(sourceController,'The createController param');
        utility.obj.inherit(DefaultController,sourceController);

        return new sourceController();
    };
    module.prototype.createViewModel =  function (data,view,controller) {

        return new vueJS({ el:view[0], data:data,methods:controller.actions,mounted:function () {

            controller.events.init($(this.$el),this._data);
        }});

    };

    module.prototype.init =  function (view) {
        var containerModules = this;
        for(controllerName in containerModules.controllers)
        {
            var currentController = containerModules.createController(containerModules.controllers[controllerName]);
            //TODO view
            var currentViewModel = containerModules.createViewModel(containerModules.viewModels[controllerName],view,currentController);

            containerModules._controllersCode[controllerName] = currentController;
            containerModules._viewModelsCode[controllerName] = currentViewModel;
            currentController.events.resize();
            $(window).resize(function () {
                setTimeout(function () {
                    currentController.events.resize();
                },200);
            });
        }

    };

    vueJS.directive('action', {
        // inserted: function (el) {
        //     // 聚焦元素
        //     el.focus()
        // },
        bind: function (el, binding, vnode, oldVnode) {
            //TODO 获取权限筛选,同时删除组件
            if(binding.arg === 'add'){
              
                el.style.display = 'none';
            }
        }
    });
    return { pageModel: module};
});