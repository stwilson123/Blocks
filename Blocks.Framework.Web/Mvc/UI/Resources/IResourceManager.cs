using System;
using System.Collections.Generic;

namespace Blocks.Framework.Web.Mvc.UI.Resources {
    public interface IResourceManager {// : IDependency {
        IEnumerable<RequireSettings> GetRequiredResources(string type);
        IList<ResourceRequiredContext> BuildRequiredResources(string resourceType);
        IList<LinkEntry> GetRegisteredLinks();
        IList<MetaEntry> GetRegisteredMetas();
        IList<ScriptEntry> GetRegisteredHeadScripts();
        IList<ScriptEntry> GetRegisteredFootScripts();
        IEnumerable<IResourceManifest> ResourceProviders { get; }
        ResourceManifest DynamicResources { get; }
        ResourceDefinition FindResource(RequireSettings settings);
        void NotRequired(string resourceType, string resourceName);
        RequireSettings Include(string resourceType, string resourcePath, string resourceDebugPath);
        RequireSettings Include(string resourceType, string resourcePath, string resourceDebugPath, string relativeFromPath);
        RequireSettings Require(string resourceType, string resourceName);
        void RegisterHeadScript(ScriptEntry script);
        void RegisterFootScript(ScriptEntry script);
        void RegisterLink(LinkEntry link);
        void SetMeta(MetaEntry meta);
        void AppendMeta(MetaEntry meta, string contentSeparator);

        void WriteResources();
        string GetTemplateNotCache(string resourceName,string webVirualPath);
    }
}
