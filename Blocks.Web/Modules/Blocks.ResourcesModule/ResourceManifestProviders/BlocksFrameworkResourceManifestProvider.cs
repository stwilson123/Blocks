﻿using System;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Web.Mvc.UI.Resources;

namespace Blocks.ResourcesModule.ResourceManifestProviders {
    public class BlocksFrameworkResourceManifestProvider : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            
 
            manifest.DefineScript(ResourceName.blocks).SetUrl("Framework/Blocks.js",
                "Framework/blocks.js").SetVersion("0.1")
                .SetDependencies(ResourceName.blocks_UI,ResourceName.blocks_utility, ResourceName.abp_wrapper,ResourceName.blocks_security).SetAMD();
            manifest.DefineScript(ResourceName.blocks_UI).SetUrl("Framework/UI/UI.js",
                "Framework/UI/UI.js").SetVersion("0.1").SetDependencies(ResourceName.jqGrid,ResourceName.layer).SetAMD();
            manifest.DefineScript(ResourceName.blocks_utility).SetUrl("Framework/Utility/utlity.js",
              "Framework/Utility/utility.js").SetVersion("0.1").SetAMD();
            manifest.DefineScript(ResourceName.blocks_security).SetUrl("Framework/Security/security.js",
                "Framework/Security/security.js").SetVersion("0.1").SetDependencies(ResourceName.jquery).SetAMD();
            manifest.DefineScript(ResourceName.abp_wrapper).SetUrl("Framework/Abp/AbpWrapper.js",
              "Framework/Abp/AbpWrapper.js").SetVersion("0.1").SetAMD() ;
        }

        public Lazy<FeatureDescriptor> Feature { get; set; }
    }
}