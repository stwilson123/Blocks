using System;
using System.Web;
using Abp.Dependency;
using Abp.Modules;
using Abp.Resources.Embedded;
using Abp.Threading;
using Blocks.Owin;
using Blocks.Web.Configuration;
using Microsoft.Owin.StaticFiles;
using Owin;

namespace Abp.Owin
{
    /// <summary>
    /// OWIN extension methods for ABP.
    /// </summary>
    public static class BlocksOwinExtensions
    {
        /// <summary>
        /// This should be called as the first line for OWIN based applications for ABP framework.
        /// </summary>
        /// <param name="app">The application.</param>
        public static void UseBlocks(this IAppBuilder app)
        {
            app.UseBlocks(null);
        }

        public static void UseBlocks(this IAppBuilder app,   Action<BlocksOwinOptions> optionsAction)
        {
            ThreadCultureSanitizer.Sanitize();

            var options = new BlocksOwinOptions
            {
                UseEmbeddedFiles = HttpContext.Current?.Server != null
            };

            optionsAction?.Invoke(options);

            if (options.UseEmbeddedFiles)
            {
                if (HttpContext.Current?.Server == null)
                {
                    throw new AbpInitializationException("Can not enable UseEmbeddedFiles for OWIN since HttpContext.Current is null! If you are using ASP.NET Core, serve embedded resources through ASP.NET Core middleware instead of OWIN. See http://www.aspnetboilerplate.com/Pages/Documents/Embedded-Resource-Files#aspnet-core-configuration");
                }

                app.UseStaticFiles(new StaticFileOptions
                {
                    FileSystem = new Blocks.Owin.EmbeddedResources.BlocksOwinEmbeddedResourceFileSystem(
                        IocManager.Instance.Resolve<IEmbeddedResourceManager>(),
                        IocManager.Instance.Resolve<IWebEmbeddedResourcesConfiguration>(),
                        HttpContext.Current.Server.MapPath("~/")
                    )
                });
            }
        }

        /// <summary>
        /// Use this extension method if you don't initialize ABP in other way.
        /// Otherwise, use <see cref="UseBlocks(IAppBuilder)"/>.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <typeparam name="TStartupModule">The type of the startup module.</typeparam>
        public static void UseBlocks<TStartupModule>(this IAppBuilder app)
            where TStartupModule : AbpModule
        {
            app.UseBlocks<TStartupModule>(null, null);
        }

        /// <summary>
        /// Use this extension method if you don't initialize ABP in other way.
        /// Otherwise, use <see cref="UseBlocks(IAppBuilder)"/>.
        /// </summary>
        /// <typeparam name="TStartupModule">The type of the startup module.</typeparam>
        public static void UseBlocks<TStartupModule>(this IAppBuilder app,  Action<AbpBootstrapper> configureAction,  Action<BlocksOwinOptions> optionsAction = null)
            where TStartupModule : AbpModule
        {
            app.UseBlocks(optionsAction);

            if (!app.Properties.ContainsKey("_AbpBootstrapper.Instance"))
            {
                var abpBootstrapper = AbpBootstrapper.Create<TStartupModule>();
                app.Properties["_AbpBootstrapper.Instance"] = abpBootstrapper;
                configureAction?.Invoke(abpBootstrapper);
                abpBootstrapper.Initialize();
            }
        }
    }
}