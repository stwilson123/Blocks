using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Localization;
using Blocks.Framework.Navigation;
using Blocks.Framework.Navigation.Provider;
using INavigationProviderContext = Blocks.Framework.Navigation.Provider.INavigationProviderContext;

namespace Blocks.BussnessWebModule.Module_Start
{
    public class BussnesWebModuleNavigationProvider : INavigationProvider
    {
        public Localizer L { get; set; }
        public ExtensionDescriptor Extension { get; set; }

        public void SetNavigation(INavigationProviderContext context)
        {
           context.Manager.MainMenu
                .AddBuilder((m) => 
                    m.Name("Test").DisplayName(L("Tests"))
                        .Action("TranditionLayoutTestNew", "Tests",Extension.Name)
                    )
                .AddBuilder((m) => m.Name("MasterData").DisplayName(L("MasterData"))
                    .Action("Index", "MasterData",Extension.Name));

             
        }
    }
}
