using System;

namespace Blocks.Framework.Environment.Extensions.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class BlocksFeatureAttribute : Attribute {
        public BlocksFeatureAttribute(string text) {
            FeatureName = text;
        }

        public string FeatureName { get; set; }
    }
}