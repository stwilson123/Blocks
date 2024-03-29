﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Abp.Extensions;
using Blocks.Framework.Exceptions;
using Blocks.Framework.FileSystems.VirtualPath;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;

namespace Blocks.Framework.FileSystems.VirtualPath
{
     public class DefaultVirtualPathProvider : IVirtualPathProvider
     {
         public ILogger Logger { get; set; }
         private IHostingEnvironment _hostingEnvironment;
         private IFileProvider _webFileProvider;
         private string _fileRoot;
         public DefaultVirtualPathProvider(IHostingEnvironment hostingEnvironment) {
            Logger = NullLogger.Instance;
            _hostingEnvironment = hostingEnvironment;
             _webFileProvider = _hostingEnvironment.ContentRootFileProvider;
             _fileRoot = _hostingEnvironment.ContentRootPath;
         }

      

        public virtual string GetDirectoryName(string virtualPath) {
            
            return Path.GetDirectoryName(virtualPath).Replace(Path.DirectorySeparatorChar, '/');
        }

        public virtual IEnumerable<string> ListFiles(string path)
        {
            return _hostingEnvironment.ContentRootFileProvider.GetDirectoryContents(FileInfoExtension.RemoveRelativeSymbols(path))
                .Where(f => !f.IsDirectory)
                .Select(t => t.ToAppRelative(_hostingEnvironment.ContentRootPath));
//            return HostingEnvironment
//                .VirtualPathProvider
//                .GetDirectory(path)
//                .Files
//                .OfType<VirtualFile>()
//                .Select(f => VirtualPathUtility.ToAppRelative(f.VirtualPath));
        }

        public virtual IEnumerable<string> ListDirectories(string path) {
            return _webFileProvider.GetDirectoryContents(FileInfoExtension.RemoveRelativeSymbols(path))
                .Where(f => f.IsDirectory)
                .Select(t => t.ToAppRelative(_hostingEnvironment.ContentRootPath));
//            return HostingEnvironment
//                .VirtualPathProvider
//                .GetDirectory(path)
//                .Directories
//                .OfType<VirtualDirectory>()
//                .Select(d => VirtualPathUtility.ToAppRelative(d.VirtualPath));
        }

        public virtual string Combine(params string[] paths) {
            return Path.Combine(paths).Replace(Path.DirectorySeparatorChar, '/');
        }

        public virtual string ToAppRelative(string virtualPath) {
            if (IsMalformedVirtualPath(virtualPath))
                return null;

            try {
//                string result = VirtualPathUtility.ToAppRelative(virtualPath);
                string result = _webFileProvider.GetFileInfo(virtualPath).ToAppRelative(_hostingEnvironment.ContentRootPath);

                // In some cases, ToAppRelative doesn't normalize the path. In those cases,
                // the path is invalid.
                // Example:
                //   ApplicationPath: /Foo
                //   VirtualPath    : ~/Bar/../Blah/Blah2
                //   Result         : /Blah/Blah2  <= that is not an app relative path!
                if (!result.StartsWith("~/")) {
                    Logger.InfoFormat("Path '{0}' cannot be made app relative: Path returned ('{1}') is not app relative.", virtualPath, result);
                    return null;
                }
                return result;
            }
            catch (Exception ex) {
                if (ex.IsFatal()) {
                    throw;
                } 
                // The initial path might have been invalid (e.g. path indicates a path outside the application root)
                Logger.InfoFormat(ex, "Path '{0}' cannot be made app relative", virtualPath);
                return null;
            }
        }

