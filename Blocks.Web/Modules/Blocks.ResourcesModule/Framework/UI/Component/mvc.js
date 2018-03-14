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
    
    var VueConfig = function () {
      this.init = function () {
          vueJS.config.errorHandler = function (err, vm, info) {
              // handle error
              // `info` 是 Vue 特定的错误信息，比如错误所在的生命周期钩子
              // 只在 2.2.0+ 可用
              throw new Error(err);
          };

          // vueJS.config.warnHandler = function (msg, vm, trace) {
          //     // `trace` 是组件的继承关系追踪
          //     throw new Error(msg);
          // };
      } ;
    };
    new VueConfig().init();
   
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