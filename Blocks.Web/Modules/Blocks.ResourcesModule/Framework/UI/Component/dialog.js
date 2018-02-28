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
                resize: false
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
   
    dialogUI.dialog = function (option) {
        req(['Blocks.BussnessWebModule/Views/MasterData/Add']);
        // utility.ajax.pubAjax({
        //     datatype: 'text/html',
        //     url: option.url, onSuccessCallBack: function (data) {
        //         var layerIndex = show($.extend(option, {dialogType: 'dialog', content: data}));
        //       
        //         //req(['/Modules/Blocks.BussnessWebModule/Views/MasterData/Add.js']);
        //       //  require.config({path:{'Blocks.BussnessWebModule/Views/MasterData/Add':'Blocks.BussnessWebModule/Views/MasterData/Add'}})
        //         req(['Blocks.BussnessWebModule/Views/MasterData/Add']);
        //         return layerIndex;
        //     }
        // })

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