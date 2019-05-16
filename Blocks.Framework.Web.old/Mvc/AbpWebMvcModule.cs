using System;
using System.ComponentModel;
using System.Reflection;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Web;
using Abp.Web.Security.AntiForgery;
using Blocks.Framework.Configurations;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.Web.Mvc.Configuration;
using Blocks.Framework.Web.Mvc.Controllers;
using Blocks.Framework.Web.Mvc.Controllers.Factory;
using Blocks.Framework.Web.Mvc.Filters;
using Blocks.Framework.Web.Mvc.ModelBinding.Binders;
using Blocks.Framework.Web.Mvc.Security.AntiForgery;
using Blocks.Framework.Web.Mvc.ViewEngines.ThemeAwareness;
using Blocks.Framework.Web.Route;
using Blocks.Framework.Web.Web;

namespace Blocks.Framework.Web.Mvc
{
    /// <summary>
    /// This module is used to build ASP.NET MVC web sites using Abp.
    /// </summary>
   [DependsOn(typeof(AbpWebModule))]
    public class AbpWebMvcModule : AbpModule
    {
        /// <inheritdoc/>
        public override void PreInitialize()
        {
            
            //Rigister WebMvc
            IocManager.Register<IRoutePublisher, RoutePublisher>();
            
           // IocManager.AddConventionalRegistrar(new ControllerConventionalRegistrar());

            IocManager.Register<IAbpMvcConfiguration, AbpMvcConfiguration>();

            Configuration.ReplaceService<IAbpAntiForgeryManager, AbpMvcAntiForgeryManager>();
//            IocManager.AddConventionalRegistrar(
//                new ControllerConventionalRegistrar(IocManager.Resolve<IExtensionManager>().AvailableExtensions()));

            IocManager.Register<MvcControllerBuilderFactory>();

        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            //Config init 
            IocManager.AddConventionalRegistrar(new ConfiguartionConventionalRegistrar(IocManager.Resolve<IExtensionManager>().AvailableExtensions()));
            IocManager.AddConventionalRegistrar(
                new ControllerConventionalRegistrar(IocManager.Resolve<IExtensionManager>().AvailableExtensions()));


            //Config WebMvc
            ControllerBuilder.Current.SetControllerFactory(new BlocksWebMvcControllerFactory(IocManager));
            //TODO how ajust to abp filter
            //  FilterProviders.Providers.Add(new BlocksWebMvcFilterProvider());

            System.Web.Mvc.ViewEngines.Engines.Clear();
            System.Web.Mvc.ViewEngines.Engines.Add(new ThemeAwareViewEngineShim(IocManager));

             

            //  ControllerBuilder.Current.SetControllerFactory(new BlocksWebMvcControllerFactory(IocManager));
            //  HostingEnvironment.RegisterVirtualPathProvider(IocManager.Resolve<EmbeddedResourceVirtualPathProvider>());
            //  HostingEnvironment.RegisterVirtualPathProvider(IocManager.Resolve<EmbeddedResourceVirtualPathProvider>());
        }

        /// <inheritdoc/>
        public override void PostInitialize()
        {
//            GlobalFilters.Filters.Add(IocManager.Resolve<AbpMvcAuthorizeFilter>());
            GlobalFilters.Filters.Add(IocManager.Resolve<AbpAntiForgeryMvcFilter>());
            //            GlobalFilters.Filters.Add(IocManager.Resolve<AbpMvcAuditFilter>());
            //            GlobalFilters.Filters.Add(IocManager.Resolve<AbpMvcValidationFilter>());
            //            GlobalFilters.Filters.Add(IocManager.Resolve<AbpMvcUowFilter>());
            
            GlobalFilters.Filters.Add(IocManager.Resolve<BlocksWebMvcAuthorizeFilter>());
            GlobalFilters.Filters.Add(IocManager.Resolve<BlocksWebMvcActionFilter>());
            GlobalFilters.Filters.Add(IocManager.Resolve<BlocksWebMvcResultFilter>());
            GlobalFilters.Filters.Add(IocManager.Resolve<BlocksWebMvcExceptionFilter>());

            //            var abpMvcDateTimeBinder = new AbpMvcDateTimeBinder();
            //            ModelBinders.Binders.Add(typeof(DateTime), abpMvcDateTimeBinder);
            //            ModelBinders.Binders.Add(typeof(DateTime?), abpMvcDateTimeBinder);


        }
    }
}
