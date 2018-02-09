﻿using Abp.Application.Navigation;
using Abp.Localization;

namespace Blocks.BussnessWebModule.Module_Start
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See Views/Layout/_TopMenu.cshtml file to know how to render menu.
    /// </summary>
    public class BlocksNavigationProvider : NavigationProvider
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
                    requiredPermissionName: "Pages.Users", url: "BussnessWebModule/Tests/TranditionLayoutTest"))
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
