define(['jquery', 'vueJS', 'blocks_utility', '../../Event/event'], function ($, vueJS, utility, eventBus) {


    var DefaultController = function (containerView) {

    };
    DefaultController.prototype.actions = {};
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
    DefaultController.prototype._localization = {};
    DefaultController.prototype._pageContext = {};

    DefaultController.prototype.L = function (str) {
        var lInstance = this._localization;
        if (lInstance) {
            var copyArguments = [this._pageContext.moduleName];
            for (var i = 0; i < arguments.length; i++) {
                copyArguments.push(arguments[i]);
            }
            return lInstance.localize.apply(lInstance, copyArguments);
        }
    };

    var module = function () {
        this.controllers = {};
        this.viewModels = {};
        this._controllersCode = {};
        this._viewModelsCode = {};
    };
    module.prototype.createController = function (sourceController) {
        utility.validate.mustFunction(sourceController, 'The createController param');
        utility.obj.inherit(DefaultController, sourceController);

        return new sourceController();
    };

    module.prototype.createViewModel = function (data, view, controller, pageContext) {
        var currentController = controller;
        var watchFunction = {};
        for(var name in data()){
            watchFunction[name] = function () {
              this.$nextTick(function () {

                  eventBus.trigger("input.update", currentController._view);
                     
                });
            };
        }
        var VM = new vueJS({
            el: view[0], data: data, methods: controller.actions,
            pageContext: pageContext,
            mounted: function () {
                controller._view = $(this.$el);
                var pageContext = {};
                controller.events.init($.extend($(this.$el), {currentPage: view.currentPage}), this._data, pageContext);
            },watch:watchFunction
             
        });
        return VM;

    };

    module.prototype.init = function (pageModel) {
       
        var containerModules = this;
        for (controllerName in containerModules.controllers) {
            var currentController = containerModules.createController(containerModules.controllers[controllerName]);
            currentController._localization = pageModel.localization;
            currentController._pageContext = pageModel.pageContext;
            //TODO view
            var currentViewModel = containerModules.createViewModel(containerModules.viewModels[controllerName], pageModel.view, currentController, pageModel.pageContext);

            containerModules._controllersCode[controllerName] = currentController;
            containerModules._viewModelsCode[controllerName] = currentViewModel;
            currentController.events.resize();
            currentController._windowResizeObject = function () {
                setTimeout(function () {
                    currentController.events.resize();
                }, 200);
            };
            $(window).on('resize', currentController._windowResizeObject);
            try {
                eventBus.trigger("moduleInit", currentController._view);
            }
            finally
            {

            }
             
        }

    };

    module.prototype.displose = function (view) {
        var containerModules = this;
        var resultData;
        for (controller in containerModules._controllersCode) {
            $(window).off('resize', containerModules._controllersCode[controller]._windowResizeObject);
            resultData = containerModules._controllersCode[controller].events.dispose();
        }

        for (vm in containerModules._viewModelsCode) {
            containerModules._viewModelsCode[vm].$destroy();
        }
        containerModules._controllersCode = {};
        containerModules._viewModelsCode = {};
        //containerModules.controllers = {};
        //containerModules.viewModels = {};
       return resultData;
    };
     
    var VueConfig = function () {
        this.init = function () {
            vueJS.config.errorHandler = function (err, vm, info) {
                // handle error
                // `info` 是 Vue 特定的错误信息，比如错误所在的生命周期钩子
                // 只在 2.2.0+ 可用
                //throw new Error(err);
                utility.log.error(err);
            };

            // vueJS.config.warnHandler = function (msg, vm, trace) {
            //     // `trace` 是组件的继承关系追踪
            //     throw new Error(msg);
            // };
        };
    };
    new VueConfig().init();

    vueJS.directive('action', {
        inserted: function (el, binding, vnode, oldVnode) {
            //TODO 获取权限筛选,同时删除组件
             if (vnode.context.$options.pageContext) {
                 var permissions = vnode.context.$options.pageContext.permissions;
                 if (!permissions)
                     return;
                 var curPermission = utility.collectUtility.FirstOrDefault(permissions, function (v) { return v === binding.arg; });
                 if (!curPermission) {
                     el.style.display = 'none';
                 }
             }
         },
        bind: function (el, binding, vnode, oldVnode) {

           
            
        }
    });

    return {pageModel: module};
});