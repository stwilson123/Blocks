using System.IO;
using Blocks.Framework.FileSystems;
using Blocks.Framework.FileSystems.VirtualPath;
using Xunit;

namespace Blocks.Framework.Test.FileSystems.VirtualPath
{
    public class DefaultVirtualPathProviderTest
    {
        private readonly DefaultVirtualPathProvider _defaultVirtualPathProvider; 
        public DefaultVirtualPathProviderTest()
        {
            var currentHostingEnvironment = WebHostingEnvironment.CreateHostingEnvironment(new WebHostingEnvironment()
            {
                ContentRootPath = Directory.GetCurrentDirectory()

            });
            _defaultVirtualPathProvider = new DefaultVirtualPathProvider(currentHostingEnvironment);
        }

        [Fact]
        public void ListFilesAndDirectoriesTest()
        {
            var listFiles = _defaultVirtualPathProvider.ListFiles("/");
        }
    }
}