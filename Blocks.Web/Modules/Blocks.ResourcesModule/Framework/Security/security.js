; define(["jquery"], function ($) {


    var  antiForgery = {};
    antiForgery.tokenHeaderName = 'X-XSRF-TOKEN';
    antiForgery.tokenCookieName = 'XSRF-TOKEN';

    antiForgery.antiForgery.getToken = function () {
        return $.cookie(antiForgery.tokenCookieName);
    };
     
    return antiForgery;
});