using Blocks.Framework.Ioc.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blocks.Framework.Localization.Provider
{
    public interface ILocalizationProvider : ITransientDependency
    {
        Task<IDictionary<string,string>> getLocalizationDicionary(string moduleName, string culture);
    }
}
