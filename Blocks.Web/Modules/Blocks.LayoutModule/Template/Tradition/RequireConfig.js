//1，about require js config//配置信息  
;
(function () {
    if (!(blocks && blocks.pageContext.listRegisteredJavaScripts)) {
        return;
    }

    var extend = function (target, options) {
        for (name in options) {
            target[name] = options[name];
        }
        return target;
    };
    var JsModule = {};
    var shimModule = {};
    for (var i = 0; i < blocks.pageContext.listRegisteredJavaScripts.length; i++) {
        var registerJavaScript = blocks.pageContext.listRegisteredJavaScripts[i];
        JsModule[registerJavaScript.Name] = registerJavaScript.Src;
        if (!registerJavaScript.IsAMD) {
            shimModule[registerJavaScript.Name] = { deps: registerJavaScript.Dependencies, exports: registerJavaScript.Name};
        }
        
    }
    extend(JsModule, {
        "Tradition/_LayoutModule": "/Modules/Blocks.LayoutModule/template/Tradition/_LayoutModule",
       
    });
    require.config({
        //define all js file path base on this base path  
        //actually this can setting same to data-main attribute in script tag  
        //定义所有JS文件的基本路径,实际这可跟script标签的data-main有相同的根路径  
        baseUrl: "Modules"

        //define each js frame path, not need to add .js suffix name  
        //定义各个JS框架路径名,不用加后缀 .js  
        , paths: JsModule
        //include NOT AMD specification js frame code  
        //包含其它非AMD规范的JS框架  
        , shim: shimModule
    });  
    if (blocks.pageContext.subPageJsVirtualPath)
    {  
        require([blocks.pageContext.subPageJsVirtualPath]);
    }




})();


//require(["/Modules/Blocks.BussnessWebModule/Views/Tests/TranditionLayoutTestNew.js"]);  

