;define(['jquery', 'jquery_blockUI',
    'Blocks.ResourcesModule/Framework/UI/Component/spin_UI'], function ($,jqueryBlockUI,spinUI) {

    if (!$.blockUI) {
        return;
    }

    $.extend($.blockUI.defaults, {
        message: ' ',
        css: {},
        overlayCSS: {
            backgroundColor: '#AAA',
            opacity: 0.3,
            cursor: 'wait'
        }
    });
    var blockUI = {};
    blockUI.block = function (elm) {
        if (!elm) {
            $.blockUI();
        } else {
            $(elm).block();
        }
    };

    blockUI.unblock = function (elm) {
        if (!elm) {
            $.unblockUI();
        } else {
            $(elm).unblock();
        }
    };

    blockUI.setBusy = function (elm, optionsOrPromise) {
        optionsOrPromise = optionsOrPromise || {};
        if (optionsOrPromise.always || optionsOrPromise['finally']) { //Check if it's promise
            optionsOrPromise = {
                promise: optionsOrPromise
            };
        }

        var options = $.extend({}, optionsOrPromise);

        if (!elm) {
            if (options.blockUI != false) {
                abp.ui.block();
            }

            $('body').spin(spinUI.spinner_config);
        } else {
            var $elm = $(elm);
            var $busyIndicator = $elm.find('.abp-busy-indicator'); //TODO@Halil: What if  more than one element. What if there are nested elements?
            if ($busyIndicator.length) {
                $busyIndicator.spin(spinUI.spinner_config_inner_busy_indicator);
            } else {
                if (options.blockUI != false) {
                    abp.ui.block(elm);
                }

                $elm.spin(abp.libs.spinjs.spinner_config);
            }
        }

        if (options.promise) { //Supports Q and jQuery.Deferred
            if (options.promise.always) {
                options.promise.always(function () {
                    abp.ui.clearBusy(elm);
                });
            } else if (options.promise['finally']) {
                options.promise['finally'](function () {
                    abp.ui.clearBusy(elm);
                });
            }
        }
    };

    blockUI.clearBusy = function (elm) {
        //TODO@Halil: Maybe better to do not call unblock if it's not blocked by setBusy
        if (!elm) {
            unblock();
            
            $('body').spin(false);
        } else {
            var $elm = $(elm);
            var $busyIndicator = $elm.find('.abp-busy-indicator');
            if ($busyIndicator.length) {
                $busyIndicator.spin(false);
            } else {
                unblock(elm);
                $elm.spin(false);
            }
        }
    };
    
    return blockUI;

});