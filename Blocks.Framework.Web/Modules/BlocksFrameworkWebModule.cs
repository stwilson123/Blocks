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
            //Rigister WebApi
            IocManager.Register<IHttpRouteProvider, StandardExtensionHttpRouteProvider>();
            IocManager.Register<IRouteProvider, StandardExtensionRouteProvider>();

            //Rigister WebMvc
            IocManager.Register<IRoutePublisher, RoutePublisher>();

            
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //Config WebMvc
            ControllerBuilder.Current.SetControllerFactory(new BlocksWebMvcControllerFactory(IocManager));
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ThemeAwareViewEngineShim(IocManager));

            //Config WebApi
            var httpConfiguration = IocManager.Resolve<IAbpWebApiConfiguration>().HttpConfiguration;
            httpConfiguration.Services.Replace(typeof(IHttpControllerSelector), new BlocksHttpControllerSelector(httpConfiguration, IocManager.Resolve<DynamicApiControllerManager>(), IocManager));
        }

        public override void PostInitialize()
        {
       

        }
    }
}
