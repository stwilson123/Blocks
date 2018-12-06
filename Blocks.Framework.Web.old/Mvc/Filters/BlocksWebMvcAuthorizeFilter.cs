using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.FileSystems.VirtualPath;
using Blocks.Framework.Ioc.Dependency;
using Blocks.Framework.Navigation.Manager;
using Blocks.Framework.Security;
using Blocks.Framework.Security.Authorization;
using Blocks.Framework.Security.Authorization.Permission;
using Blocks.Framework.Web.Mvc.Route;

namespace Blocks.Framework.Web.Mvc.Filters
{
    public class BlocksWebMvcAuthorizeFilter : IAuthorizationFilter, ITransientDependency
    {
        private readonly INavigationManager _navigationManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContext _userContext;


        public BlocksWebMvcAuthorizeFilter(INavigationManager navigationManager, IAuthorizationService authorizationService,IUserContext userContext
            )
        {
            this._navigationManager = navigationManager;
            this._authorizationService = authorizationService;
            this._userContext = userContext;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {

            
          ;
        }
    }
     
}