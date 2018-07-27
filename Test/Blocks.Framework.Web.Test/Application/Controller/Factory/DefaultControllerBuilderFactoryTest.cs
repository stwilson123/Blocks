using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abp.Dependency;
using Abp.TestBase;
using Blocks.Framework.ApplicationServices;
using Blocks.Framework.ApplicationServices.Controller;
using Blocks.Framework.ApplicationServices.Controller.Factory;
using Blocks.Framework.ApplicationServices.Manager;
using Blocks.Framework.Ioc;
using Blocks.Framework.Web.Test.Application.Controller.TestModel;
using Blocks.Framework.Web.Web;
using Blocks.Framework.Web.Web.HttpMethod;
using Xunit;
using Blocks.Framework.Utility.Extensions;
namespace Blocks.Framework.Web.Test.Application.Controller.Factory
{
    public class DefaultControllerBuilderFactoryTest : AbpIntegratedTestBase<TestModule>
    {
        private IEnumerable<Type> servicesType;
        public DefaultControllerBuilderFactoryTest()
        {
            LocalIocManager.Register<DefaultControllerManager>();
            this.servicesType = LocalIocManager.IocContainer.Kernel.GetHandlers().SelectMany(t => t.Services).Where(t => t.Assembly == Assembly.GetExecutingAssembly());
        }
        
        [Fact]
        public void GetAppServiceForOneWithDefault()
        {
            var serviceName = TestModule.ModuleName + "/Test";
            IDefaultControllerBuilderFactory factory = new DefaultControllerBuilderFactory(LocalIocManager);
            factory.For<ITestAppService>(serviceName).WithApiExplorer(true).Build();

            TestAppService(serviceName);
        }

        private void TestAppService(string serviceName)
        {
            var defaultControllermanager = LocalIocManager.Resolve<DefaultControllerManager>();
            var testController = defaultControllermanager.FindOrNull(serviceName);
            Assert.NotNull(testController);
            Assert.Equal(testController.ServiceName, serviceName);
         //   Assert.True(testController.IsApiExplorerEnabled);
            Assert.True(testController.ApiControllerType == typeof(NopController));

            var controllerActionDefault = testController.Actions.FirstOrDefault(t => t.Key == "Default");
            Assert.NotNull(controllerActionDefault.Value);
            Assert.Equal("Default", controllerActionDefault.Value.ActionName);
       //     Assert.Equal(HttpVerb.Post, controllerActionDefault.Value.Verb);

            var controllerActionGet = testController.Actions.FirstOrDefault(t => t.Key == "TestGet");
            Assert.NotNull(controllerActionGet.Value);
            Assert.Equal("TestGet", controllerActionGet.Value.ActionName);
        //    Assert.Equal(HttpVerb.Get, controllerActionGet.Value.Verb);


            //Attribute in implement type is unavailable
            var controllerActionDelete = testController.Actions.FirstOrDefault(t => t.Key == "TestDelete");
            Assert.NotNull(controllerActionDelete.Value);
            Assert.Equal("TestDelete", controllerActionDelete.Value.ActionName);
       //     Assert.Equal(HttpVerb.Post, controllerActionDelete.Value.Verb);

            var controllerActionIgnore = testController.Actions.FirstOrDefault(t => t.Key == "TestIgnore");
            Assert.Null(controllerActionIgnore.Value);

            var controllerActionNoActionName = testController.Actions.FirstOrDefault(t => t.Key == "TestNoActionName");
            Assert.Null(controllerActionNoActionName.Value);
        }

        [Fact]
        public void GetAllAppServiceForAll()
        {
            var servicePrefix = TestModule.ModuleName.ToCamelCase();
            var serviceName = servicePrefix + "/"+ typeof(TestAppService).Name.ToCamelCase().RemovePostFix(AppService.CommonPostfixes);
            IDefaultControllerBuilderFactory factory = new DefaultControllerBuilderFactory(LocalIocManager);
            factory.ForAll<ITestAppService>(servicePrefix,servicesType).Build();

            TestAppService(serviceName);
        }
        
    }
}