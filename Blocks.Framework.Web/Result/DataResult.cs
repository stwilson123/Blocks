using System;
using System.Runtime.Serialization;
using Abp.Web.Models;

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
//        [DataMember]
//        public string token { get; set; }
    }
}