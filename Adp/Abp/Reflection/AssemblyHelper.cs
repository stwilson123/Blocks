using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Abp.Reflection
{
    internal static class AssemblyHelper
    {
        public static List<Assembly> GetAllAssembliesInFolder(string folderPath, SearchOption searchOption)
        {
            var assemblyFiles = Directory
                .EnumerateFiles(folderPath, "*.*", searchOption)
                .Where(s => (s.EndsWith(".dll") || s.EndsWith(".exe")) && s.EndsWith("Module.dll") || s.EndsWith("Module.exe"));

            return assemblyFiles.Select(
                Assembly.LoadFile
            ).ToList();
        }
    }
}
