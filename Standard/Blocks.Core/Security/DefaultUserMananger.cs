using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Dependency;
using Blocks.Core.Security.Authorization.Permission;
using Blocks.Core.Security.Events;
using Blocks.Framework.Caching;
using Blocks.Framework.Event;
using Blocks.Framework.NullObject;
using Blocks.Framework.Security;
using Blocks.Framework.Security.Authorization.Permission;
using Blocks.Framework.Security.Authorization.User;

namespace Blocks.Core.Security
{
    public class DefaultUserManager : IUserManager,IDomainEventHandler<PermissionChangeEventData>
    {
        IIocManager _iocManager;
        private readonly ICacheManager _cacheManager;

        internal static string getPermissionKey(params object[] param)
        {
            return string.Format("permission-userId-{0}",param);
        }

        public DefaultUserManager(IIocManager iocManager,ICacheManager cacheManager)
        {
            this._iocManager = iocManager;
            _cacheManager = cacheManager;
        }
        //public Task<bool> IsGrantedAsync(IUserIdentifier user, string permission)
        //{
        //    return Task.FromResult(true);
        //}

        public Task<bool> IsGrantedAsync(IUserIdentifier user, Permission permission)
        {
            Task<bool> result = Task.FromResult(true);


            var permissionCache = _cacheManager.GetCache<string,List<PermissionItem>>();
            var permissionCacheKey = getPermissionKey(user.UserId);
            var userPermissionList = permissionCache.Get(permissionCacheKey, (key) =>
            {
                return  (TaskResult(user, permission).Result ) ? new List<PermissionItem>()
                {
                    new PermissionItem(permission.ResourceKey,true)
                } : new List<PermissionItem>(){  };
            });
            var curResourcePermission = userPermissionList.FirstOrDefault(p => p.ResourceKey == permission.ResourceKey);
            if(curResourcePermission != null )
                return curResourcePermission.IsGranted ? Task.FromResult<bool>(true) : Task.FromResult(false);

            var isGranted = TaskResult(user, permission).Result;
            userPermissionList.Add(new PermissionItem(permission.ResourceKey, isGranted));
            permissionCache.Put(permissionCacheKey, userPermissionList);
            return Task.FromResult(isGranted);
        }

        private Task<bool> TaskResult(IUserIdentifier user, Permission permission)
        {
            var taskResult = Task.FromResult(true);
            if (_iocManager.IsRegistered<IPermissionCheck>())
            {
                taskResult = _iocManager.Resolve<IPermissionCheck>().IsGrantedAsync(user, permission);
            }

            return taskResult;
        }

        public void HandleEvent(PermissionChangeEventData eventData)
        {
            var permissionCacheKey = DefaultUserManager.getPermissionKey(eventData.UserId);
            var permissionCache = _cacheManager.GetCache<string, List<PermissionItem>>();

            if (eventData.ResourceKey == "*")
            {
                permissionCache.Remove(permissionCacheKey);
                return;
            }
            
            var userPermissionList = permissionCache.Get(permissionCacheKey, (key) =>
            {
                return  new List<PermissionItem>(){  };
            });

            userPermissionList.RemoveAll(p => p.ResourceKey == eventData.ResourceKey);
            permissionCache.Put(permissionCacheKey, userPermissionList);
        }
    }

    class PermissionItem
    {
        public PermissionItem(string resourceKey, bool isGranted)
        {
            ResourceKey = resourceKey;
            IsGranted = isGranted;
        }


        public string ResourceKey { get; set; }

        public bool IsGranted { get; set; }

    }
}