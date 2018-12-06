; define([], function (moment) {

    var stringUtility = {};
    stringUtility.replaceAll = function (str, search, replacement) {
        var fix = search.replace(/[.*+?^${}()|[\]\\]/g, "\\$&");
        return str.replace(new RegExp(fix, 'g'), replacement);
    };
    stringUtility.format = function (str) {
        if (arguments.length < 1 || str === null) {
            return null;
        }

        var str = str;

        for (var i = 1; i < arguments.length; i++) {
            var placeHolder = '{' + (i - 1) + '}';
            str = this.replaceAll(str, placeHolder, arguments[i]);
        }
        return str;
    };
    return stringUtility;

});


