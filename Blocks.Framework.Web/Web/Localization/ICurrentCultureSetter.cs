using System.Web;
using Microsoft.AspNetCore.Http;

namespace Blocks.Framework.Web.Web.Localization
{
    public interface ICurrentCultureSetter
    {
        void SetCurrentCulture(HttpContext httpContext);
    }
}
