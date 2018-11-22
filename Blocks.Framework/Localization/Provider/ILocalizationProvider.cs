using Blocks.Framework.Ioc.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.Localization.Provider
{
    public interface ILocalizationProvider : ITransientDependency
    {
        IDictionary<string,string> getLocalizationDicionary(string moduleName, string culture);
    }
}
