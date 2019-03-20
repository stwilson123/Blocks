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
using System.Configuration;
using System.Security.Claims;
using System.Threading;
using Abp.Runtime.Security;
using Blocks.Framework.Security;
using EntityFramework.Test.Model;
using Moq;

namespace EntityFramework.Test
{
    [DependsOn(
        typeof(AbpTestBaseModule),
        typeof(AbpAutoMapperModule),
     //   typeof(BlocksDataModule),
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


            //TODO unit test can't read config from appconfig.
             
//            var testUserContext = new Mock<ClaimsPrincipal>();
//                testUserContext.Setup(u => u.Claims)
//                .Returns(new List<Claim>{ new Claim(AbpClaimTypes.UserId,"testId"),new Claim(AbpClaimTypes.UserName,"testName") });

            Thread.CurrentPrincipal = new ClaimsPrincipal(new List<ClaimsIdentity>
            {
                new ClaimsIdentity(new List<Claim>(){ new Claim(AbpClaimTypes.UserId,"testId"),new Claim(AbpClaimTypes.UserName,"testName")})
               
            }){ };
        }


        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());


          //  IocManager.Register<IUserContext,TestUserContext>();
                
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