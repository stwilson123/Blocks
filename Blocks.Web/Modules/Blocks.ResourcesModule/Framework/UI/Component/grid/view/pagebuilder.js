define(function (grid) {
    var gridPage = function (grid) {

        initpage.call(this, grid._options)
    };
    
    function initpage(options) {
        var $gridObj = options.gridObj;
        //创建分页区 
        if (options.showPager) {
            var pagerId = $gridObj.attr('id') + "_pager" + ~~(Math.random() * 1000000);
            $gridObj.siblings('div.gridpager[id="' + pagerId + '"]').remove();
            $gridObj.parent().append("<div id='" + pagerId + "' ></div>");
            options.pager = "#" + pagerId;
        }
    }
    return gridPage;
});