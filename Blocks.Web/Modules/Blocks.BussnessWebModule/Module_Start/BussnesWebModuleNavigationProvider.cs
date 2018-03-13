using Abp.Application.Navigation;
using Abp.Localization;
using Blocks.Framework.Web.Application;

namespace Blocks.BussnessWebModule.Module_Start
{
    public class BussnesWebModuleNavigationProvider : BlocksNavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu

                .AddItem(
                    new MenuItemDefinition(
                        "Test",
                        L("Tests"),

                        icon: "people",
                        requiredPermissionName: "Pages.Users"
                    ).AddItem(new MenuItemDefinition("Test", L("Test"), icon: "people",
                    requiredPermissionName: "Pages.Users", url: "BussnessWebModule/Tests/TranditionLayoutTestNew"))
                    .AddItem(new MenuItemDefinition("MasterData", L("MasterData"), icon: "people",
                    requiredPermissionName: "Pages.Users", url: "BussnessWebModule/MasterData/Index")
                    )
                );

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, "Blocks");
        }
    }
}
