using System.Collections.Generic;
using System.Reflection;
using Abp.Modules;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Navigation;
using Moq;

namespace Blocks.Framework.Test.Navigation
{
    [DependsOn(typeof(TestModule))]
    [DependsOn(typeof(NavigationModule))]
    public class TestNavigationModule : AbpModule
    {
        public string Name = "TestFrameworkModule";
        public override void PreInitialize()
        {
            var mo = new Mock<IExtensionManager>();
            mo.Setup(m => m.AvailableExtensions()).Returns(new List<ExtensionDescriptor>()
            {
                new ExtensionDescriptor()
                {
                    Id = this.GetType().Assembly.FullName,
                     Name = Name
                }
                
            });
           IocManager.Register<IExtensionManager>(mo.Object);
        }
    }
}