using System;
using System.IO;
using System.Linq;
using Blocks.Framework.FileSystems;
using Blocks.Framework.FileSystems.VirtualPath;
using Xunit;

namespace Blocks.Framework.Test.FileSystems.VirtualPath
{
    public class DefaultVirtualPathProviderTest
    {
        private readonly DefaultVirtualPathProvider _defaultVirtualPathProvider;
        private readonly string _currentRootPath;
        public DefaultVirtualPathProviderTest()
        {
            _currentRootPath = Directory.GetCurrentDirectory();
            var currentHostingEnvironment = WebHostingEnvironment.CreateHostingEnvironment(new WebHostingEnvironment()
            {
                ContentRootPath =_currentRootPath

            });
            _defaultVirtualPathProvider = new DefaultVirtualPathProvider(currentHostingEnvironment);
        }

        [Fact]
        public void ListFilesAndDirectoriesTest()
        {
            var listFiles = _defaultVirtualPathProvider.ListFiles("/").ToList();
            
            var listDirectories = _defaultVirtualPathProvider.ListDirectories("/").ToList();
        }

        [Fact]
        public void MapPathTest()
        {
            var serverPath = _defaultVirtualPathProvider.MapPath("~/bin");
            var localPath = _defaultVirtualPathProvider.MapPath("/bin");
            Assert.Equal(serverPath,Path.Combine(_currentRootPath,"bin"));
            Assert.Equal(localPath,Path.Combine(_currentRootPath,"bin"));

        }
        [Fact]
        public void CreateAndUpdateAndListFile_CreateAndUpdateAndListDirectory()
        {
            var directoryName = Guid.NewGuid().ToString();
            var fileName = directoryName + "/" + Guid.NewGuid().ToString();
            try
            {
                _defaultVirtualPathProvider.CreateDirectory(directoryName);
                Assert.True( _defaultVirtualPathProvider.DirectoryExists(directoryName));

                using(var fileStream = _defaultVirtualPathProvider.CreateText(fileName))
                {
                    fileStream.WriteLine(string.Empty);
                }
                Assert.True( _defaultVirtualPathProvider.FileExists(fileName));

                Assert.Equal(1, _defaultVirtualPathProvider.ListFiles(directoryName).Count());
            }
            finally
            {
                foreach (var fileInfo in  _defaultVirtualPathProvider.ListFiles(directoryName))
                {
                    _defaultVirtualPathProvider.DeleteFile(fileInfo);
                }
               
                _defaultVirtualPathProvider.DeleteDirectory(directoryName);
            }
        }
        
        
        [Fact]
        public void CreateAndUpdateAndListFile_CreateAndUpdateAndListDirectory_TestRootPath()
        {
            var directoryName = "~/" + Guid.NewGuid().ToString();
            var fileName = directoryName + "/" + Guid.NewGuid().ToString();
            try
            {
                _defaultVirtualPathProvider.CreateDirectory(directoryName);
                Assert.True( _defaultVirtualPathProvider.DirectoryExists(directoryName));

                using(var fileStream = _defaultVirtualPathProvider.CreateText(fileName))
                {
                    fileStream.WriteLine(string.Empty);
                }
                Assert.True( _defaultVirtualPathProvider.FileExists(fileName));

                Assert.Equal(1, _defaultVirtualPathProvider.ListFiles(directoryName).Count());
            }
            finally
            {
                foreach (var fileInfo in  _defaultVirtualPathProvider.ListFiles(directoryName))
                {
                    _defaultVirtualPathProvider.DeleteFile(fileInfo);
                }
               
                _defaultVirtualPathProvider.DeleteDirectory(directoryName);
            }
        }
    }
}