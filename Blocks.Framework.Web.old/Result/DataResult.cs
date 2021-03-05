using Blocks.Framework.Web.Web.Result;
using Blocks.Web.Models;
using System;
using System.Runtime.Serialization;

namespace Blocks.Framework.Web.Result
{
    [Serializable]
    [DataContract]
    public class DataResult : AjaxResponse<object>
    {
        [DataMember]
        public dynamic content { get; set; }
        [DataMember]
        public string logID { get; set; }
        [DataMember]
        public string msg { get; set; }
        [DataMember]
        public string code { get; set; }


        public bool success { get; set; }

        public Web.Result.ErrorInfo error { get; set; }
        //        [DataMember]
        //        public string token { get; set; }
    }
}