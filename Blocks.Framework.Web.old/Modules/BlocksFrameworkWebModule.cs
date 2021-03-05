using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.Results;
using System.Web.Mvc;
using Abp;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Web;
using Blocks.Framework.Configurations;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.Json.Convert;
using Blocks.Framework.Modules;
using Blocks.Framework.Services.DataTransfer;
using Blocks.Framework.Utility.SafeConvert;
using Blocks.Framework.Web.Api;
using Blocks.Framework.Web.Api.Configuration.Startup;
using Blocks.Framework.Web.Api.Controllers;
using Blocks.Framework.Web.Api.Controllers.Dynamic;
using Blocks.Framework.Web.Api.Controllers.Dynamic.Selectors;
using Blocks.Framework.Web.Api.Filter;
using Blocks.Framework.Web.Mvc;
using Blocks.Framework.Web.Mvc.Controllers;
using Blocks.Framework.Web.Mvc.Filters;
using Blocks.Framework.Web.Mvc.ViewEngines;
using Blocks.Framework.Web.Mvc.ViewEngines.ThemeAwareness;
using Blocks.Framework.Web.Route;
using Castle.Core.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Blocks.Framework.Web.Modules
{
    [DependsOn(typeof(BlocksFrameworkModule))]
    [DependsOn(typeof(BlocksWebMvcModule))]
    [DependsOn(typeof(WebApiModule))]
   public class BlocksFrameworkWebModule : AbpModule
    {
        public override void PreInitialize()
        {
      
//            Configuration.Auditing.Selectors.Add(
//                new NamedTypeSelector(
//                    "Blocks.Mvc.Controller",
//                    type => typeof(IController).IsAssignableFrom(type)  
//                )
//            );
            
//            Configuration.Auditing.IgnoredTypes.AddRange(new Type[]
//            {
//                typeof(ResultExecutingContext),typeof(ResultExecutedContext),typeof(AuthorizationContext),typeof(ExceptionContext),
//                typeof(ActionExecutingContext),typeof(ActionExecutedContext) 
//
//            });
//            Func<ControllerContext, string> func = (type) => JsonConvert.SerializeObject(type?.RouteData?.Values);
//
//            Configuration.Auditing.TypeConverts.Add(typeof(ResultExecutingContext), (obj) =>
//                func(obj as ResultExecutingContext)
//            );
//            Configuration.Auditing.TypeConverts.Add(typeof(ResultExecutedContext), (obj) =>
//                func(obj as ResultExecutedContext)
//            );
//            Configuration.Auditing.TypeConverts.Add(typeof(AuthorizationContext), (obj) =>
//                func(obj as AuthorizationContext)
//            );
//            Configuration.Auditing.TypeConverts.Add(typeof(ExceptionContext), (obj) =>
//                func(obj as ExceptionContext)
//            );
//            Configuration.Auditing.TypeConverts.Add(typeof(ActionExecutingContext), (obj) =>
//                func(obj as ActionExecutingContext)
//            );
//            Configuration.Auditing.TypeConverts.Add(typeof(ActionExecutedContext), (obj) =>
//                func(obj as ActionExecutedContext)
//            );
//            Configuration.Auditing.Selectors.Add(
//                new NamedTypeSelector(
//                    "Blocks.Mvc.Filter",
//                    type => typeof(IActionFilter).IsAssignableFrom(type) || typeof(IAuthorizationFilter).IsAssignableFrom(type) || 
//                            typeof(IExceptionFilter).IsAssignableFrom(type) || typeof(IResultFilter).IsAssignableFrom(type)
//                )
//            );
        }

        public override void Initialize()
        {
            
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

        }

        public override void PostInitialize()
        {
          
        }
    }
}
