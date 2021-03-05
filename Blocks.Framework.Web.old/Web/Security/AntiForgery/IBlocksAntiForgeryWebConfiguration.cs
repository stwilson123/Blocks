using Blocks.Framework.Web.Web.HttpMethod;
using System.Collections.Generic;

namespace Blocks.Framework.Web.Web.Security.AntiForgery
{
    /// <summary>
    /// Common configuration shared between ASP.NET MVC and ASP.NET Web API.
    /// </summary>
    public interface IBlocksAntiForgeryWebConfiguration
    {
        /// <summary>
        /// Used to enable/disable Anti Forgery token security mechanism of ABP.
        /// Default: true (enabled).
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// A list of ignored HTTP verbs for Anti Forgery token validation.
        /// Default list: Get, Head, Options, Trace.
        /// </summary>
        HashSet<HttpVerb> IgnoredHttpVerbs { get; }
    }
}
