using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Hangfire;
using Abp.Hangfire.Configuration;
using Abp.Zero.Configuration;
using Abp.Modules;
using Abp.Web.Mvc;
using Abp.Web.SignalR;
using Blocks.Api;
using Blocks.Framework.DBORM;
using Castle.MicroKernel.Registration;
using Hangfire;
using Microsoft.Owin.Security;
using Blocks.Framework.Modules;
using Blocks.Framework.Web.Modules;
using Blocks.Framework.Web.Route;

namespace Blocks.Web
{
    [DependsOn(
        typeof(BlocksDataModule),
        typeof(BlocksApplicationModule),
        typeof(BlocksWebApiModule),
        typeof(AbpWebSignalRModule),
        //typeof(AbpHangfireModule), - ENABLE TO USE HANGFIRE INSTEAD OF DEFAULT JOB MANAGER
     //   typeof(AbpWebMvcModule),
        typeof(BlocksFrameworkWebModule),
        typeof(BlocksFrameworkDBORMModule))]
    public class BlocksWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Enable database based localization
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<BlocksNavigationProvider>();

            IocManager.Register<RouteCollection>(RouteTable.Routes);
            Configuration.Settings.Providers.Add<GlobalSettingProvider>();

            //Configure Hangfire - ENABLE TO USE HANGFIRE INSTEAD OF DEFAULT JOB MANAGER
            //Configuration.BackgroundJobs.UseHangfire(configuration =>
            //{
            //    configuration.GlobalConfiguration.UseSqlServerStorage("Default");
            //});
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());


            IocManager.IocContainer.Register(
                Component
                    .For<IAuthenticationManager>()
                    .UsingFactoryMethod(() => HttpContext.Current.GetOwinContext().Authentication)
                    .LifestyleTransient()
            );
           // RouteHandle();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
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
