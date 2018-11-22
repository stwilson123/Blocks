using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Localization;
using Blocks.Framework.Navigation;
using Blocks.Framework.Navigation.Provider;

namespace Blocks.Framework.Web.Test.Navigation.TestModels
{
    public class TestNavigationProvider: INavigationProvider
    {
        public Localizer L { get; set; }
        public ExtensionDescriptor Extension { get; set; }

        public void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddBuilder((builder) =>
                    builder.Name("TestMvc").DisplayName(new LocalizableString(TestModule.ModuleName,"TestMvc"))
                        .Action("Default", "TestNavigation", TestModule.ModuleName).HasPermissions(Permissons.Add, Permissons.Edit));

        }
    }
}