using System.Collections.Generic;
using Abp.Dependency;
using Abp.TestBase;
using Blocks.Framework.Navigation.Manager;
using Blocks.Framework.Navigation.Provider;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Xunit;
using Blocks.Framework.Ioc;
namespace Blocks.Framework.Test.Navigation
{
    public class NavigationProivderTest : AbpIntegratedTestBase<TestNavigationModule>
    { 
        public NavigationProivderTest()
        {
         
        }
        
        [Fact]
        public void Test()
        { 
            var a = LocalIocManager.Resolve<INavigationManager>().MainMenu;
            
            List<string> list = new List<string>();
       
        }
    }
}