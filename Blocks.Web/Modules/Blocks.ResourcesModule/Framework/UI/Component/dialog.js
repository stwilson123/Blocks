;define(['jquery','layer', 'blocks_utility','../../Localization/localization'],function ($, layer, utility,localizationJS) {
   

    var dialogOption = {
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
                area: ['80%', 'auto'],
                closeBtn: 0,
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
            },
            loading: {type: 3, icon: 1, resize: !1, shade: .1}
        }
    };

    var dialog = function (setting) {
        this.dialogIndex = setting.dialogIndex;
        this.passData = setting.passData;
        this.viewComponent = layer;
    };
    dialog.prototype = {
        close: function () {
            this.viewComponent.close(this.dialogIndex);
        },
        style: function (style) {
            this.viewComponent.style(style);
        },
        title: function (title) {
            this.viewComponent.title(title);
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
        if (!newOption.content)
            newOption.content = '';
        var opts = $.extend(
            {},
            dialogOption.config['default'],
            dialogOption.config[option.dialogType],
            newOption
        );
        var index = layer.open(opts);
        return index;
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

    function pathToRelative(path, modulePrefix, fileExtensionName) {
        var moduleFrefix = modulePrefix;
        var startIndex = path.indexOf(moduleFrefix);
        var endIndex = path.lastIndexOf(fileExtensionName);

        return path.slice(startIndex > -1 ? startIndex + moduleFrefix.length + '\\'.length : 0, endIndex > -1 ? endIndex : undefined);
    }

    dialogUI.dialog = function (option) {

        var passData = option.passData;
        dialogUI.loading.open();
        utility.ajax.pubAjax({
            dataType: 'html',
            url: option.url
        }).done(function (data) {
            var WrapperId = ('' + Math.random()).replace('0.', '');
            var dialogContent = WrapperId+'dialog';
            var dataWrapper = '<div id="' + WrapperId + '" style="height:100%">' + data + '</div>';
            
            var endCallback = option.end;
            var currentModule;
            var layerIndex = show($.extend(option, {
               id:dialogContent, dialogType: 'dialog', content: dataWrapper, end: function () {
                    var currentModuleResult;
                      
                    try {
                        if (currentModule)
                            currentModuleResult = currentModule.displose();
                    }
                    finally {
                        if (endCallback)
                            endCallback(currentModuleResult);
                    }

                }

            }));
            $("#" + dialogContent).parents(".layui-layer").filter(':first').attr("role",'dialog');
            //req(['/Modules/Blocks.BussnessWebModule/Views/MasterData/Add.js']);
            //  require.config({path:{'Blocks.BussnessWebModule/Views/MasterData/Add':'Blocks.BussnessWebModule/Views/MasterData/Add'}})


            if (blocks.pageContext && blocks.pageContext.subPageJsVirtualPath) {
                // require([utility.url.pathToRelative(blocks.pageContext.subPageJsVirtualPath, blocks.pageContext.modulePrefix, '.js')], function (containerModules) {
                require([blocks.pageContext.subPageJsVirtualPath], function (containerModules) {
                    var localization = new localizationJS();
                    localization.dictionary = blocks.localization;
                    currentModule = containerModules;
                    containerModules.init(
                        {
                            view: $.extend($('#' + WrapperId), { currentPage: new dialog({ dialogIndex: layerIndex, passData: passData }) }),
                            pageContext: $.extend(true, {}, blocks.pageContext),
                            localization: localization
                            
                            
                        });
                });
                //  require(['Blocks.BussnessWebModule/Views/MasterData/Index']);
            }
            return layerIndex;


        }).always(function () {
            dialogUI.loading.close();
        });


    };

    var loadingDep = 0, dialogIndex = null;

    dialogUI.loading =
        {
            open: function (option) {
                if (loadingDep < 1)
                    dialogIndex = show($.extend(option, {dialogType: 'loading'}));

                loadingDep++;
                return dialogIndex;

            },
            close: function () {
                if (--loadingDep < 1)
                    layer.close(dialogIndex);
            }
        };
    return dialogUI;

});