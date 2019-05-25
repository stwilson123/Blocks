using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Blocks.Framework.Ioc.Dependency;
using Blocks.Framework.Navigation;
using Blocks.Framework.Security.Authorization.Permission.Provider;
using Blocks.Framework.Utility.Extensions;

namespace Blocks.Framework.Security.Authorization.Permission
{
    internal class PermissionManager : IPermissionManager 
    {
        private readonly IEnumerable<IRolePermissionProvider> _rolePermissionProviders;
        private readonly IEnumerable<IPermissionProvider> _providers;
        private IDictionary<string, IList<IPermission>> _rolePermissions;

        private IDictionary<string, IPermission> _permissions;

        public PermissionManager(IEnumerable<IPermissionProvider> providers,
            IEnumerable<IRolePermissionProvider> rolePermissionProviders)
        {
            _rolePermissionProviders = rolePermissionProviders;
            _providers = providers;
            _rolePermissions = new Dictionary<string, IList<IPermission>>();
            _permissions = new Dictionary<string, IPermission>();
        }

        public void Initialize()
        {
            foreach (var provider in _providers)
            {
                foreach (var permission in provider.GetPermissions())
                {
                    if (_permissions.ContainsKey(permission.ResourceKey))
                        throw new Exception($"Double permission resourceKey {permission.ResourceKey}");
                    _permissions.Add(permission.ResourceKey, permission);
                }
            }
        }

        public void InitializeRolePermission(string RoleId)
        {
            foreach (var rolePermissionProvider in _rolePermissionProviders)
            {
                foreach (var rolePermission in rolePermissionProvider.GetPermissions(RoleId))
                {
                    if (!_rolePermissions.ContainsKey(rolePermission.Key))
                    {
                        _rolePermissions.Add(rolePermission.Key,
                            ToList(rolePermission));
                        continue;
                    }
                    _rolePermissions[rolePermission.Key] = ToList(rolePermission);
                }
            }
        }

        private List<IPermission> ToList(KeyValuePair<string, IList<string>> rolePermission)
        {
            return rolePermission.Value.Select(v =>
            {
                var result = default(IPermission);
                if (v != null)
                    _permissions.TryGetValue(v, out result);
                return  result;
            }).Where(p => p != null).ToList();
        }

        public IDictionary<string, IList<IPermission>> GetAllPermissions()
        {
            return _rolePermissions;
        }
    }
}