using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Localization;
using Blocks.Framework.Navigation;
using Blocks.Framework.Navigation.Builder;
using Blocks.Framework.Navigation.Provider;
using Microsoft.AspNetCore.Routing;

namespace Blocks.Framework.Test.Navigation.Model
{
    public class TestNavigationProvider : INavigationProvider
    {
        public Localizer L { get; set; }
        public ExtensionDescriptor Extension { get; set; }

        public void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu.AddItem(new NavigationItemDefinition("Test", new LocalizableString("", "")));
            context.Manager.MainMenu.AddBuilder((builder) =>
                builder.Name("Test1").DisplayName(new LocalizableString("","")).Action("abc","controller","TestNavigationModule")
            );
            context.Manager.MainMenu.AddBuilder((builder) =>
                builder.Name("Test2").DisplayName(new LocalizableString("","")).Action("abc","controller", "TestNavigationModule")
                );
            
        }
    }
}