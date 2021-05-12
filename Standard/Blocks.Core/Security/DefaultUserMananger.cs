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
    public class DefaultUserManager : IUserManager , IDomainEventHandler<PermissionChangeEventData>
    {
        IIocManager _iocManager;
        
        //TODO single class cache can't move to other class
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
            if (_iocManager.IsRegistered<IPermissionCheck>())
            {
                result = _iocManager.Resolve<IPermissionCheck>().IsGrantedAsync(user, permission);
            }
            return result;
        }

        public Task CheckUserStatus(IUserIdentifier user)
        {
            throw new System.NotImplementedException();
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

        public void testc()
        {
            throw new System.NotImplementedException();
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