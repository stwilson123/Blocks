using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Web;
using Abp.Web.Mvc;
using Abp.WebApi.Configuration;
using Abp.WebApi.Controllers.Dynamic;
using Abp.WebApi.Controllers.Dynamic.Selectors;
using Blocks.Framework.Configurations;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.Json.Convert;
using Blocks.Framework.Modules;
using Blocks.Framework.Services.DataTransfer;
using Blocks.Framework.Web.Api.Controllers;
using Blocks.Framework.Web.Api.Controllers.Selectors;
using Blocks.Framework.Web.Api.Filter;
using Blocks.Framework.Web.Mvc;
using Blocks.Framework.Web.Mvc.Controllers;
using Blocks.Framework.Web.Mvc.Filters;
using Blocks.Framework.Web.Mvc.ViewEngines;
using Blocks.Framework.Web.Mvc.ViewEngines.ThemeAwareness;
using Blocks.Framework.Web.Route;
using Castle.Core.Logging;

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


            //Config WebMvc,Webi register
            IocManager.AddConventionalRegistrar(new ControllerConventionalRegistrar(IocManager.Resolve<IExtensionManager>().AvailableExtensions()));
            IocManager.AddConventionalRegistrar(new ApiControllerConventionalRegistrar(IocManager.Resolve<IExtensionManager>().AvailableExtensions()));

            //Config init 
            IocManager.AddConventionalRegistrar(new ConfiguartionConventionalRegistrar(IocManager.Resolve<IExtensionManager>().AvailableExtensions()));

            

            //Config WebMvc
            ControllerBuilder.Current.SetControllerFactory(new BlocksWebMvcControllerFactory(IocManager));
          //TODO how ajust to abp filter
            //  FilterProviders.Providers.Add(new BlocksWebMvcFilterProvider());

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ThemeAwareViewEngineShim(IocManager));

            
            //Config WebApi
            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));

        }

        public override void PostInitialize()
        {
            //Config WebApi
            var httpConfiguration = Configuration.Modules.AbpWebApi().HttpConfiguration;
            httpConfiguration.Services.Replace(typeof(IHttpControllerSelector), new BlocksHttpControllerSelector(httpConfiguration, IocManager.Resolve<DynamicApiControllerManager>(),IocManager));
            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new JsonAttribuateContractResolver();
            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new BlocksIsoDateTimeConverter());
            httpConfiguration.Filters.Add(IocManager.Resolve<BlocksApiExceptionFilterAttribute>());
            httpConfiguration.Filters.Add(IocManager.Resolve<BlocksApiActionFilterAttribute>());

           GlobalFilters.Filters.Add(IocManager.Resolve<BlocksWebMvcActionFilter>());
           GlobalFilters.Filters.Add(IocManager.Resolve<BlocksWebMvcResultFilter>());

        }
    }
}
