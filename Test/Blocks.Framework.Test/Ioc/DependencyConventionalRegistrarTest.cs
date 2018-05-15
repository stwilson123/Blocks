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
    
        [Fact]
        public void MultObjectUseSameInterfaceDefaultFirst()
        {
            IConventionalDependencyRegistrar register = new MultObjectConventionalRegistrar(this.LocalIocManager);
 
            register.RegisterAssembly( new Blocks.Framework.Ioc.Dependency.ConventionalRegistrationContext(this.GetType().Assembly, LocalIocManager, null));

            var actualObject = this.LocalIocManager.Resolve<ILog>();
            Assert.True(actualObject is Log4Net );
        }
        
        [Fact]
        public void TestActalObjectReplaceNullObject()
        {
            IConventionalDependencyRegistrar register = new DependencyConventionalRegistrar(this.LocalIocManager);
 
            register.RegisterAssembly( new Blocks.Framework.Ioc.Dependency.ConventionalRegistrationContext(this.GetType().Assembly, LocalIocManager, null));

            var actualObject = this.LocalIocManager.Resolve<INullObjectTest>();
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

        public MultObjectConventionalRegistrar(IIocManager iIocManager)
        {
            _iIocManager = iIocManager;
        }
        /// <inheritdoc/>
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            context.IocManager.IocContainer.Register(Component.For<ILog>().ImplementedBy<Log4Net>());
            context.IocManager.IocContainer.Register(Component.For<ILog>().ImplementedBy<NullLog>());

        }
    }
    
    
}