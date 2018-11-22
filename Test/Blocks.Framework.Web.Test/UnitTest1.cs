using System;
using Blocks.Framework.Navigation;
using Xunit;

namespace Blocks.Framework.Web.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            (new NavigationDefinition(null)).Items[0].GetUniqueId();
        }
    }
}