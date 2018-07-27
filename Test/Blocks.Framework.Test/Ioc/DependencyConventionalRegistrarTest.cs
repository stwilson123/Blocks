using System.Reflection;
using Abp.Dependency;
using Blocks.Framework.Data.Paging;
using Blocks.Framework.Ioc;
using Blocks.Framework.NullObject;
using Blocks.Framework.Test.Interface;
using Blocks.Framework.Test.Model;
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
            IConventionalDependencyRegistrar register = new DependencyConventionalRegistrar(iocManager);
 
            register.RegisterAssembly( new Blocks.Framework.Ioc.Dependency.ConventionalRegistrationContext(this.GetType().Assembly, iocManager, null));

            var actualObject = iocManager.Resolve<INullObjectTest>();
            Assert.True(actualObject is NullObjectTestActual );
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
    
    
}