using System.Linq;
using System.Reflection;
using Abp.Dependency;
using Abp.Extensions;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.Ioc.Dependency;
using Castle.MicroKernel.Registration;
using ISingletonDependency = Blocks.Framework.Ioc.Dependency.ISingletonDependency;
using ITransientDependency = Blocks.Framework.Ioc.Dependency.ITransientDependency;

namespace Blocks.Framework.Ioc
{
    /// <summary>
    /// Registers all Web API Controllers derived from <see cref="IDependency"/>.
    /// </summary>
    public class DependencyConventionalRegistrar : IConventionalDependencyRegistrar
    {
        private IExtensionManager _extensionManager;

        public DependencyConventionalRegistrar(IExtensionManager extensionManager)
        {
            _extensionManager = extensionManager;
        }

        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                    .Where(t => t.IsAssignableFrom(typeof(IDependency)) && !t.IsAssignableFrom(typeof(ISingletonDependency)) && 
                                !t.IsAssignableFrom(typeof(ITransientDependency))  
                                )
                    .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                    .ConfigureIf(t => t.Implementation.IsAssignableFrom(typeof(IDependency)) ,t => t.DependsOn((kernel, param) =>
                    {
                        param.Add("Feature",
                            _extensionManager.AvailableFeatures()
                                .FirstOrDefault(f => f.Id == context.Assembly.GetName().Name));
                    }))
                    .LifestyleTransient()
            );
        }
    }
}