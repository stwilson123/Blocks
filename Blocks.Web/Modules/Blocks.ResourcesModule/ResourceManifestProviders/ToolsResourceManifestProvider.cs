using System;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Web.Mvc.UI.Resources;

namespace Blocks.ResourcesModule.ResourceManifestProviders {
    public class ToolsResourceManifestProvider : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

     
//            manifest.DefineStyle(ResourceName.toastr).SetUrl("lib/toastr/toastr.min.css",
//                "lib/toastr/toastr.css").SetVersion("0.1");
         
            
            
            manifest.DefineScript(ResourceName.json2).SetUrl("lib/json2/json2.js",
                "lib/json2/json2.js").SetVersion("0.1");
            manifest.DefineScript(ResourceName.json2).SetUrl("lib/json2/json2.js",
                "lib/json2/json2.js").SetVersion("0.1");
  
           
        }

        public Lazy<FeatureDescriptor> Feature { get; set; }
    }
}
