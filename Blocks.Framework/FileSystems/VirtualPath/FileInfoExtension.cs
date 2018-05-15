using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Blocks.Framework.Localization;
using Microsoft.Extensions.FileProviders;

namespace Blocks.Framework.FileSystems.VirtualPath
{
    public static class FileInfoExtension
    {
        private static readonly char[] _pathSeparators = new char[2]
        {
            Path.DirectorySeparatorChar,
            Path.AltDirectorySeparatorChar
        };
        private static readonly string[] relativeSymbols =  new string[]{ "~\\", "~/"};

        public static string ToAppRelative(this IFileInfo fileInfo,string appRootPath)
        {
            
            var matchIndex = fileInfo.PhysicalPath.IndexOf(appRootPath);
            if (matchIndex < 0)
                throw new FileSystemsException(StringLocal.Format("ToAppRelative Fail.Current fileInfo could't match \"{0}\" appRootPath.",appRootPath));
            return fileInfo.PhysicalPath.Remove(0,matchIndex + appRootPath.Length);
        }

        public static string MapPath(this IFileProvider fileProvider,string subpath)
        {
            if(!(fileProvider is PhysicalFileProvider))
                throw new VirtualPathException(StringLocal.Format("fileProvider must be PhysicalFileProvider"));

            if (string.IsNullOrEmpty(subpath) || PathUtils.HasInvalidPathChars(subpath))
                throw new VirtualPathException(StringLocal.Format("Path \"{0}\" is not available", subpath));
            subpath = subpath.TrimStart(_pathSeparators);
            if (Path.IsPathRooted(subpath))
                throw new VirtualPathException(StringLocal.Format("Path \"{0}\" is already root path", subpath));
            string fullPath = GetFullPath(fileProvider, subpath);
            if (fullPath == null)
                throw new VirtualPathException(StringLocal.Format("Path \"{0}\" is not available", subpath));
            return fullPath;
        }

        private static string GetFullPath(IFileProvider fileProvider, string path)
        {
            var actFileProvider = (fileProvider as PhysicalFileProvider);
            if (PathUtils.PathNavigatesAboveRoot(path))
                return (string) null;
            string fullPath;
            try
            {
                fullPath = Path.GetFullPath(Path.Combine(actFileProvider.Root, path));
            }
            catch
            {
                return (string) null;
            }

            if (!IsUnderneathRoot(actFileProvider,fullPath))
                return (string) null;
            return fullPath;
        }
        
        private static  bool IsUnderneathRoot(PhysicalFileProvider fileProvider,string fullPath)
        {
            return fullPath.StartsWith(fileProvider.Root, StringComparison.OrdinalIgnoreCase);
        }
        
        
        public static string RemoveRelativeSymbols(string virtualPath)
        {
            if (virtualPath != null &&  IsRelativePath(virtualPath))
            { 
                return IsRelativePath(virtualPath)  ? virtualPath.Substring(relativeSymbols[0].Length) : virtualPath;
                return IsRelativePath(virtualPath)  ? virtualPath.Substring(relativeSymbols[0].Length) : virtualPath;
            }

            return virtualPath;
        }
        
        private static bool IsRelativePath(string path)
        {
            return string.IsNullOrEmpty(path) ? false : relativeSymbols.Any(sym => path.StartsWith(sym)) ;
        }
    }
}