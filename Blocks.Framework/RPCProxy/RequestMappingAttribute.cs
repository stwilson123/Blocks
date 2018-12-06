using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.RPCProxy
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequestMappingAttribute : Attribute
    {
        public string Path { get; set; }
        public RequestMappingAttribute(string path)
        {
            Path = path;
        }

    }
}
