using Blocks.Framework.Navigation.Manager;

namespace Blocks.Framework.Navigation.Provider
{
    internal class NavigationProviderContext : INavigationProviderContext
    {
        public INavigationManager Manager { get; internal set; }
        
        public NavigationProviderContext(INavigationManager manager)
        {
            Manager = manager;
        }
    }
}