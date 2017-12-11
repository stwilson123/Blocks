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
           
        }
    }
}
