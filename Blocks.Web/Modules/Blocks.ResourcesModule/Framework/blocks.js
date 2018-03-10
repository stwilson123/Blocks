; define(["jquery", "blocks_UI", 'blocks_utility', "abp_wrapper",'blocks_security'], function ($, blocksUI, blocksUtility,abpWrapper,security) {
   var ajaxHead = {};
    ajaxHead[security.antiForgery.tokenHeaderName] = security.antiForgery.getToken();
    $.ajaxSetup({
        headers: ajaxHead
    });


    return { "ui": blocksUI, 'utility': blocksUtility,'security':security }
});