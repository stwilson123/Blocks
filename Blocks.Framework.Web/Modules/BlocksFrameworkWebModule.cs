using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using Abp.Modules;
using Abp.Web;
using Abp.Web.Mvc;
using Abp.WebApi.Configuration;
using Abp.WebApi.Controllers.Dynamic;
using Abp.WebApi.Controllers.Dynamic.Selectors;
using Blocks.Framework.Web.Api.Controllers.Selectors;
using Blocks.Framework.Web.Mvc;
using Blocks.Framework.Web.Route;

namespace Blocks.Framework.Web.Modules
{
    [DependsOn(typeof(AbpWebMvcModule))]
   public class BlocksFrameworkWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IHttpRouteProvider, StandardExtensionHttpRouteProvider>();
            IocManager.Register<IRouteProvider, StandardExtensionRouteProvider>();

            
            IocManager.Register<IRoutePublisher, RoutePublisher>();
        }

        public override void Initialize()
        {
            ControllerBuilder.Current.SetControllerFactory(new BlocksWebMvcControllerFactory(IocManager));

        }

        public override void PostInitialize()
        {
            var httpConfiguration = IocManager.Resolve<IAbpWebApiConfiguration>().HttpConfiguration;
            httpConfiguration.Services.Replace(typeof(IHttpControllerSelector), new BlocksHttpControllerSelector(httpConfiguration, IocManager.Resolve<DynamicApiControllerManager>(),IocManager));

        }
    }
}
