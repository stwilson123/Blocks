﻿using System;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Web.Mvc.UI.Resources;

namespace Blocks.LayoutModule {
    public class ResourceManifestProvider : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            
            manifest.DefineResource("template","_LayoutRoot").SetUrl("_LayoutRoot.cshtml", "_LayoutRoot.cshtml").SetVersion("0.1");
            manifest.DefineResource("template","_LayoutFirst").SetUrl("_LayoutFirst.cshtml", "_LayoutFirst.cshtml").SetVersion("0.1");
            manifest.DefineResource("template","_LayoutPartialViewFirst").SetUrl("_LayoutPartialViewFirst.cshtml", "_LayoutPartialViewFirst.cshtml").SetVersion("0.1");
            manifest.DefineResource("template","Tradition/_Layout").SetUrl("Tradition/_Layout.cshtml", "Tradition/_Layout.cshtml").SetVersion("0.1");
            manifest.DefineResource("template", "Tradition/_LayoutModule").SetUrl("Tradition/_LayoutModule.cshtml", "Tradition/_LayoutModule.cshtml").SetVersion("0.1");
            manifest.DefineResource("template", "Tradition/_LayoutPartialModule").SetUrl("Tradition/_LayoutPartialModule.cshtml", "Tradition/_LayoutPartialModule.cshtml").SetVersion("0.1");
            manifest.DefineResource("template", "Tradition/_LayoutPDAModule").SetUrl("Tradition/_LayoutPDAModule.cshtml", "Tradition/_LayoutPDAModule.cshtml").SetVersion("0.1");

        }

        public Lazy<FeatureDescriptor> Feature { get; set; }
    }
}
