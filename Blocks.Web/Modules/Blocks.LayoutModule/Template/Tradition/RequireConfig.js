//1，about require js config//配置信息  
;
require.config({
    //define all js file path base on this base path  
    //actually this can setting same to data-main attribute in script tag  
    //定义所有JS文件的基本路径,实际这可跟script标签的data-main有相同的根路径  
    baseUrl: "/Modules"

    //define each js frame path, not need to add .js suffix name  
    //定义各个JS框架路径名,不用加后缀 .js  
    , paths: {
        "jquery": ["Blocks.ResourcesModule/Scripts/jquery-1.9.1.min"]

        , "underscore": "" //路径未提供，可网上搜索然后加上即可  
    }

    //include NOT AMD specification js frame code  
    //包含其它非AMD规范的JS框架  
    , shim: {
        "underscore": {
            "exports": "_"
        }
    }

});  

require(["/Modules/Blocks.BussnessWebModule/Views/Tests/TranditionLayoutTestNew.js"]);  