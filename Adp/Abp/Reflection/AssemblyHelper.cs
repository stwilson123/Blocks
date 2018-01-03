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

            //filter Repeat Assembly

            var assemblyRepeatFilesGroup = assemblyFiles
                .GroupBy(t => t.Substring(t.LastIndexOf(@"\"))).Where(t => t.Count() > 1);
            var assemblyRepeatFiles = assemblyRepeatFilesGroup
                .Select(t => t.FirstOrDefault() )
                .ToArray();
            ;
            var loadAssembly = assemblyFiles.Except(assemblyRepeatFilesGroup.SelectMany(t => t))
                                            .Concat(assemblyRepeatFiles);
               
            return loadAssembly.Select(
                Assembly.LoadFile
            ).ToList();
        }
    }
}
