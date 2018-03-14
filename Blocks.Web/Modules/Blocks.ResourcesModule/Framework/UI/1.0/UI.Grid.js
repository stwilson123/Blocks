


 //获取GridContainer长宽的工厂
var GridContainerLengthFactory = (function ($) {
    
    GetGridContainerHeight: function GetGridContainerHeight(configName) {
        if (!typeof (configName) == "string")
            throw "configName不是一个字符串"
        var result = 100;
        var gridHeight = GetGridHeightWithoutBdiv();
        switch(configName)
        {
            case "Layout4TopLeftRight": result = $(window).height() - $("#TopText").outerHeight() - $(".ui-jqgrid-hdiv").height() - $(".ui-jqgrid-pager").height(); break;
            case "WindowsContent": result = $(window).height() - $("#WindowContentSearchDiv").outerHeight() - $("#WindowContentButtonDiv").outerHeight() - GetGridHeightWithoutBdiv() - 20; break; //30是layor的窗口栏固定高度
            case "Content": result = $(window).height() - $("#NavTitle").outerHeight(true) - $("#SearchDiv").outerHeight(true) -
                GetGridHeightWithoutBdiv() - 12; break; //-$(".ui-jqgrid-caption").outerHeight()
            case "TreeContent": result = $(window).height() - $("#TopText").outerHeight() - $("#SearchDiv").outerHeight() -
                 GetGridHeightWithoutBdiv() - $(".panel.layout-panel.layout-panel-center").children(".panel-header").outerHeight() - 15; break;
            case "SelectContent": result = $(window).height() - $("#SearchDiv").outerHeight() - $(".ui-jqgrid-hdiv").outerHeight() - $(".ui-jqgrid-pager").outerHeight() - 15; break;
            case "DoubleGrid": result = $(window).height() - $("#NavTitle").outerHeight(true) - $("#SearchDiv").outerHeight(true) -
                gridHeight - 25; break;

            default: result = 100;
        }
       // console.log("gridPagerAndHeadheight",result);
      //  console.log(result);
        return result <= 100 ? 100 : result;
    };
    //URL 参数赋值
    GetGridContainerWidth: function GetGridContainerWidth(configName, cellNotHiddenNum) {
        var result = 300;
        switch (configName) {
            case "Layout4TopLeftRight": result = $("#centerText").width(); break;
            case "WindowsContent": result = $("#WindowContentSearchDiv").width(); break;
            case "Content": 
            case "TreeContent":  
            case "SelectContent":  
            case "DoubleGrid": result = $("#SearchDiv").width(); break;
                
            default: result = 100;
        }
        var minWidth = 400;
        //console.log(result);
        return result <= minWidth ? minWidth : result;
    };

    GetGridHeightWithoutBdiv: function GetGridHeightWithoutBdiv()
    {
        var hdiv = $(".ui-jqgrid-hdiv").filter(function(index,htmlObj){ return $(htmlObj).css('display') != 'none'});
        var pagerDiv = $(".ui-jqgrid-pager").filter(function(index,htmlObj){ return $(htmlObj).css('display') != 'none'});
        var captionDiv = $(".ui-jqgrid-caption").filter(function(index,htmlObj){ return $(htmlObj).css('display') != 'none'});
        return hdiv.length * hdiv.outerHeight(true) + pagerDiv.length * pagerDiv.outerHeight(true) + captionDiv.length * captionDiv.outerHeight(true)//$(".ui-jqgrid-hdiv").length * $(".ui-jqgrid-hdiv").outerHeight() - $(".ui-jqgrid-pager").length * $(".ui-jqgrid-pager").outerHeight()
    }
    return {
        GetGridContainerHeight: GetGridContainerHeight,
        GetGridContainerWidth: GetGridContainerWidth,
        GetGridHeightWithoutBdiv: GetGridHeightWithoutBdiv
    };

})(jQuery);