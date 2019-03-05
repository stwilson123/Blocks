using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using Abp.TestBase;
using Blocks;
using Blocks.Framework.DBORM;
using Castle.MicroKernel.Registration;
using Blocks.Framework.Configurations;
using System.Linq;
using Blocks.Framework.FileSystems;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using Abp.Runtime.Security;
using Blocks.Framework.Security;
using Moq;

namespace EntityFramework.Test
{
    [DependsOn(
        typeof(AbpTestBaseModule),
        typeof(AbpAutoMapperModule),
        typeof(BlocksDataModule),
        typeof(BlocksFrameworkDBORMModule)
    )]
    public class TestModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Settings.Providers.Add<GlobalSettingProvider>();
            var a = WebHostingEnvironment.CreateHostingEnvironment(new WebHostingEnvironment()
            {
                ContentRootPath = AppDomain.CurrentDomain.BaseDirectory //HostingEnvironment.ApplicationPhysicalPath
            });
            IocManager.Register<IHostingEnvironment>(a);
            
            var testUserContext = new Mock<ClaimsPrincipal>();
            testUserContext.Setup(u => u.Claims)
                .Returns(new List<Claim>{ new Claim(AbpClaimTypes.UserId,"testId"),new Claim(AbpClaimTypes.UserName,"testName") });

            Thread.CurrentPrincipal = testUserContext.Object;
        }


        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

         
            
            IocManager.IocContainer.Register(
                Classes.FromAssembly(Assembly.GetExecutingAssembly())
                    .BasedOn<IConfiguration>()
                    .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                    .WithService.Select((t, baseTypes) => t.GetInterfaces().Where(bType => typeof(IConfiguration) != bType && typeof(IConfiguration).IsAssignableFrom(bType)))
                    .LifestyleTransient()
            );
        }
    }
}