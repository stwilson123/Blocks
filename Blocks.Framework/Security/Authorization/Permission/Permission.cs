using Blocks.Framework.Localization;

namespace Blocks.Framework.Security.Authorization.Permission
{
    public class Permission
    {
        public string Name { get; internal set; }

        public string Navigation { get; internal set; }
        
        public ILocalizableString DisplayName { get;internal set; }
       
        public IPermissionDependency PermissionDependency { get;internal set;  }
        
        public static Permission Create(string name,string navigation,ILocalizableString displayName,IPermissionDependency permissionDependency = null) {
            return new Permission { Name = name , Navigation = navigation, DisplayName =  displayName,
                PermissionDependency = permissionDependency == null ? new DefaultPermissionDependency(name) : permissionDependency};
        }
    }
}