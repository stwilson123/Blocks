define(['blocks_utility'],function (utility) {

    var decoratorFunction = function (functionName, decoratorFunc) {
        utility.validate.mustFunction(decoratorFunc,'decoratorFunc');

        function decorator(funcName,sourceThis,argus) {
            var sourceFunc = sourceThis[funcName];
            sourceThis[funcName] = function () {
                if (sourceFunc)
                    sourceFunc.apply(this, argus);
                decoratorFunc.apply(this, argus);

            };
        }

        decorator(functionName,this,arguments);
      
    };

    return {'func': decoratorFunction};
});