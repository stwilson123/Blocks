using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Abp.Dependency;
using Blocks.Framework.Environment.Exception;
using Blocks.Framework.Environment.Extensions.Models;
using Castle.MicroKernel.Registration;

namespace Blocks.Framework.Web.Api.Controllers
{
    /// <summary>
    /// Registers all Web API Controllers derived from <see cref="ApiController"/>.
    /// </summary>
    public class ApiControllerConventionalRegistrar : IConventionalDependencyRegistrar
    {
        public static string GetControllerSerivceName(string area,string controllerName)
        {
            return $@"WebApiController\{area}\{controllerName}";
        }
        private readonly IEnumerable<ExtensionDescriptor> _extensionDescriptors;

        public ApiControllerConventionalRegistrar(IEnumerable<ExtensionDescriptor> extensionDescriptors)
        {
            _extensionDescriptors = extensionDescriptors;
        }


        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            var currentAssmeblyName = context.Assembly.GetName().Name;
            var extensionDescriptor = _extensionDescriptors.FirstOrDefault(t => t.Id == currentAssmeblyName);
            if(extensionDescriptor == null)
                throw  new ExtensionNotFoundException($"{currentAssmeblyName} can't found extension depond on it");
            
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                    .BasedOn<BlockWebApiController>()
                    .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                    .Configure(t => t.Named(GetControllerSerivceName(extensionDescriptor.Name ,t.Implementation.Name)))
                    .LifestyleTransient()
                );
        }
    }
}
