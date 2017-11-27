using System;
using Abp;
using Abp.Dependency;
using Abp.Modules;
using Abp.PlugIns;
using Abp.Threading;
using Abp.Web;
using Abp.Web.Localization;
using Blocks.Framework.FileSystems.VirtualPath;
using Blocks.Framework.Web.FileSystems.VirtualPath;

namespace Blocks.Framework.Web
{
    public abstract class BlocksWebApplication<TStartupModule> : AbpWebApplication<TStartupModule> where TStartupModule : AbpModule
    {
        /// <summary>
        /// Gets a reference to the <see cref="P:Abp.Web.AbpWebApplication`1.AbpBootstrapper" /> instance.
        /// </summary>
        public static AbpBootstrapper AbpBootstrapper { get; } = AbpBootstrapper.Create<TStartupModule>((Action<AbpBootstrapperOptions>) null);

        protected virtual void Application_Start(object sender, EventArgs e)
        {
            ThreadCultureSanitizer.Sanitize();

            IVirtualPathProvider pathProvider = new DefaultVirtualPathProvider();
            if (pathProvider.FileExists(@"~\Modules"))
                AbpWebApplication<TStartupModule>.AbpBootstrapper.PlugInSources.AddFolder(@"~\Modules");
            AbpWebApplication<TStartupModule>.AbpBootstrapper.Initialize();
        }

        protected virtual void Application_End(object sender, EventArgs e)
        {
            AbpWebApplication<TStartupModule>.AbpBootstrapper.Dispose();
        }

        protected virtual void Session_Start(object sender, EventArgs e)
        {
        }

        protected virtual void Session_End(object sender, EventArgs e)
        {
        }

        protected virtual void Application_BeginRequest(object sender, EventArgs e)
        {
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
        }

        protected virtual void Application_Error(object sender, EventArgs e)
        {
        }

        protected virtual void SetCurrentCulture()
        {
            AbpWebApplication<TStartupModule>.AbpBootstrapper.IocManager.Using<ICurrentCultureSetter>((Action<ICurrentCultureSetter>) (cultureSetter => cultureSetter.SetCurrentCulture(this.Context)));
        }
    }
}