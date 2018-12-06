using Blocks.Framework.Ioc.Dependency;
using Blocks.Framework.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks.Framework.RPCProxy.Manager
{
    public class RPCApiManager 
    {
        public IList<Type> rPCProxies;

        public RPCApiManager()
        {
            rPCProxies = new List<Type>();
        }

        public void Register(Type[] types)
        {
            rPCProxies.AddRange(types);
        }


        public IList<Type> GetAll()
        {
            return rPCProxies;
        }
    }
}