        /// <summary>
        /// We want to reject path that contains ".." going outside of the application root.
        /// ToAppRelative does that already, but we want to do the same while avoiding exceptions.
        /// 
        /// Note: This method doesn't detect all cases of malformed paths, it merely checks
        ///       for *some* cases of malformed paths, so this is not a replacement for full virtual path
        ///       verification through VirtualPathUtilty methods.
        ///       In other words, !IsMalformed does *not* imply "IsWellformed".
        /// </summary>
        public bool IsMalformedVirtualPath(string virtualPath) {
            if (string.IsNullOrEmpty(virtualPath))
                return true;

            if (virtualPath.IndexOf("..") >= 0) {
                virtualPath = virtualPath.Replace(Path.DirectorySeparatorChar, '/');
                string rootPrefix = virtualPath.StartsWith("~/") ? "~/" : virtualPath.StartsWith("/") ? "/" : "";
                if (!string.IsNullOrEmpty(rootPrefix)) {
                    string[] terms = virtualPath.Substring(rootPrefix.Length).Split('/');
                    int depth = 0;
                    foreach (var term in terms) {
                        if (term == "..") {
                            if (depth == 0) {
                                Logger.InfoFormat("Path '{0}' cannot be made app relative: Too many '..'", virtualPath);
                                return true;
                            }
                            depth--;
                        }
                        else {
                            depth++;
                        }
                    }
                }
            }

            return false;
        }

        public virtual Stream OpenFile(string virtualPath) {
            return _webFileProvider.GetFileInfo(virtualPath).CreateReadStream();

//            return HostingEnvironment.VirtualPathProvider.GetFile(virtualPath).Open();
        }

        public virtual StreamWriter CreateText(string virtualPath) {
            return File.CreateText(MapPath(virtualPath));
        }

        public virtual Stream CreateFile(string virtualPath) {
            return File.Create(MapPath(virtualPath));
        }

        public virtual DateTime GetFileLastWriteTimeUtc(string virtualPath) {
#if true
            // This code is less "pure" than the code below, but performs fewer file I/O, and it 
            // has been measured to make a significant difference (4x) on slow file systems.
            return File.GetLastWriteTimeUtc(MapPath(virtualPath));
#else
            var dependency = HostingEnvironment.VirtualPathProvider.GetCacheDependency(virtualPath, new[] { virtualPath }, DateTime.UtcNow);
            if (dependency == null) {
                throw new Exception(string.Format("Invalid virtual path: '{0}'", virtualPath));
            }
            return dependency.UtcLastModified;
#endif
        }

//        public string GetFileHash(string virtualPath) {
//            return GetFileHash(virtualPath, new[] { virtualPath });
//        }
//
//        public string GetFileHash(string virtualPath, IEnumerable<string> dependencies) {
//         
//        return HostingEnvironment.VirtualPathProvider.GetFileHash(virtualPath, dependencies);
//        }

        public virtual void DeleteFile(string virtualPath) {
            File.Delete(MapPath(virtualPath));
        }

        public virtual string MapPath(string virtualPath)
        {
            string path =   FileInfoExtension.RemoveRelativeSymbols(virtualPath);

          
            return _webFileProvider.MapPath(path);
//            return HostingEnvironment.MapPath(virtualPath);
        }

        public virtual bool FileExists(string virtualPath) {
            return _webFileProvider.GetFileInfo(FileInfoExtension.RemoveRelativeSymbols(virtualPath)).Exists;

//            return HostingEnvironment.VirtualPathProvider.FileExists(virtualPath);
        }

        public virtual bool TryFileExists(string virtualPath) {
            if (IsMalformedVirtualPath(virtualPath))
                return false;

            try {
                return FileExists(virtualPath);
            }
            catch (Exception ex) {
                if (ex.IsFatal()) {
                    throw;
                } 
                Logger.InfoFormat(ex, "File '{0}' can not be checked for existence. Assuming doesn't exist.", virtualPath);
                return false;
            }
        }

        public virtual bool DirectoryExists(string virtualPath)
        {
            return _webFileProvider.GetDirectoryContents(FileInfoExtension.RemoveRelativeSymbols(virtualPath)).Exists;
//            return HostingEnvironment.VirtualPathProvider.DirectoryExists(virtualPath);
        }

        public virtual void CreateDirectory(string virtualPath) {
            Directory.CreateDirectory(MapPath(virtualPath));
        }

        public virtual void DeleteDirectory(string virtualPath) {
            Directory.Delete(MapPath(virtualPath));
        }

        

         
    }
}