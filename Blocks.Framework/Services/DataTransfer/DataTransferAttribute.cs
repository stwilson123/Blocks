using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blocks.Framework.Json
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Parameter, AllowMultiple = false)]
    public class DataTransferAttribute : Attribute
    {
        //TODO add addtional property to match JsonPropertyAttribute
        public DataTransferAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }
        public string PropertyName { get; private set; }
    }
}
