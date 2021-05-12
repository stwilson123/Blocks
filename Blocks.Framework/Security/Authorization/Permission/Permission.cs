using Blocks.Framework.Localization;

namespace Blocks.Framework.Security.Authorization.Permission
{
    public class Permission : IPermission
    {
        public static string PermissionTemplate = "{0}/{1}";
        public string Name { get; internal set; }

        public string Navigation { get; internal set; }

        public ILocalizableString DisplayName { get; internal set; }

        internal IPermissionDependency PermissionDependency { get; set; }

        public string Type { get; internal set; }

        public string ResourceKey { get; internal set; }

        public Permission Clone(Permission permission)
        {
            return Create(permission.Name, permission.Navigation, permission.Type, permission.ResourceKey,
                permission.DisplayName, permission.PermissionDependency);
        }

        public static Permission Create(string name, string navigation, string type,
            string resourceKey,
            ILocalizableString displayName, IPermissionDependency permissionDependency = null)
        {
            Permission p = new Permission
            {
                Name = name,
                Navigation = navigation,
                Type = type,
                ResourceKey = resourceKey,
                DisplayName = displayName,
            };
            var pDependency = permissionDependency != null ? permissionDependency :
               new DefaultPermissionDependency(p);
            p.PermissionDependency = pDependency;

            return p;
        }
    }
}