using INavigationManager = Blocks.Framework.Navigation.Manager.INavigationManager;

namespace Blocks.Framework.Navigation.Provider
{
    public interface INavigationProviderContext  
    {
        /// <summary>
        /// Gets a reference to the navigation manager.
        /// </summary>
        INavigationManager Manager { get; }
    }
 
}