using System;
using System.Linq;
using System.Reflection;
using Abp.Dependency;
using Abp.Extensions;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.Environment.Extensions.Models;
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
        private IIocManager _iIocManager;

        public DependencyConventionalRegistrar(IIocManager iIocManager)
        {
            _iIocManager = iIocManager;
        }

        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                    .Where(t => typeof(IDependency).IsAssignableFrom(t) &&
                                !typeof(ISingletonDependency).IsAssignableFrom(t) &&
                                !typeof(ITransientDependency).IsAssignableFrom(t) && 
                                !typeof(Abp.Dependency.ISingletonDependency).IsAssignableFrom(t) && 
                                !typeof(Abp.Dependency.ITransientDependency).IsAssignableFrom(t)
                    )
                    .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                    .WithService.Self()
                    .WithService.DefaultInterfaces()
                    .ConfigureIf(t => typeof(IFeature).IsAssignableFrom(t.Implementation), t =>
                        t.DependsOn((kernel, param) =>
                        {
                            param.Add("Feature",
                                new Lazy<FeatureDescriptor>(() =>

                                    _iIocManager.Resolve<IExtensionManager>().AvailableFeatures()
                                        .FirstOrDefault(f => f.Id == context.Assembly.GetName().Name)));
                        }))
                 
                    .LifestyleTransient()
            );
        }
    }
}