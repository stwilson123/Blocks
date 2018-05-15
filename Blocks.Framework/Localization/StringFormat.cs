using System;

namespace Blocks.Framework.Localization
{
    public class StringLocal
    {
        public string FormatStr { get; private set; }
        
        public object[] FormatArgs { get; private set; }

        private StringLocal()
        {
            
        }
        private StringLocal(string formatStr, object[] formatArgs)
        {
            FormatStr = formatStr;
            FormatArgs = formatArgs;
        }
        
        public static StringLocal Format(string format, params object[] args)
        {
            return new StringLocal(format, args);
        }

        public override string ToString()
        {
            return FormatArgs != null ? FormatStr : string.Format(FormatStr, FormatArgs);
        }
    }
}