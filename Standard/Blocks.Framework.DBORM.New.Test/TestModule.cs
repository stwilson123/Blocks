using System.Reflection;
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
using System.Diagnostics;
using System.Security.Claims;
using System.Threading;
using Abp.Runtime.Security;
using Blocks.Framework.Security;
using Castle.Core.Logging;
using EntityFramework.Test.Model;
using Moq;

namespace EntityFramework.Test
{
    [DependsOn(
        typeof(AbpTestBaseModule),
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
            
            IocManager.Register<ILogger,MyClass>();
          

        }
        
        
    }

    public class MyClass : ILogger
    {
        public ILogger CreateChildLogger(string loggerName)
        {
            throw new NotImplementedException();
        }

        public void Debug(string message)
        {
            Trace.TraceInformation(message);
        }

        public void Debug(Func<string> messageFactory)
        {
            throw new NotImplementedException();
        }

        public void Debug(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void DebugFormat(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void DebugFormat(Exception exception, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void DebugFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(Func<string> messageFactory)
        {
            throw new NotImplementedException();
        }

        public void Error(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void ErrorFormat(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void ErrorFormat(Exception exception, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void ErrorFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message)
        {
            throw new NotImplementedException();
        }

        public void Fatal(Func<string> messageFactory)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void FatalFormat(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void FatalFormat(Exception exception, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void FatalFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Info(string message)
        {
            throw new NotImplementedException();
        }

        public void Info(Func<string> messageFactory)
        {
            throw new NotImplementedException();
        }

        public void Info(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void InfoFormat(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void InfoFormat(Exception exception, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void InfoFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message)
        {
            throw new NotImplementedException();
        }

        public void Warn(Func<string> messageFactory)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void WarnFormat(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void WarnFormat(Exception exception, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void WarnFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public bool IsDebugEnabled { get; }
        public bool IsErrorEnabled { get; }
        public bool IsFatalEnabled { get; }
        public bool IsInfoEnabled { get; }
        public bool IsWarnEnabled { get; }
    }
}