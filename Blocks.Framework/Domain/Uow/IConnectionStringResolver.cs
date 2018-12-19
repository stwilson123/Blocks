using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.Domain.Uow
{
    public interface IConnectionStringResolver
    {
        /// <summary>
        /// Gets a connection string name (in config file) or a valid connection string.
        /// </summary>
        /// <param name="args">Arguments that can be used while resolving connection string.</param>
        string GetNameOrConnectionString(ConnectionStringResolveArgs args);
    }
}
