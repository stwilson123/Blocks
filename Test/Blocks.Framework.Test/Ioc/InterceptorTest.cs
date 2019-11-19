using Abp.Dependency;
using Castle.Core;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Xunit;

namespace Blocks.Framework.Test.Ioc
{
    public class InterceptorTest
    {
        private IIocManager _iocManager;
        public InterceptorTest()
        {
            _iocManager = new IocManager();
        }
        [Fact]
        public void TestDependencyIs_Resolve_TheLastestRegisterType()
        {
//            _iocManager.Register(typeof(EventTest));
//            _iocManager.Register(typeof(TestInterceptor));
            
            WindsorContainer  container = new WindsorContainer();
            container.Register(Component.For<TestInterceptor>());
            container.Register(Component.For<EventTest,IEventTest>()
                .Interceptors(new InterceptorReference(typeof(TestInterceptor))).Anywhere
               );
            container.Resolve<EventTest>().Run();
        }
    }
    
     [Interceptor(typeof(TestInterceptor))]
    public class EventTest : IEventTest
    {
        public void Run()
        {}
        
    }

    public interface IEventTest
    {
        void Run();
    }

    public class TestInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
        }
    }
}