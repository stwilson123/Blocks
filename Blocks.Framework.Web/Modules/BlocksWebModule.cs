using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Blocks.Framework.Web.Route;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Blocks.Framework.Web.Mvc.ViewEngines.ThemeAwareness;
using Blocks.Framework.Web.Mvc;
using Abp.WebApi.Configuration;
using System.Web.Http.Dispatcher;
using Blocks.Framework.Web.Api.Controllers.Selectors;
using Abp.WebApi.Controllers.Dynamic;

namespace Blocks.Framework.Modules
{
    public abstract class BlocksWebModule : AbpModule
    {

        public override void PreInitialize()
        {
            //Rigister WebApi
            IocManager.Register<IHttpRouteProvider, StandardExtensionHttpRouteProvider>();
            IocManager.Register<IRouteProvider, StandardExtensionRouteProvider>();

            //Rigister WebMvc
            IocManager.Register<IRoutePublisher, RoutePublisher>();
         
        }

        /// <summary>
        /// This is the first event called on application startup. 
        /// Codes can be placed here to run before dependency injection registrations.
        /// </summary>
        /// This method is used to register dependencies for this module.
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(this.GetType().Assembly);

            //Config WebMvc
            ControllerBuilder.Current.SetControllerFactory(new BlocksWebMvcControllerFactory(IocManager));

            //Config WebApi
            var httpConfiguration = IocManager.Resolve<IAbpWebApiConfiguration>().HttpConfiguration;
            httpConfiguration.Services.Replace(typeof(IHttpControllerSelector), new BlocksHttpControllerSelector(httpConfiguration, IocManager.Resolve<DynamicApiControllerManager>(), IocManager));

            //Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
            //    .ForAll<IApplicationService>(typeof(BlocksApplicationModule).Assembly, "app")
            //    .Build();

            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));

            RouteHandle();
            InitializeEvent();
        }


        /// <summary>
        /// This is the first event called on application startup. 
        /// Codes can be placed here to run before dependency injection registrations.
        /// </summary>
        /// This method is used to register dependencies for this module.
        /// </summary>
        public virtual void InitializeEvent()
        {

        }
 

        private void RouteHandle()
        {
            var listRouteDesc = new List<RouteDescriptor>();
            IocManager.Resolve<IRouteProvider>().GetRoutes(listRouteDesc, this.GetType().Assembly.GetName().Name);
            IocManager.Resolve<IHttpRouteProvider>().GetRoutes(listRouteDesc, this.GetType().Assembly.GetName().Name);

            IocManager.Resolve<IRoutePublisher>().Publish(listRouteDesc);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ThemeAwareViewEngineShim(IocManager));

        }
    }
}
