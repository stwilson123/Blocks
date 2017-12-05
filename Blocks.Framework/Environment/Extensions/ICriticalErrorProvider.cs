using System.Collections.Generic;
using Abp.Dependency;
using Abp.Localization;

namespace Blocks.Framework.Environment.Extensions
{
    public interface ICriticalErrorProvider : ISingletonDependency {
        IEnumerable<string> GetErrors();

        /// <summary>
        /// Called by any to notice the system of a critical issue at the system level, e.g. incorrect extensions
        /// </summary>
        void RegisterErrorMessage(string message);

        /// <summary>
        /// Removes all error message
        /// </summary>
        void Clear();

    }
}