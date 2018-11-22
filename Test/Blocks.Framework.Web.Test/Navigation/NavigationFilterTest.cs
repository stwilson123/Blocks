using System.Linq;
using System.Reflection;
using Abp.TestBase;
using Blocks.Framework.ApplicationServices.Controller.Factory;
using Blocks.Framework.Ioc;
using Blocks.Framework.Navigation.Manager;
using Blocks.Framework.Utility.Extensions;
using Blocks.Framework.Web.Mvc.Controllers;
using Blocks.Framework.Web.Mvc.Controllers.Factory;
using Blocks.Framework.Web.Mvc.Controllers.Manager;
using Blocks.Framework.Web.Navigation;
using Blocks.Framework.Web.Test.Navigation.TestModels;
using Xunit;

namespace Blocks.Framework.Web.Test.Navigation
{
    public class NavigationFilterTest: AbpIntegratedTestBase<TestNavigationModule>
    {
        public NavigationFilterTest()
        {
          
        }
        [Fact]
        public async  void TestGetMenus()
        {
            var menuItems = LocalIocManager.Resolve<NavigationManager>().MainMenu;
            var menuItem = menuItems.Items.FirstOrDefault();
            Assert.NotNull(menuItem);
            Assert.True(menuItem is WebNavigationItemDefinition);
            Assert.True(menuItem.IsVisible);
            var webMenuItem = menuItem as WebNavigationItemDefinition;
            Assert.True(webMenuItem.HasPermissions.Any(p => p.Name ==  Permissons.Add));
            Assert.True(webMenuItem.HasPermissions.Any(p => p.Name ==  Permissons.Edit));
 
           
        }
    }
}