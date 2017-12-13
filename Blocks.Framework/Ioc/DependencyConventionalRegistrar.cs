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
                    .BasedOn<ISingletonDependency>()
                    .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                    .ConfigureSpecial(_iIocManager,context.Assembly.GetName().Name)
                    .WithService.Self()
                    .WithService.DefaultInterfaces()
                    .LifestyleSingleton()
            );
            
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                    .BasedOn<ITransientDependency>()
                    .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                    .ConfigureSpecial(_iIocManager,context.Assembly.GetName().Name)
                    .WithService.Self()
                    .WithService.DefaultInterfaces()
                    .LifestyleTransient()
            );
            
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                    .BasedOn<IUnitOfWorkDependency>()
                    .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                    .ConfigureSpecial(_iIocManager,context.Assembly.GetName().Name)
                    .WithService.Self()
                    .WithService.DefaultInterfaces()
                    .LifestylePerWebRequest()
            );
            
            
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                    .Where(t => typeof(IDependency).IsAssignableFrom(t.GetTypeInfo()) &&
                                !typeof(ISingletonDependency).IsAssignableFrom(t) &&
                                !typeof(ITransientDependency).IsAssignableFrom(t) && 
                                !typeof(IUnitOfWorkDependency).IsAssignableFrom(t) &&
                                !typeof(Abp.Dependency.ISingletonDependency).IsAssignableFrom(t) && 
                                !typeof(Abp.Dependency.ITransientDependency).IsAssignableFrom(t)
                    )
                    .ConfigureSpecial(_iIocManager,context.Assembly.GetName().Name)
                    .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                    .WithService.Self()
                    .WithService.DefaultInterfaces()
                    .LifestyleTransient()
            );
        }

       
    }

    static class  RegisterEx
    {
        public static  BasedOnDescriptor ConfigureSpecial(this BasedOnDescriptor baseOnDescriptor,IIocManager _iIocManager,string ModuleID)
        {
            return baseOnDescriptor.ConfigureIf(t => typeof(IFeature).IsAssignableFrom(t.Implementation), t =>
            {
                t.DependsOn(Castle.MicroKernel.Registration.Dependency.OnValue("Feature", new Lazy<FeatureDescriptor>(
                    () =>
                    {
                        return _iIocManager.Resolve<IExtensionManager>().AvailableFeatures()
                            .FirstOrDefault(f => f.Id == ModuleID);
                    })));

            });
        }
    }
}