using System;
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
                "lib/jquery-slimscroll/jquery.slimscroll.js").SetVersion("0.1");
            
            manifest.DefineScript(ResourceName.push).SetUrl("lib/push.js/push.min.js",
                "lib/push.js/push.js").SetVersion("0.1");
        }

        public Lazy<FeatureDescriptor> Feature { get; set; }
    }
}
