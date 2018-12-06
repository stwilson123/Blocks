using Blocks.Framework.Exceptions;
using Blocks.Framework.Localization;
using Blocks.Framework.Reflection.Extensions;
using Blocks.Framework.Utility.Extensions;
using Castle.DynamicProxy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks.Framework.RPCProxy
{
    public class RPCInterceptor : IInterceptor
    {
        //    private readonly T _proxiedObject;

        private readonly HttpContextModel _httpContextModel;
        public RPCInterceptor(HttpContextModel httpContextModel)
        {
            this._httpContextModel = httpContextModel;
        }
        public RPCInterceptor()
        {
           // _proxiedObject = proxiedObject;

        }


        public void Intercept(IInvocation invocation)
        {

            var requestAttribute = invocation.Method.GetSingleAttributeOrNull<RequestMappingAttribute>();
            if(requestAttribute == null || requestAttribute.Path.IsNullOrEmpty())
            {
                throw new BlocksException(StringLocal.Format("Request Attribute is null or empty!"));
            }
            var url = _httpContextModel.RequestUrl;
            var prePath = $"{url.Scheme}://{url.Host}:{url.Port}";
            var path = prePath + "/api/services" + requestAttribute.Path;

            var dataResult = HttpWebClient.GetResponse<DataResult>(path, invocation.Arguments.FirstOrDefault());

            invocation.ReturnValue = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(dataResult.content),invocation.Method.ReturnType);
            ;
          //  var a = invocation.Method.CustomAttributes.FirstOrDefault(t => t.)



        }
    }


    public class DataResult
    {
        
        public dynamic content { get; set; }
        public string logID { get; set; }
        public string msg { get; set; }
        public string code { get; set; }
    }
}
