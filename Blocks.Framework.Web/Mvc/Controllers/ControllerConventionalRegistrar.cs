using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Abp.Dependency;
using Blocks.Framework.Environment.Exception;
using Blocks.Framework.Environment.Extensions.Models;
using Castle.MicroKernel.Registration;

namespace Blocks.Framework.Web.Mvc.Controllers
{
    /// <summary>
    /// Registers all MVC Controllers derived from <see cref="Controller"/>.
    /// </summary>
    public class ControllerConventionalRegistrar : IConventionalDependencyRegistrar
    {
        public static string GetControllerSerivceName(string area,string controllerName)
        {
            return $@"WebController\{area}\{controllerName}";
        }

        private readonly IEnumerable<ExtensionDescriptor> _extensionDescriptors;

        public ControllerConventionalRegistrar(IEnumerable<ExtensionDescriptor> extensionDescriptors)
        {
            _extensionDescriptors = extensionDescriptors;
        }

        /// <inheritdoc/>
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            var currentAssmeblyName = context.Assembly.GetName().Name;
            var extensionDescriptor = _extensionDescriptors.FirstOrDefault(t => t.Id == currentAssmeblyName);
            if(extensionDescriptor == null)
                throw  new ExtensionNotFoundException($"{currentAssmeblyName} can't found extension depond on it");

            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                    .BasedOn<BlockWebController>()
                    .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                    .Configure(t => t.Named(GetControllerSerivceName(extensionDescriptor.Name ,t.Implementation.Name)))
                    .LifestyleTransient()
                );
        }
    }
}