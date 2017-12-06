using System.Collections.Generic;
using System.Reflection;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using Abp.Modules;
using Abp.Web;
using Abp.Web.Mvc;
using Abp.WebApi.Configuration;
using Abp.WebApi.Controllers.Dynamic;
using Abp.WebApi.Controllers.Dynamic.Selectors;
using Blocks.Framework.Modules;
using Blocks.Framework.Web.Api.Controllers.Selectors;
using Blocks.Framework.Web.Mvc;
using Blocks.Framework.Web.Mvc.ViewEngines;
using Blocks.Framework.Web.Mvc.ViewEngines.ThemeAwareness;
using Blocks.Framework.Web.Route;

namespace Blocks.Framework.Web.Modules
{
    [DependsOn(typeof(BlocksFrameworkModule))]
    [DependsOn(typeof(AbpWebMvcModule))]
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
