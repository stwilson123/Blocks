; define(['jquery'], function (jQuery) {

    var ValidateHelper = (function ($) {
        isNullOrEmpty: function isNullOrEmpty(v) {
            return v === '' || v == undefined || v == null || typeof (v) == "undefined";

        };
        isDecimal: function isDecimal(v) {
            return /^(-?\d+)(\.\d+)?$/.test(v);
        };
        isHtml: function isHtml(v) {
            return new RegExp('^<([^>\s]+)[^>]*>(.*?<\/\\1>)?$').test(v);
        };
        isInt: function isInt(v) {
            return /^[+]{0,1}(\d+)$/.test(v)
        };
        isErrorCode: function isErrorCode(code) {
            return (code != 100 && code < 3000) || code > 10000;
        };
        return { isNullOrEmpty: isNullOrEmpty, isDecimal: isDecimal, isHtml: isHtml, isInt: isInt, isErrorCode: isErrorCode };
    })(jQuery);


    return { validate: ValidateHelper }
});