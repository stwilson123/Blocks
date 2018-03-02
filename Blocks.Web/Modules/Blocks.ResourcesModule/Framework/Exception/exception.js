;define(['Blocks.ResourcesModule/Framework/UI/Component/dialog'], function (dialog) {

    //  var a = require(['Blocks.ResourcesModule/Framework/UI/Component/dialog']);
    var handle = function (error) {
        dialog.error({content: error.message});
    };
    return {'handle': handle};
});