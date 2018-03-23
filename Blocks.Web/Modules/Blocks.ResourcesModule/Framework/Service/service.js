; define(['blocks_utility','json2','../UI/Component/dialog'], function (utility,json2,dialog) {

    var safePubAjax = function (userOptions) {
        var onSuccessCallBack = userOptions.onSuccessCallBack;
        var onFailCallBack = userOptions.onFailCallBack;
        var onCompleteCallBack = userOptions.onCompleteCallBack;
        var combineOptions = $.extend({},userOptions,{
            onCompleteCallBack:function () {
                safePubAjax.config.default.onCompleteCallBack();
                onCompleteCallBack && onCompleteCallBack();
            },
            
        });
        return utility.ajax.pubAjax(userOptions);
    };

    safePubAjax.config = {
        'default': {
            beforeSend:function () {
                dialog.loading.open();
            },
            onCompleteCallBack: function () {
                dialog.loading.close();
            }
        }


    };

 
    return { safePubAjax:safePubAjax};
});