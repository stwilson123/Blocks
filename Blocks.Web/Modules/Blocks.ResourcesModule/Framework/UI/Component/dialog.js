;define(function(req, exports, module) {
    var $ = require('jquery'),layer =require('layer'),utility = require('blocks_utility');
     
//define(['require', 'jquery', 'layer', 'blocks_utility'], function (req, $, layer, utility) {
    
    var dialogObj = {
        config: {
            'default': {},
            info: {},
            success: {
                icon: 1,
                time: 3000

            },
            warn: {
                icon: 0
            },
            error: {
                icon: 2
            },
            confirm: {
                icon: 3,
                title: 'Are you sure?',
                btn: ['yes', 'cancel']

            },
            dialog: {
                type: 1,
                title: "",
                offset: "auto",
                isMaxmin: true,
                //area: ['80%', '80%'],
                content: settings.url,
                cancel: function () {
                    return true;
                },
                end: function () {
                    // if (!ValidateHelper.isNullOrEmpty(settings.onEnd)) {
                    //     var returnData = ValidateHelper.isNullOrEmpty(returnValueFunction) ? null : returnValueFunction;
                    //     settings.onEnd(returnData);
                    // }

                },
                resize: true
            }
        }
    };
    /* MESSAGE **************************************************/
    var show = function (option) {
        if (!option)
            throw "option can't be null";
        var newOption = option;
        // if (!newOption.title) {
        //     newOption.title = newOption.message;
        //     newOption.message = undefined;
        // }
        // newOption.title = newOption.message;
        var opts = $.extend(
            {},
            dialogObj.config['default'],
            dialogObj.config[option.dialogType],
            newOption
        );

        return layer.open(opts)
    };

    var dialogUI = {};
    dialogUI.info = function (option) {
        return show($.extend(option, {dialogType: 'info'}));
    };

    dialogUI.success = function (option) {
        return show($.extend(option, {dialogType: 'success'}));
    };

    dialogUI.warn = function (option) {
        return show($.extend(option, {dialogType: 'warn'}));

    };

    dialogUI.error = function (option) {
        return show($.extend(option, {dialogType: 'warn'}));
    };

    dialogUI.confirm = function (option) {


        return show($.extend(option, {dialogType: 'confirm'}));
        // var userOpts = {
        //     text: message
        // };
        //
        // if ($.isFunction(titleOrCallback)) {
        //     callback = titleOrCallback;
        // } else if (titleOrCallback) {
        //     userOpts.title = titleOrCallback;
        // }
        // ;
        //
        // var opts = $.extend(
        //     {},
        //     abp.libs.sweetAlert.config['default'],
        //     abp.libs.sweetAlert.config.confirm,
        //     userOpts
        // );
        //
        // return $.Deferred(function ($dfd) {
        //     sweetAlert(opts, function (isConfirmed) {
        //         callback && callback(isConfirmed);
        //         $dfd.resolve(isConfirmed);
        //     });
        // });
    };
    function pathToRelative(path,modulePrefix,fileExtensionName) {
        var moduleFrefix=modulePrefix;
        var startIndex = path.indexOf(moduleFrefix);
        var endIndex = path.lastIndexOf(fileExtensionName);

        return path.slice(startIndex > -1 ? startIndex + moduleFrefix.length + '\\'.length : 0,endIndex > -1 ? endIndex : undefined);
    }
    dialogUI.dialog = function (option) {
        utility.ajax.pubAjax({
            datatype: 'text/html',
            url: option.url, onSuccessCallBack: function (data) {
                var WrapperId = (''+Math.random()).replace('0.','');
                var dataWrapper = '<div id="'+ WrapperId+'">' + data + '</div>'; 
                var layerIndex = show($.extend(option, {dialogType: 'dialog', content: dataWrapper}));

                //req(['/Modules/Blocks.BussnessWebModule/Views/MasterData/Add.js']);
              //  require.config({path:{'Blocks.BussnessWebModule/Views/MasterData/Add':'Blocks.BussnessWebModule/Views/MasterData/Add'}})
 

                if (blocks.pageContext && blocks.pageContext.subPageJsVirtualPath)
                {
                    require([utility.url.pathToRelative(blocks.pageContext.subPageJsVirtualPath,blocks.pageContext.modulePrefix,'.js')],function (containerModules) {
                        containerModules.init($('#' + WrapperId).children());
                    });

                    //  require(['Blocks.BussnessWebModule/Views/MasterData/Index']);
                }
                return layerIndex;
            }
        });

        // var userOpts = {
        //     text: message
        // };
        //
        // if ($.isFunction(titleOrCallback)) {
        //     callback = titleOrCallback;
        // } else if (titleOrCallback) {
        //     userOpts.title = titleOrCallback;
        // }
        // ;
        //
        // var opts = $.extend(
        //     {},
        //     abp.libs.sweetAlert.config['default'],
        //     abp.libs.sweetAlert.config.confirm,
        //     userOpts
        // );
        //
        // return $.Deferred(function ($dfd) {
        //     sweetAlert(opts, function (isConfirmed) {
        //         callback && callback(isConfirmed);
        //         $dfd.resolve(isConfirmed);
        //     });
        // });
    };
    return dialogUI;

});