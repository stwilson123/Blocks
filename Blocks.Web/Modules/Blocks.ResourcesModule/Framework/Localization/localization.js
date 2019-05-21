;define(["jquery",'blocks_utility'], function ($,blocks_utility) {

    var localization = function (dic) {

    };
    localization.prototype.dictionary = {};
    localization.prototype.localize = function (sourceName, key) {
         

        var source = (this.dictionary ? this.dictionary : {})[sourceName];

        if (!source) {
            blocks_utility.log.warn('Could not find localization source: ' + sourceName);
            return key;
        }

        var value = source[key];
        if (value == undefined) {
            return key;
        }
        var copiedArguments = Array.prototype.slice.call(arguments, 3);
        copiedArguments.splice(1, 1);
        copiedArguments[0] = value;
        return blocks_utility.string.format.apply(this, copiedArguments);
    };
    return localization;
});