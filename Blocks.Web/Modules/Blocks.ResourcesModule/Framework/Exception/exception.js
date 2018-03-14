;define(['Blocks.ResourcesModule/Framework/UI/Component/dialog','blocks_utility'], function (dialog,utility) {

    //  var a = require(['Blocks.ResourcesModule/Framework/UI/Component/dialog']);
    var handle = function (error) {
        dialog.error({content: error.message});
        utility.log.error(error);
    };
    return {'handle': handle};
});