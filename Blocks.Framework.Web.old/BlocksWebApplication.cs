using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using Abp;
using Abp.Castle.Logging.Log4Net;
using Abp.Dependency;
using Abp.Modules;
using Abp.PlugIns;
using Abp.Threading;
using Abp.Web;
using Blocks.Framework.FileSystems;
using Blocks.Framework.FileSystems.VirtualPath;
using Blocks.Framework.Logging;
using Blocks.Framework.Web.Web.Localization;
using Castle.Facilities.Logging;
using Castle.Winsdor.Aspnet.Web;

namespace Blocks.Framework.Web
{
    public abstract class BlocksWebApplication<TStartupModule> : HttpApplication
        where TStartupModule : AbpModule
    {
        protected virtual string logConfigName
        {
            get { return "log4net.config"; }
        }

        private Stopwatch requestWatch = new Stopwatch();

        /// <summary>
        /// Gets a reference to the <see cref="P:Abp.Web.AbpWebApplication`1.AbpBootstrapper" /> instance.
        /// </summary>
        private static AbpBootstrapper abpBootstrapper =
            AbpBootstrapper.Create<TStartupModule>((Action<AbpBootstrapperOptions>) null);

        public static AbpBootstrapper AbpBootstrapper
        {
            get { return abpBootstrapper; }
        }

        protected virtual void Application_Start(object sender, EventArgs e)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            PerWebRequestLifestyleModule.FuncHttpCache = (noInput) => { return HttpContext.Current.Items; };

            AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(
                f => f.UseAbpLog4Net().WithConfig(Server.MapPath(logConfigName))
            );

            ThreadCultureSanitizer.Sanitize();

            var environment = WebHostingEnvironment.CreateHostingEnvironment(new WebHostingEnvironment()
            {
                ContentRootPath = Server.MapPath("~")
            });
            IVirtualPathProvider pathProvider = new DefaultVirtualPathProvider(environment);
            if (pathProvider.DirectoryExists(@"~\Modules"))
            {
                foreach (var modulePath in pathProvider.ListDirectories(@"~\Modules"))
                {
                    var moduleFileList = pathProvider.ListDirectories(modulePath);
//                    if (!moduleFileList.Any(t => string.Equals(t, "Module.txt", StringComparison.CurrentCultureIgnoreCase)))
//                        continue;
                    var moduleBin = moduleFileList.FirstOrDefault(t => t.EndsWith("bin"));
                    moduleBin = moduleBin??moduleFileList.FirstOrDefault(t => t.EndsWith("Release"));
                    if (!string.IsNullOrEmpty(moduleBin))
                        AbpBootstrapper.PlugInSources.AddFolder(pathProvider.MapPath(moduleBin),
                            SearchOption.AllDirectories);
                }
            }

            AbpBootstrapper.Initialize();

            stopwatch.Stop();

            LogHelper.Log(new LogModel()
            {
                Message = "Framework Init time:" + stopwatch.ElapsedMilliseconds + "ms", LogSeverity = LogSeverity.Info
            });
        }

        protected virtual void Application_End(object sender, EventArgs e)
        {
            AbpBootstrapper.Dispose();
            //  AbpWebApplication<TStartupModule>.AbpBootstrapper.Dispose();
        }

        protected virtual void Session_Start(object sender, EventArgs e)
        {
        }

        protected virtual void Session_End(object sender, EventArgs e)
        {
        }

        protected virtual void Application_BeginRequest(object sender, EventArgs e)
        {
            requestWatch.Restart();
        }

        protected virtual void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected virtual void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
             this.SetCurrentCulture();
        }

        protected virtual void Application_EndRequest(object sender, EventArgs e)
        {
            PerWebRequestLifestyleModule.EndRequest(sender, e);
            requestWatch.Stop();
            LogHelper.Log(new LogModel()
            {
                Message = $"Framework request url:{HttpContext.Current.Request.Url.AbsolutePath}  time:" + requestWatch.ElapsedMilliseconds + "ms",
                LogSeverity = LogSeverity.Info
            });
        }

        protected virtual void Application_Error(object sender, EventArgs e)
        {
        }

        protected virtual void SetCurrentCulture()
        {
            AbpBootstrapper.IocManager.Using<ICurrentCultureSetter>(
                (Action<ICurrentCultureSetter>) (cultureSetter => cultureSetter.SetCurrentCulture(this.Context)));

            //   AbpWebApplication<TStartupModule>.AbpBootstrapper.IocManager.Using<ICurrentCultureSetter>((Action<ICurrentCultureSetter>) (cultureSetter => cultureSetter.SetCurrentCulture(this.Context)));
        }
    }
}