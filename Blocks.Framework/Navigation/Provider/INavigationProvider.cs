using System;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Ioc.Dependency;
using Blocks.Framework.Localization;

namespace Blocks.Framework.Navigation.Provider
{
    
    /// <summary>
    /// This interface should be implemented by classes which change
    /// navigation of the application.
    /// </summary>
    public interface INavigationProvider : ITransientDependency
    {
         Localizer L { get;  set; }

        ExtensionDescriptor Extension { get; set; }

        /// <summary>
        /// Used to set navigation.
        /// </summary>
        /// <param name="context">Navigation context</param>
         void SetNavigation(INavigationProviderContext context);
    }
}