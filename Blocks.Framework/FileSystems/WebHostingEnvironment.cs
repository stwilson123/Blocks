using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.FileProviders;
 

namespace Blocks.Framework.FileSystems
{
    public class WebHostingEnvironment
    {
        public string ContentRootPath { set; get; }
        public static HostingEnvironment CreateHostingEnvironment(WebHostingEnvironment hostingEnvironment)
        {
            return new HostingEnvironment()
            {
                ContentRootPath = hostingEnvironment.ContentRootPath,
                ContentRootFileProvider = new PhysicalFileProvider( hostingEnvironment.ContentRootPath)
            };
        }
        
      
    }
}