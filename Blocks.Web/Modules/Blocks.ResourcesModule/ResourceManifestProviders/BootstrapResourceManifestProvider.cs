using System;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Web.Mvc.UI.Resources;

namespace Blocks.ResourcesModule.ResourceManifestProviders {
    public class BootstrapResourceManifestProvider : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            manifest.DefineStyle(ResourceName.bootstrap).SetUrl(PathBuilder.BuilderStyle("bootstrap.min.css"),
                PathBuilder.BuilderStyle("bootstrap.css")).SetVersion("3.3.7");
            
            manifest.DefineStyle(ResourceName.bootstrap_select).SetUrl("lib/bootstrap-select/dist/css/bootstrap-select.min.css",
                "lib/bootstrap-select/dist/css/bootstrap-select.css").SetVersion("3.3.7").SetDependencies(ResourceName.bootstrap);
            
            
            manifest.DefineScript(ResourceName.bootstrap).SetUrl(PathBuilder.BuilderScripts("bootstrap.min.js"),
                PathBuilder.BuilderScripts("bootstrap.js")).SetVersion("3.3.7").SetDependencies(ResourceName.jquery);
            
            manifest.DefineScript(ResourceName.bootstrap_select).SetUrl("lib/bootstrap-select/dist/js/bootstrap-select.min.js",
                "lib/bootstrap-select/dist/js/bootstrap-select.js").SetVersion("3.3.7").SetDependencies(ResourceName.bootstrap).SetAMD();
            

        }

        public Lazy<FeatureDescriptor> Feature { get; set; }
    }
}
