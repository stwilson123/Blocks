using Blocks.Framework.Ioc.Dependency;

namespace Blocks.Framework.Web.Application.Navigation
{
    
    /// <summary>
    /// This interface should be implemented by classes which change
    /// navigation of the application.
    /// </summary>
    public abstract class BlocksNavigationProvider : ITransientDependency
    {
        /// <summary>
        /// Used to set navigation.
        /// </summary>
        /// <param name="context">Navigation context</param>
        public abstract void SetNavigation(INavigationProviderContext context);
    }
}