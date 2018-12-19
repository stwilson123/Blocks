using Blocks.Framework.ApplicationServices;
using Blocks.Framework.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Blocks.Framework.Domain.Uow
{
    internal class UnitOfWorkDefaultOptions : IUnitOfWorkDefaultOptions
    {
        public TransactionScopeOption Scope { get; set; }

        /// <inheritdoc/>
        public bool IsTransactional { get; set; }

        /// <inheritdoc/>
        public TimeSpan? Timeout { get; set; }

        /// <inheritdoc/>
        public bool IsTransactionScopeAvailable { get; set; }

        /// <inheritdoc/>
        public IsolationLevel? IsolationLevel { get; set; }

        //public IReadOnlyList<DataFilterConfiguration> Filters => _filters;
        //private readonly List<DataFilterConfiguration> _filters;

        public List<Func<Type, bool>> ConventionalUowSelectors { get; }

        public UnitOfWorkDefaultOptions()
        {
            //_filters = new List<DataFilterConfiguration>();
            IsTransactional = true;
            Scope = TransactionScopeOption.Required;

            IsTransactionScopeAvailable = true;

            ConventionalUowSelectors = new List<Func<Type, bool>>
            {
                type => typeof(IRepository).IsAssignableFrom(type) ||
                        typeof(IAppService).IsAssignableFrom(type)
            };

            IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
        }

        //public void RegisterFilter(string filterName, bool isEnabledByDefault)
        //{
        //    if (_filters.Any(f => f.FilterName == filterName))
        //    {
        //        throw new AbpException("There is already a filter with name: " + filterName);
        //    }

        //    _filters.Add(new DataFilterConfiguration(filterName, isEnabledByDefault));
        //}

        //public void OverrideFilter(string filterName, bool isEnabledByDefault)
        //{
        //    _filters.RemoveAll(f => f.FilterName == filterName);
        //    _filters.Add(new DataFilterConfiguration(filterName, isEnabledByDefault));
        //}
    }
}
