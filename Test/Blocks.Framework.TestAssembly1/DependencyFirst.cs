using Blocks.Framework.Ioc.Dependency;
using System;

namespace Blocks.Framework.TestAssembly1
{
    public class TestDependencyFirst : ITestDependency
    {
    }


    public interface ITestDependency : ITransientDependency
    {

    }
}
