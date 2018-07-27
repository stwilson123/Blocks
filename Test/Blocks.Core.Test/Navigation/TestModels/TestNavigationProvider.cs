using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Localization;
using Blocks.Framework.Navigation;
using Blocks.Framework.Navigation.Provider;

namespace Blocks.Core.Test.Navigation.TestModels
{
    public class TestNavigationProvider: INavigationProvider
    {
        public Localizer L { get; set; }
        public ExtensionDescriptor Extension { get; set; }

        public void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddBuilder((builder) =>
                    builder.Name("TestMvc").DisplayName(new LocalizableString("TestMvc", TestModule.ModuleName))
                        .Action("Default", "TestMvc", TestModule.ModuleName).HasPermissions("View", "Add"));

        }
    }
}