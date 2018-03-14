using System;

namespace Blocks.Framework.Services.DataTransfer
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
