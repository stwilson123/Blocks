using Blocks.Framework.Web;
using Castle.Facilities.Logging;
using Abp.Castle.Logging.Log4Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Abp;
using Abp.Dependency;
using Abp.PlugIns;
using Abp.Threading;
using Abp.Web;
using Abp.Web.Localization;
using Blocks.Framework.FileSystems.VirtualPath;
using Blocks.Framework.Web.FileSystems.VirtualPath;

namespace Blocks.Web
{
    public class MvcApplication : HttpApplication
    {

        /// <summary>
        /// Gets a reference to the <see cref="P:Abp.Web.AbpWebApplication`1.AbpBootstrapper" /> instance.
        /// </summary>
        public static AbpBootstrapper AbpBootstrapper { get; } = AbpBootstrapper.Create<BlocksWebModule>((Action<AbpBootstrapperOptions>) null);

        protected  void Application_Start(object sender, EventArgs e)
        {
     
            ThreadCultureSanitizer.Sanitize();
            
            IVirtualPathProvider pathProvider = new DefaultVirtualPathProvider();
            if (pathProvider.DirectoryExists(@"~\Modules"))
                AbpBootstrapper.PlugInSources.AddFolder(pathProvider.MapPath(@"~\Modules"),
                    SearchOption.AllDirectories);
            AbpBootstrapper.Initialize();
        }

        protected  void Application_End(object sender, EventArgs e)
        {
            AbpBootstrapper.Dispose();
        }

        protected  void Session_Start(object sender, EventArgs e)
        {
        }

        protected  void Session_End(object sender, EventArgs e)
        {
        }

        protected  void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected  void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected  void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            this.SetCurrentCulture();
        }

        protected  void Application_EndRequest(object sender, EventArgs e)
        {
        }

        protected  void Application_Error(object sender, EventArgs e)
        {
        }

        protected  void SetCurrentCulture()
        {
            AbpBootstrapper.IocManager.Using<ICurrentCultureSetter>((Action<ICurrentCultureSetter>) (cultureSetter => cultureSetter.SetCurrentCulture(this.Context)));
        }
    }
}