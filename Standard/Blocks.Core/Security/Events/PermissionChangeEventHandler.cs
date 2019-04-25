using System.Collections.Generic;
using Blocks.Framework.Caching;
using Blocks.Framework.Event;

namespace Blocks.Core.Security.Events
{
    public class PermissionChangeEventHandler  
    {
        private readonly ICacheManager _cacheManager;

        public PermissionChangeEventHandler(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public void HandleEvent(PermissionChangeEventData eventData)
        {
            bool isRemoveAll = eventData.UserId == "*" ;
            
            var permissionCacheKey = DefaultUserManager.getPermissionKey(eventData.UserId);
            var permissionCache = _cacheManager.GetCache<string, List<PermissionItem>>();

            if (isRemoveAll && eventData.ResourceKey == "*")
            {
                //TODO according to  ResourceKey to remove
                permissionCache.Remove();
            }
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
}