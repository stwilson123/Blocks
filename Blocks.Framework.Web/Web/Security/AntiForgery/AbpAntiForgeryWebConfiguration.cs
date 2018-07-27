﻿using System.Collections.Generic;
using Abp.Web;

namespace Blocks.Framework.Web.Web.Security.AntiForgery
{
    public class AbpAntiForgeryWebConfiguration : IAbpAntiForgeryWebConfiguration
    {
        public bool IsEnabled { get; set; }

        public HashSet<HttpVerb> IgnoredHttpVerbs { get; }

        public AbpAntiForgeryWebConfiguration()
        {
            IsEnabled = true;
            IgnoredHttpVerbs = new HashSet<HttpVerb> { HttpVerb.Get, HttpVerb.Head, HttpVerb.Options, HttpVerb.Trace };
        }
    }
}