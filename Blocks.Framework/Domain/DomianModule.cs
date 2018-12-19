using Abp.Dependency;
using Blocks.Framework.Domain.Uow;
using Blocks.Framework.Ioc;
using Castle.Core;
using Castle.MicroKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Blocks.Framework.Domain
{
    public class DomianModule : BlocksModule
    {

        public override void PreInitialize()
        {
            IocManager.Register<IUnitOfWorkDefaultOptions, UnitOfWorkDefaultOptions>();
        }

        public override void OnRegistered(IKernel kernel, ComponentModel model)
        {
            var implementationType = model.Implementation.GetTypeInfo();
          
            HandleTypesWithUnitOfWorkAttribute(implementationType, model);
            HandleConventionalUnitOfWorkTypes(IocManager, implementationType, model);
        }


        private static void HandleTypesWithUnitOfWorkAttribute(TypeInfo implementationType, ComponentModel model)
        {
            if (IsUnitOfWorkType(implementationType) || AnyMethodHasUnitOfWork(implementationType))
            {
                model.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
            }
        }

        private static void HandleConventionalUnitOfWorkTypes(IIocManager iocManager, TypeInfo implementationType, ComponentModel model)
        {
            if (!iocManager.IsRegistered<IUnitOfWorkDefaultOptions>())
            {
                return;
            }

            var uowOptions = iocManager.Resolve<IUnitOfWorkDefaultOptions>();

            if (uowOptions.IsConventionalUowClass(implementationType.AsType()))
            {
                model.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
            }
        }

        private static bool IsUnitOfWorkType(TypeInfo implementationType)
        {
            return UnitOfWorkHelper.HasUnitOfWorkAttribute(implementationType);
        }

        private static bool AnyMethodHasUnitOfWork(TypeInfo implementationType)
        {
            return implementationType
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Any(UnitOfWorkHelper.HasUnitOfWorkAttribute);
        }

    }
}
