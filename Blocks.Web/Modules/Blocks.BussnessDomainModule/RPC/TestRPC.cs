using Blocks.Framework.RPCProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blocks.BussnessDomainModule.RPC
{
    public class TestRPC : IRPCProxy
    {
        [RequestMappingAttribute("/BussnessWebModule/Test/ProxFunction")]
        public virtual List<string> ProxFunction(ProxModel id)
        {
            throw new NotImplementedException();
        }
    }

    public interface ITest2Rpc : IRPCClientProxy
    {
        [RequestMappingAttribute("/BussnessWebModule/Test/ProxFunction")]
        List<string> ProxFunction(ProxModel id);
    }

    public class Test2RPC : ITest2Rpc
    {
        [RequestMappingAttribute("/BussnessWebModule/Test/ProxFunction")]
        public virtual List<string> ProxFunction(ProxModel id)
        {
            throw new NotImplementedException();
        }
    }
    public class SecondTest2RPC : ITest2Rpc
    {
        [RequestMappingAttribute("/BussnessWebModule/Test/ProxFunction")]
        public virtual List<string> ProxFunction(ProxModel id)
        {
            throw new NotImplementedException();
        }
    }

    public class ProxModel
    {
        public Dictionary<string, string> dic { get; set; }
    }
}
