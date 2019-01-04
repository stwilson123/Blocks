using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abp.Dependency;
using Abp.Modules;
using Abp.PlugIns;
using Blocks.Framework.Data.Paging;
using Blocks.Framework.Ioc;
using Blocks.Framework.NullObject;
using Blocks.Framework.Test.Interface;
using Blocks.Framework.Test.Model;
using Blocks.Framework.TestAssembly1;
using Blocks.Framework.TestAssembly2;
using Castle.MicroKernel.Registration;
using Newtonsoft.Json;
using Xunit;

namespace Blocks.Framework.Test.Ioc
{
    public class DependencyConventionalRegistrarTest : BlocksTestBase
    {
        private IIocManager _iocManager;
        public DependencyConventionalRegistrarTest()
        {
            _iocManager = new IocManager();
        }

        [Fact]
        public void MultObjectUseSameInterfaceDefaultFirst()
        {
            IConventionalDependencyRegistrar register = new MultObjectConventionalRegistrar();
 
            register.RegisterAssembly( new Blocks.Framework.Ioc.Dependency.ConventionalRegistrationContext(this.GetType().Assembly, _iocManager, null));

            var actualObject = _iocManager.Resolve<ILog>();
            Assert.True(actualObject is Log4Net );
        }
        
        [Fact]
        public void TestActalObjectReplaceNullObject()
        {
            var iocManager = new IocManager();

            iocManager.Register<IAbpModuleManager, TestAbpMoudleManger>();
            IConventionalDependencyRegistrar register = new DependencyConventionalRegistrar(iocManager);

            register.RegisterAssembly(new Blocks.Framework.Ioc.Dependency.ConventionalRegistrationContext(this.GetType().Assembly, iocManager, null));

            var actualObject = iocManager.Resolve<INullObjectTest>();
            Assert.True(actualObject is NullObjectTestActual );
        }


        [Fact]
        public void TestDependencyIs_Resolve_TheLastestRegisterType()
        {
            var iocManager = new IocManager();

            iocManager.Register<IAbpModuleManager, TestAbpMoudleManger>();
            IConventionalDependencyRegistrar register = new DependencyConventionalRegistrar(iocManager);

            var Assembly1 = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == "Blocks.Framework.TestAssembly1");
            var Assembly2 = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == "Blocks.Framework.TestAssembly2");

            register.RegisterAssembly(new Blocks.Framework.Ioc.Dependency.ConventionalRegistrationContext(Assembly1, iocManager, null));
            register.RegisterAssembly(new Blocks.Framework.Ioc.Dependency.ConventionalRegistrationContext(Assembly2, iocManager, null));

            var actualObject = iocManager.Resolve<ITestDependency>();
            Assert.True(actualObject is TestDependencySecond);



            var iocManager2 = new IocManager();

            iocManager2.Register<IAbpModuleManager, TestAbpMoudleManger>();
            IConventionalDependencyRegistrar register2 = new DependencyConventionalRegistrar(iocManager2);

            register2.RegisterAssembly(new Blocks.Framework.Ioc.Dependency.ConventionalRegistrationContext(Assembly2, iocManager2, null));
            register2.RegisterAssembly(new Blocks.Framework.Ioc.Dependency.ConventionalRegistrationContext(Assembly1, iocManager2, null));

            var actualObject2 = iocManager2.Resolve<ITestDependency>();
            Assert.True(actualObject is TestDependencyFirst);
        }
    }

    public interface INullObjectTest : Blocks.Framework.Ioc.Dependency.ITransientDependency
    {
        
    }
    public class  NullObjectTest  : INullObjectTest,INullObject
    {
        
    }
    
    public class  NullObjectTestActual  : INullObjectTest
    {
        
    }
    /// <summary>
    /// Registers all MVC Controllers derived from <see cref="Controller"/>.
    /// </summary>
    class MultObjectConventionalRegistrar : IConventionalDependencyRegistrar
    {
        private IIocManager _iIocManager;

        
        /// <inheritdoc/>
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            context.IocManager.IocContainer.Register(Component.For<ILog>().ImplementedBy<Log4Net>());
            context.IocManager.IocContainer.Register(Component.For<ILog>().ImplementedBy<NullLog>());

        }
    }

    class TestAbpMoudleManger : IAbpModuleManager
    {
        public AbpModuleInfo StartupModule => null;

        public IReadOnlyList<AbpModuleInfo> Modules => new List<AbpModuleInfo>();

        public void Initialize(Type startupModule)
        {
        }

        public void ShutdownModules()
        {
        }

        public void StartModules()
        {
        }
    }
}