using System.Collections.Generic;
using Blocks.Framework.Environment.Extensions.Models;

namespace Blocks.Framework.Web.Mvc.UI.Resources {
    public class ResourceManifestBuilder {
        public ResourceManifestBuilder() {
            ResourceManifests = new HashSet<IResourceManifest>();
        }

       // public Feature Feature { get; set; }
        public FeatureDescriptor Feature { get; set; }
        
        internal HashSet<IResourceManifest> ResourceManifests { get; private set; }

        public ResourceManifest Add() {
            var manifest = new ResourceManifest { Feature = Feature };
            ResourceManifests.Add(manifest);
            return manifest;
        }

        public void Add(IResourceManifest manifest) {
            ResourceManifests.Add(manifest);
        }
    }
}
