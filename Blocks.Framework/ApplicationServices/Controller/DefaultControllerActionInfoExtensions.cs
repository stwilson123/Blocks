using Blocks.Framework.Reflection.Extensions;
using Blocks.Framework.Security.Authorization.Permission.Attributes;
using JetBrains.Annotations;

namespace Blocks.Framework.ApplicationServices.Controller
{
    public static class DefaultControllerActionInfoExtensions
    {
        
        public static string[] GetAuthorize(this DefaultControllerActionInfo controllerAction)
        {
            var permissionAttribute = controllerAction?.Method.GetSingleAttributeOrNull<BlocksAuthorizeAttribute>();
            return permissionAttribute?.Permissions;
        }

       
         
        
    }
}