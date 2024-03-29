﻿using Abp.Dependency;
using Blocks.Framework.Ioc.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Blocks.Framework.Domain.Uow
{
    public class UnitOfWorkManager : IUnitOfWorkManager, Ioc.Dependency.ITransientDependency
    {
        private readonly IIocResolver _iocResolver;
        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;
        private readonly IUnitOfWorkDefaultOptions _defaultOptions;

        public IUnitOfWork Current
        {
            get { return _currentUnitOfWorkProvider.Current; }
        }

        public UnitOfWorkManager(
            IIocResolver iocResolver,
            ICurrentUnitOfWorkProvider currentUnitOfWorkProvider,
            IUnitOfWorkDefaultOptions defaultOptions)
        {
            _iocResolver = iocResolver;
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
            _defaultOptions = defaultOptions;
        }

        public IUnitOfWorkCompleteHandle Begin()
        {
            return Begin(new UnitOfWorkOptions());
        }

        public IUnitOfWorkCompleteHandle Begin(TransactionScopeOption scope)
        {
            return Begin(new UnitOfWorkOptions { Scope = scope });
        }

        public IUnitOfWorkCompleteHandle Begin(UnitOfWorkOptions options)
        {
            options.FillDefaultsForNonProvidedOptions(_defaultOptions);

            var outerUow = _currentUnitOfWorkProvider.Current;

            if (options.Scope == TransactionScopeOption.Required && outerUow != null)
            {
                return new InnerUnitOfWorkCompleteHandle();
            }

            var uow = _iocResolver.Resolve<IUnitOfWork>();

            uow.Completed += (sender, args) =>
            {
                _currentUnitOfWorkProvider.Current = null;
            };

            uow.Failed += (sender, args) =>
            {
                _currentUnitOfWorkProvider.Current = null;
            };

            uow.Disposed += (sender, args) =>
            {
                _iocResolver.Release(uow);
            };

            //Inherit filters from outer UOW
            if (outerUow != null)
            {
               // options.FillOuterUowFiltersForNonProvidedOptions(outerUow.Filters.ToList());
            }

            uow.Begin(options);

            //Inherit tenant from outer UOW
            //if (outerUow != null)
            //{
            //    uow.SetTenantId(outerUow.GetTenantId(), false);
            //}

            _currentUnitOfWorkProvider.Current = uow;

            return uow;
        }
    }
}
