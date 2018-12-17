using System.Collections.Generic;
using System.Net;
using System.Web;
using Abp.Collections.Extensions;
using Abp.Modules;
using Abp.Web;
using Blocks.Framework.Web.Web.Configuration;
using Blocks.Framework.Web.Web.Security.AntiForgery;

namespace Blocks.Framework.Web.Web
{
    /// <summary>
    /// This module is used to use ABP in ASP.NET web applications.
    /// </summary>
    [DependsOn(typeof(AbpWebCommonModule))]    
    public class AbpWebModule : AbpModule
    {
        /// <inheritdoc/>
        public override void PreInitialize()
        {
            IocManager.Register<IAbpAntiForgeryWebConfiguration, AbpAntiForgeryWebConfiguration>();
            IocManager.Register<IAbpWebLocalizationConfiguration, AbpWebLocalizationConfiguration>();
            //            IocManager.Register<IAbpWebModuleConfiguration, AbpWebModuleConfiguration>();
            //            
            //            Configuration.ReplaceService<IPrincipalAccessor, HttpContextPrincipalAccessor>(DependencyLifeStyle.Transient);
            //            Configuration.ReplaceService<IClientInfoProvider, WebClientInfoProvider>(DependencyLifeStyle.Transient);
            //
            //            Configuration.MultiTenancy.Resolvers.Add<DomainTenantResolveContributor>();
            //            Configuration.MultiTenancy.Resolvers.Add<HttpHeaderTenantResolveContributor>();
            //            Configuration.MultiTenancy.Resolvers.Add<HttpCookieTenantResolveContributor>();

            IocManager.Register<HttpContextModel, HttpContextModel>((kernel, componentModel, creationContext) => {
                var ru = HttpContext.Current.Request;
                var cookies = new System.Net.CookieContainer() { };

                for (int i = 0; i < ru.Cookies.Count; i++)
                {
                    var cook = ru.Cookies[i];
                    cookies.Add(new System.Net.Cookie(cook.Name,cook.Value,cook.Path, ru.ServerVariables["Server_Name"]));
                }

                var heads = new WebHeaderCollection();
                heads.Add("X-XSRF-TOKEN", ru.Headers.Get("X-XSRF-TOKEN"));
                //for (int i = 0; i < ru.Cookies.Count; i++)
                //{
                //    var head = ru.Headers[i];
                //    heads.Add(head, ru.Headers.Get(head));
                //}

                return new HttpContextModel() { RequestUrl = ru.Url , CookieCollection = cookies, webHeaderCollection = heads };
            },Abp.Dependency.DependencyLifeStyle.Transient);
            AddIgnoredTypes();
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
//            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());            
        }

        private void AddIgnoredTypes()
        {
            var ignoredTypes = new[]
            {
                typeof(HttpPostedFileBase),
                typeof(IEnumerable<HttpPostedFileBase>),
                typeof(HttpPostedFileWrapper),
                typeof(IEnumerable<HttpPostedFileWrapper>)
            };
            
            foreach (var ignoredType in ignoredTypes)
            {
                Configuration.Auditing.IgnoredTypes.AddIfNotContains(ignoredType);
                Configuration.Validation.IgnoredTypes.AddIfNotContains(ignoredType);
            }
        }
    }
}
