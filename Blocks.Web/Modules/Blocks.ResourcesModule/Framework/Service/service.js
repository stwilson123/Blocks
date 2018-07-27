;define(['blocks_utility', 'json2', '../UI/Component/dialog'], function (utility, json2, dialog) {

    var safePubAjax = function (userOptions) {
        var onSuccessCallBack = userOptions.onSuccessCallBack;
        var onFailCallBack = userOptions.onFailCallBack;
        var onCompleteCallBack = userOptions.onCompleteCallBack;
        var combineOptions = $.extend({}, userOptions, {
            onSuccessCallBack:function(data){
                if(data.code === '200')
                    onSuccessCallBack && onSuccessCallBack(data);
                else {
                    if (onFailCallBack) {onFailCallBack(data);return;};
                    safePubAjax.config.default.onFailCallBack(data, undefined, data.msg);
                }
                },
            onCompleteCallBack: function () {
                try {
                    onCompleteCallBack && onCompleteCallBack();
                }
                finally {
                    safePubAjax.config.default.onCompleteCallBack();
                }
            },
            onFailCallBack: function (jqXHR, textStatus, errorThrown) {
                try {
                    safePubAjax.config.default.onFailCallBack(jqXHR, textStatus, errorThrown);
                }
                finally {
                    onFailCallBack && onFailCallBack(undefined,jqXHR, textStatus, errorThrown);
                }
            }

        });
        safePubAjax.config.default.beforeSend();
        return utility.ajax.pubAjax(combineOptions);
    };

    safePubAjax.config = {
        'default': {
            beforeSend: function () {
                dialog.loading.open();
            },
            onCompleteCallBack: function () {
                dialog.loading.close();
            },
            onFailCallBack: function (jqXHR, textStatus, errorThrown) {
                dialog.error({content: errorThrown});
            }
        }


    };


    return {safePubAjax: safePubAjax};
});