using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Web;
using Abp.Web.Mvc;
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
using Newtonsoft.Json.Converters;

namespace Blocks.Framework.Web.Modules
{
    [DependsOn(typeof(BlocksFrameworkModule))]
    [DependsOn(typeof(AbpWebMvcModule))]
    [DependsOn(typeof(AbpWebApiModule))]
   public class BlocksFrameworkWebModule : AbpModule
    {
        public override void PreInitialize()
        {
      


            
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
