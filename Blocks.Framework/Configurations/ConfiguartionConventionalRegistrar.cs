using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Abp.Dependency;
using Abp.Logging;
using Blocks.Framework.Environment.Extensions.Models;
using Castle.MicroKernel.Registration;

namespace Blocks.Framework.Configurations
{
    /// <summary>
    ///     Registers all MVC Controllers derived from <see cref="Controller" />.
    /// </summary>
    public class ConfiguartionConventionalRegistrar : IConventionalDependencyRegistrar
    {
        public static string AppConfigKey = "AppStartConfig";
        private readonly IEnumerable<ExtensionDescriptor> _extensionDescriptors;

        public ConfiguartionConventionalRegistrar(IEnumerable<ExtensionDescriptor> extensionDescriptors)
        {
            _extensionDescriptors = extensionDescriptors;
        }

        /// <inheritdoc />
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            var currentAssmeblyName = context.Assembly.GetName().Name;
            var extensionDescriptor = _extensionDescriptors.FirstOrDefault(t => t.Id == currentAssmeblyName);
            if (extensionDescriptor == null)
            {
                LogHelper.Logger.WarnFormat(
                    $"{currentAssmeblyName} can't found extension depond on it.so ignore to register BlocksConfiguration");
                return;
            }
            var configKey = $"{extensionDescriptor.Name}\\{AppConfigKey}";
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                    .BasedOn<IConfiguration>()
                    .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                    .ConfigureIf(t => !context.IocManager.IsRegistered(configKey) ,t => t.Named(configKey))
                    .WithService.Select((t,baseTypes )=> t.GetInterfaces().Where(bType =>  typeof(IConfiguration) != bType && typeof(IConfiguration).IsAssignableFrom(bType) )  )
                    .LifestyleTransient()
            );

        }
    }
}