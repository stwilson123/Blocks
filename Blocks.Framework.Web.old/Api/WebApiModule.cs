using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.ModelBinding;
using System.Web.Mvc;
using Abp.Json;
using Abp.Logging;
using Abp.Modules;
using Abp.Web;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.Ioc;
using Blocks.Framework.Services.DataTransfer;
using Blocks.Framework.Utility.SafeConvert;
using Blocks.Framework.Web.Api.Configuration;
using Blocks.Framework.Web.Api.Configuration.Startup;
using Blocks.Framework.Web.Api.Controllers;
using Blocks.Framework.Web.Api.Controllers.Dynamic;
using Blocks.Framework.Web.Api.Controllers.Dynamic.Builders;
using Blocks.Framework.Web.Api.Controllers.Dynamic.Selectors;
using Blocks.Framework.Web.Api.Filter;
using Blocks.Framework.Web.Api.Uow;
using Blocks.Framework.Web.Mvc.Controllers;
using Blocks.Framework.Web.Mvc.Filters;
using Blocks.Framework.Web.Route;
using Blocks.Framework.Web.Web;
using Castle.MicroKernel.Registration;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Blocks.Framework.Web.Api
{
    /// <summary>
    /// This module provides Abp features for ASP.NET Web API.
    /// </summary>
    [DependsOn(typeof(BlocksWebModule))]
    public class WebApiModule : AbpModule
    {
        /// <inheritdoc/>
        public override void PreInitialize()
        {
            //Move to Initialize
            //IocManager.AddConventionalRegistrar(new ApiControllerConventionalRegistrar());

            IocManager.Register<IDynamicApiControllerBuilder, DynamicApiControllerBuilder>();
            IocManager.Register<IAbpWebApiConfiguration, AbpWebApiConfiguration>();

            // Configuration.Settings.Providers.Add<ClearCacheSettingProvider>();

            Configuration.Modules.AbpWebApi().ResultWrappingIgnoreUrls.Add("/swagger");

            //Rigister WebApi
            IocManager.Register<IHttpRouteProvider, StandardExtensionHttpRouteProvider>();
            IocManager.Register<IRouteProvider, StandardExtensionRouteProvider>();
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            //IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //Config WebMvc,Webi register

            IocManager.AddConventionalRegistrar(
                new ApiControllerConventionalRegistrar(IocManager.Resolve<IExtensionManager>().AvailableExtensions()));

            //Config WebApi
            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));
        }

        public override void PostInitialize()
        {
            //Config WebApi
            var httpConfiguration = Configuration.Modules.AbpWebApi().HttpConfiguration;
   
           

     
            
            
          //  var httpConfiguration = IocManager.Resolve<IAbpWebApiConfiguration>().HttpConfiguration;

            InitializeAspNetServices(httpConfiguration);
            InitializeFilters(httpConfiguration);
            InitializeFormatters(httpConfiguration);
            InitializeRoutes(httpConfiguration);
            InitializeModelBinders(httpConfiguration);

            foreach (var controllerInfo in IocManager.Resolve<DynamicApiControllerManager>().GetAll())
            {
                IocManager.IocContainer.Register(
                    Component.For(controllerInfo.InterceptorType).LifestyleTransient(),
                    Component.For(controllerInfo.ApiControllerType)
                        .Proxy.AdditionalInterfaces(controllerInfo.ServiceInterfaceType)
                        .Activator<DefaultBlocksComponentActivator>()
                        .Interceptors(controllerInfo.InterceptorType)
                        .LifestyleTransient()
                        
                );

                LogHelper.Logger.DebugFormat(
                    "Dynamic web api controller is created for type '{0}' with service name '{1}'.",
                    controllerInfo.ServiceInterfaceType.FullName, controllerInfo.ServiceName);
            }

            Configuration.Modules.AbpWebApi().HttpConfiguration.EnsureInitialized();
        }

        private void InitializeAspNetServices(HttpConfiguration httpConfiguration)
        {
              httpConfiguration.Services.Replace(typeof(IHttpControllerSelector),  new BlocksHttpControllerSelector(httpConfiguration, IocManager.Resolve<DynamicApiControllerManager>(),
                  IocManager));
              httpConfiguration.Services.Replace(typeof(IHttpActionSelector), new AbpApiControllerActionSelector(IocManager.Resolve<IAbpWebApiConfiguration>()));
              httpConfiguration.Services.Replace(typeof(IHttpControllerActivator), new BlocksApiControllerActivator(IocManager));
//            httpConfiguration.Services.Replace(typeof(IApiExplorer), IocManager.Resolve<AbpApiExplorer>());
        }

        private void InitializeFilters(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Filters.Add(IocManager.Resolve<BlocksApiExceptionFilterAttribute>());
            httpConfiguration.Filters.Add(IocManager.Resolve<BlocksApiActionFilterAttribute>());

  
//            httpConfiguration.Filters.Add(IocManager.Resolve<AbpApiAuthorizeFilter>());
//            httpConfiguration.Filters.Add(IocManager.Resolve<AbpAntiForgeryApiFilter>());
//            httpConfiguration.Filters.Add(IocManager.Resolve<AbpApiAuditFilter>());
//            httpConfiguration.Filters.Add(IocManager.Resolve<AbpApiValidationFilter>());
              httpConfiguration.Filters.Add(IocManager.Resolve<BlocksApiUowFilter>());
//            //TODO extensions
//
//            httpConfiguration.MessageHandlers.Add(IocManager.Resolve<ResultWrapperHandler>());
        }

        private static void InitializeFormatters(HttpConfiguration httpConfiguration)
        {
            //Remove formatters except JsonFormatter.
            foreach (var currentFormatter in httpConfiguration.Formatters.ToList())
            {
                if (!(currentFormatter is JsonMediaTypeFormatter ||
                      currentFormatter is JQueryMvcFormUrlEncodedFormatter))
                {
                    httpConfiguration.Formatters.Remove(currentFormatter);
                }
            }

            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new JsonAttribuateContractResolver();
            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.Converters.Insert(0,
                new IsoDateTimeConverter()
                {
                    DateTimeFormat = SafeConvert.DateTimeFormat[(Int32) (TimeFormatEnum.UTCDateTime)]
                });
            
//            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
//                new CamelCasePropertyNamesContractResolver();
            
            
//            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.Converters.Insert(0,
//                new AbpDateTimeConverter());
            //  httpConfiguration.Formatters.Add(new PlainTextFormatter());
        }

        private static void InitializeRoutes(HttpConfiguration httpConfiguration)
        {
            //Dynamic Web APIs

            httpConfiguration.Routes.MapHttpRoute(
                //name: "AbpDynamicWebApi",
                name: "BlocksDynamicWebApi",
                routeTemplate: "api/services/{*serviceNameWithAction}"
            );

            //Other routes

            httpConfiguration.Routes.MapHttpRoute(
//                name: "AbpCacheController_Clear",
                name: "BlocksCacheController_Clear",
                routeTemplate: "api/AbpCache/Clear",
                defaults: new {controller = "AbpCache", action = "Clear"}
            );

            httpConfiguration.Routes.MapHttpRoute(
//                name: "AbpCacheController_ClearAll",
                name: "BlocksCacheController_ClearAll",
                routeTemplate: "api/AbpCache/ClearAll",
                defaults: new {controller = "AbpCache", action = "ClearAll"}
            );
        }

        private static void InitializeModelBinders(HttpConfiguration httpConfiguration)
        {
//            var abpApiDateTimeBinder = new AbpApiDateTimeBinder();
//            httpConfiguration.BindParameter(typeof(DateTime), abpApiDateTimeBinder);
//            httpConfiguration.BindParameter(typeof(DateTime?), abpApiDateTimeBinder);
        }
    }
}