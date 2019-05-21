using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Abp.IO.Extensions;

namespace Blocks.Framework.Localization.Dictionaries
{
    internal static class Utf8Helper
    {
        public static string ReadStringFromStream(Stream stream)
        {
            var bytes = stream.GetAllBytes();
            var skipCount = HasBom(bytes) ? 3 : 0;
            return Encoding.UTF8.GetString(bytes, skipCount, bytes.Length - skipCount);
        }
        
        public static async Task<string> ReadStringFromStreamAsync(Stream stream)
        {
            var bytes = new Byte[stream.Length];

            var length = await stream.ReadAsync(bytes, 0, (int)stream.Length);
            var skipCount = HasBom(bytes) ? 3 : 0;
            var result = Encoding.UTF8.GetString(bytes, skipCount, bytes.Length - skipCount);
            return result;
        }

        private static bool HasBom(byte[] bytes)
        {
            if (bytes.Length < 3)
            {
                return false;
            }

            if (!(bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF))
            {
                return false;
            }

            return true;
        }
    }
}
