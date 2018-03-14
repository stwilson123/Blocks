using System.Web;
using Blocks.Framework.Web.Mvc.UI.Resources;

namespace Blocks.Framework.Web.Mvc.UI.Extensions
{
    public static class ViewBagExtensions
    {
        public static void SetResourceManager(HttpContextBase viewBage,IResourceManager resourceManager)
        {
              viewBage.Items["IResourceManager"] = resourceManager;
        }
        
        public static IResourceManager GetResourceManager(HttpContextBase viewBage)
        {
            return (IResourceManager)viewBage.Items["IResourceManager"];
        }
    }
}