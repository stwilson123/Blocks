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
                            rolePermission.Value.Select(v => _permissions[v]).ToList());
                        continue;
                    }
                    _rolePermissions[rolePermission.Key] = rolePermission.Value.Select(v => _permissions[v]).ToList();
                }
            }
        }

        public IDictionary<string, IList<IPermission>> GetAllPermissions()
        {
            return _rolePermissions;
        }
    }
}