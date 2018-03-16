; define(['jquery', './UI/ui', 'blocks_utility', "./Abp/AbpWrapper",'./Security/security','./Event/event','./Exception/exception'], function ($, blocksUI, blocksUtility,abpWrapper,security,event,exception) {
   var ajaxHead = {};
    ajaxHead[security.antiForgery.tokenHeaderName] = security.antiForgery.getToken();
    $.ajaxSetup({
        headers: ajaxHead
    });


    return { "ui": blocksUI, 'utility': blocksUtility,'security':security,'event':event,'exception':exception }
});