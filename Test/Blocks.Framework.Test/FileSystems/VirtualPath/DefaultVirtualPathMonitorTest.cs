using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using Blocks.Framework.FileSystems;
using Blocks.Framework.FileSystems.VirtualPath;
using Blocks.Framework.Services;
using Castle.Core.Logging;
using Castle.MicroKernel.Lifestyle;
using Xunit;

namespace Blocks.Framework.Test.FileSystems.VirtualPath
{
    public class DefaultVirtualPathMonitorTest
    {
        IVirtualPathMonitor _patchMonitor ;
        private string fileName;
        private readonly string _currentRootPath;
        public DefaultVirtualPathMonitorTest()
        {
            _currentRootPath = Directory.GetCurrentDirectory();
            var currentHostingEnvironment = WebHostingEnvironment.CreateHostingEnvironment(new WebHostingEnvironment()
            {
                ContentRootPath =_currentRootPath

            });
            _patchMonitor = new DefaultVirtualPathMonitor(new Clock(),new DefaultVirtualPathProvider(currentHostingEnvironment));
            fileName = Guid.NewGuid().ToString();
        }
        
        [Fact]
        public void VirtualPathChangesTest()
        {
            try
            {
                using (var stream = File.Create(fileName))
                {
                    using (var sw = new StreamWriter(stream))
                    {
                        sw.Write(Guid.NewGuid().ToString());
                    }
                }

                var fileToken = _patchMonitor.WhenPathChanges("./");
                File.WriteAllText(fileName, Guid.NewGuid().ToString());
                //Wait for fileWatcher event fire
                Thread.Sleep(10);
                Assert.False(fileToken.IsCurrent);
               // File.SetLastWriteTime(fileName,DateTime.Now);
            }
            finally
            {
                File.Delete(fileName);
            }
            
        
            
        }
    }

}