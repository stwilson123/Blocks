;define(['jquery', 'sweetalert'], function ($) {

    if (!sweetAlert || !$) {
        return;
    }

    var sweetAlertObj = {
        config: {
            'default': {},
            info: {
                type: 'info'
            },
            success: {
                type: 'success'
            },
            warn: {
                type: 'warning'
            },
            error: {
                type: 'error'
            },
            confirm: {
                type: 'warning',
                title: 'Are you sure?',
                showCancelButton: true,
                cancelButtonText: 'Cancel',
                confirmButtonColor: "#DD6B55",
                confirmButtonText: 'Yes'
            }
        }
    };
    /* MESSAGE **************************************************/
    var showMessage = function (option) {
        if (!option)
            throw "option can't be null";
        var newOption = option;
        if (!newOption.title) {
            newOption.title = newOption.message;
            newOption.message = undefined;
        }
        newOption.title = newOption.message;
        var opts = $.extend(
            {},
            sweetAlertObj.config['default'],
            sweetAlertObj.config[option.type],
            newOption
        );

        return $.Deferred(function ($dfd) {
            sweetAlert(opts, function () {
                $dfd.resolve();
            });
        });
    };

    var messageUI = {};
    messageUI.info = function (option) {
        return showMessage($.extend(option, {type: 'info'}));
    };

    messageUI.success = function (option) {
        return showMessage($.extend(option, {type: 'success'}));
    };

    messageUI.warn = function (option) {
        return showMessage($.extend(option, {type: 'warn'}));

    };

    messageUI.error = function (option) {
        return showMessage($.extend(option, {type: 'warn'}));
    };

    messageUI.confirm = function (option) {
        var userOpts = {
            text: message
        };

        if ($.isFunction(titleOrCallback)) {
            callback = titleOrCallback;
        } else if (titleOrCallback) {
            userOpts.title = titleOrCallback;
        }
        ;

        var opts = $.extend(
            {},
            abp.libs.sweetAlert.config['default'],
            abp.libs.sweetAlert.config.confirm,
            userOpts
        );

        return $.Deferred(function ($dfd) {
            sweetAlert(opts, function (isConfirmed) {
                callback && callback(isConfirmed);
                $dfd.resolve(isConfirmed);
            });
        });
    };


    return messageUI;

});