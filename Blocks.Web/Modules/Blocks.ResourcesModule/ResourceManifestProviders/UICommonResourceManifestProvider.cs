﻿using System;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Web.Mvc.UI.Resources;

namespace Blocks.ResourcesModule.ResourceManifestProviders {
    public class UICommonResourceManifestProvider : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            
            manifest.DefineStyle(ResourceName.toastr).SetUrl("lib/toastr/toastr.min.css",
                "lib/toastr/toastr.css").SetVersion("0.1");
            manifest.DefineStyle(ResourceName.sweetalert).SetUrl("lib/sweetalert/dist/sweetalert.css",
                "lib/sweetalert/dist/sweetalert.css").SetVersion("0.1");
            manifest.DefineStyle(ResourceName.waves).SetUrl("lib/Waves/dist/waves.min.css",
                "lib/Waves/dist/waves.css").SetVersion("0.1");
            manifest.DefineStyle(ResourceName.animate).SetUrl("lib/animate.css/animate.min.css",
                "lib/animate.css/animate.css").SetVersion("0.1");
            manifest.DefineStyle(ResourceName.jqGrid).SetUrl("lib/jqGrid/css/ui.jqgrid-bootstrap.css",
                "lib/jqGrid/css/ui.jqgrid-bootstrap.css").SetVersion("5.2.0");
            manifest.DefineStyle(ResourceName.jqGridUI).SetUrl("lib/jqGrid/css/ui.jqgrid-bootstrap-ui.css",
                "lib/jqGrid/css/ui.jqgrid-bootstrap-ui.css").SetVersion("5.2.0").SetDependencies(ResourceName.jqGrid);
            manifest.DefineStyle(ResourceName.layer).SetUrl("lib/layer/theme/default/layer.css",
                "lib/layer/theme/default/layer.css").SetVersion("3.3.1");
            manifest.DefineStyle(ResourceName.magicsuggest).SetUrl("lib/combobox/magicsuggest-min.css",
                "lib/combobox/magicsuggest.css").SetVersion("3.3.1");
            manifest.DefineStyle(ResourceName.select2).SetUrl("lib/select2-develop/dist/css/select2.min.css",
                "lib/select2-develop/dist/css/select2.css").SetVersion("4.0.6");
            manifest.DefineStyle(ResourceName.zTree).SetUrl("lib/zTree_v3/css/metroStyle/metroStyle.css",
                "lib/zTree_v3/css/metroStyle/metroStyle.css").SetVersion("3.5.35");
            
            manifest.DefineScript(ResourceName.toastr).SetUrl("lib/toastr/toastr.min.js",
                "lib/toastr/toastr.js").SetVersion("0.1");
            manifest.DefineScript(ResourceName.sweetalert).SetUrl("lib/sweetalert/dist/sweetalert.min.js",
                "lib/sweetalert/dist/sweetalert-dev.js").SetVersion("0.1");
            manifest.DefineScript(ResourceName.waves).SetUrl("lib/Waves/dist/waves.min.js",
                "lib/Waves/dist/waves.js").SetVersion("0.1");
            manifest.DefineScript(ResourceName.moment).SetUrl("lib/moment/min/moment-with-locales.min.js",
                "lib/moment/min/moment-with-locales.js").SetVersion("0.1");
            
            manifest.DefineScript(ResourceName.jquery_blockUI).SetUrl("lib/blockUI/jquery.blockUI.js",
                "lib/blockUI/jquery.blockUI.js").SetVersion("0.1");
            
            manifest.DefineScript(ResourceName.spin).SetUrl("lib/spin.js/spin.min.js",
                "lib/spin.js/spin.js").SetVersion("0.1");
            manifest.DefineScript(ResourceName.jquery_spin).SetUrl("lib/spin.js/jquery.spin.js",
                "lib/spin.js/jquery.spin.js").SetVersion("0.1").SetDependencies(ResourceName.spin);
            
            manifest.DefineScript(ResourceName.slimscroll).SetUrl("lib/jquery-slimscroll/jquery.slimscroll.min.js",
                "lib/jquery-slimscroll/jquery.slimscroll.js").SetVersion("0.1").SetDependencies(ResourceName.jquery);
            
            manifest.DefineScript(ResourceName.push).SetUrl("lib/push.js/push.min.js",
                "lib/push.js/push.js").SetVersion("0.1");


            manifest.DefineScript(ResourceName.jqGrid).SetUrl("lib/jqGrid/script/jquery.jqGrid.min.js",
                "lib/jqGrid/script/jquery.jqGrid.min.js").SetVersion("5.2.0").SetCultures();

            manifest.DefineScript(ResourceName.jqGrid + "-zh-CN").SetUrl("lib/jqGrid/script/i18n/grid.locale-cn.js",
                          "lib/jqGrid/script/i18n/grid.locale-cn.js").SetVersion("5.2.0");

            manifest.DefineScript(ResourceName.jqGrid + "-en").SetUrl("lib/jqGrid/script/i18n/grid.locale-en.js",
                      "lib/jqGrid/script/i18n/grid.locale-en.js").SetVersion("5.2.0");

            manifest.DefineScript(ResourceName.layer).SetUrl("lib/layer/layer.js",
                "lib/layer/layer.js").SetVersion("3.1.1").SetAMD();
            
            manifest.DefineScript(ResourceName.magicsuggest).SetUrl("lib/combobox/magicsuggest-min.js",
                "lib/combobox/magicsuggest.js").SetVersion("2.1.4");
            
            manifest.DefineScript(ResourceName.select2).SetUrl("lib/select2-develop/dist/js/select2.full.min.js",
                "lib/select2-develop/dist/js/select2.full.js").SetVersion("4.0.6").SetAMD();
            
            manifest.DefineScript(ResourceName.my97DatePicker).SetUrl("lib/My97DatePicker/My97DatePicker/WdatePicker.js",
                "lib/My97DatePicker/My97DatePicker/WdatePicker.js").SetVersion("4.8").SetDependencies(ResourceName.jquery);
            
            manifest.DefineScript(ResourceName.zTree).SetUrl("lib/zTree_v3/js/jquery.ztree.all.js",
                "lib/zTree_v3/js/jquery.ztree.all.js").SetVersion("3.5.35").SetDependencies(ResourceName.jquery);
    
  
        }

        public Lazy<FeatureDescriptor> Feature { get; set; }
    }
}
