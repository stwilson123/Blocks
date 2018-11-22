using Blocks.Framework.Localization;

namespace Blocks.Framework.Authorization.Permission
{
    public class PermissionInfo
    {
        public string Name { get; internal set; }

        public string Navigation { get; internal set; }
        
        public ILocalizableString DisplayName { get;internal set; }
       
        public static PermissionInfo Create(string name,string navigation,ILocalizableString displayName) {
            return new PermissionInfo { Name = name , Navigation = navigation, DisplayName =  displayName};
        }
    }
}