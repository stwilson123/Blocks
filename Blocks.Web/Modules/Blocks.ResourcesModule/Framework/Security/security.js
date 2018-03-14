;define(["jquery",'blocks_utility'], function ($,blocks_utility) {


    var antiForgery = {};
    antiForgery.tokenHeaderName = 'X-XSRF-TOKEN';
    antiForgery.tokenCookieName = 'XSRF-TOKEN';

    antiForgery.getToken = function () {
        return blocks_utility.cookie.getCookieValue(antiForgery.tokenCookieName);
    };

    return {antiForgery: antiForgery};
});