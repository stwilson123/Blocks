using System.Collections.Generic;

namespace Blocks.Framework.Web.Web.Security.AntiForgery
{
    public class AbpAntiForgeryWebConfiguration : IAbpAntiForgeryWebConfiguration
    {
        public bool IsEnabled { get; set; }

        public HashSet<Abp.Web.HttpVerb> IgnoredHttpVerbs { get; }

        public AbpAntiForgeryWebConfiguration()
        {
            IsEnabled = true;
            IgnoredHttpVerbs = new HashSet<Abp.Web.HttpVerb> { Abp.Web.HttpVerb.Get, Abp.Web.HttpVerb.Head, Abp.Web.HttpVerb.Options, Abp.Web.HttpVerb.Trace };
        }
    }
}