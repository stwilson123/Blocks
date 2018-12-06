using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Web;
using Microsoft.Extensions.Caching.Memory;
using Blocks.Framework.Caching;
using Blocks.Framework.Collections;
using Blocks.Framework.Services;
using Castle.Core.Logging;
using Castle.DynamicProxy.Generators;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;

namespace Blocks.Framework.FileSystems.VirtualPath
{
     public class DefaultVirtualPathMonitor : IVirtualPathMonitor {
        private readonly Thunk _thunk;
        private readonly string _prefix = Guid.NewGuid().ToString("n");
        private readonly IDictionary<string, Weak<Token>> _tokens = new Dictionary<string, Weak<Token>>();
        private readonly IClock _clock;
         private readonly IVirtualPathProvider _hostingVirtualPathProvider;
         private readonly LazyConcurrentDictionary<string, object> _caches = new LazyConcurrentDictionary<string, object>();

        public DefaultVirtualPathMonitor(IClock clock,IVirtualPathProvider hostingVirtualPathProvider) {
            _clock = clock;
            _hostingVirtualPathProvider = hostingVirtualPathProvider;
            _thunk = new Thunk(this);
            Logger = NullLogger.Instance;
        }
         private FileSystemWatcher CreateFileSystemWatcher(string directoryName,FileSystemEventHandler  fsy_Changed)
         {
             FileSystemWatcher fsy = new FileSystemWatcher(directoryName, "*.*");
             fsy.EnableRaisingEvents = true;
             fsy.IncludeSubdirectories = false;
             fsy.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.FileName | NotifyFilters.DirectoryName;
             fsy.Changed += fsy_Changed;
             return fsy;
         }

        public ILogger Logger { get; set; }

        public IVolatileToken WhenPathChanges(string virtualPath)
        {
            
            var fullPath = _hostingVirtualPathProvider.MapPath(virtualPath);
            var token = BindToken(fullPath);
            try {
                BindSignal(fullPath);
                
            }
            //TODO Exception must change to httpException but not found in .net code
            catch (Exception  e) {
                // This exception happens if trying to monitor a directory or file
                // inside a directory which doesn't exist
                Logger.InfoFormat(e, "Error monitoring file changes on virtual path '{0}'", virtualPath);

                //TODO: Return a token monitoring first existing parent directory.
            }
            return token;
        }

        private Token BindToken(string virtualPath) {
            lock (_tokens) {
                Weak<Token> weak;
                if (!_tokens.TryGetValue(virtualPath, out weak)) {
                    weak = new Weak<Token>(new Token(virtualPath));
                    _tokens[virtualPath] = weak;
                }

                var token = weak.Target;
                if (token == null) {
                    token = new Token(virtualPath);
                    weak.Target = token;
                }

                return token;
            }
        }

        private Token DetachToken(string virtualPath) {
            lock (_tokens) {
                Weak<Token> weak;
                if (!_tokens.TryGetValue(virtualPath, out weak)) {
                    return null;
                }
                var token = weak.Target;
                weak.Target = null;
                return token;
            }
        }

        private void BindSignal(string virtualPath) {
            BindSignal(virtualPath, _thunk.Signal);

        }

        private void BindSignal(string virtualPath, Action<string,object> fileChangesCallback) {
            string key = _prefix + virtualPath;

            //PERF: Don't add in the cache if already present. Creating a "CacheDependency"
            //      object (below) is actually quite expensive.
       
            _caches.GetOrAdd(key, (k) =>
                {
                    Logger.DebugFormat("Monitoring virtual path \"{0}\"", virtualPath);

                    if(File.Exists(virtualPath))
                    CreateFileSystemWatcher(virtualPath, (fileObject,args) => fileChangesCallback(key,virtualPath));
                    return virtualPath;
                });

           
//            if ( _caches.get(key) != null)
//                return;
//
//            var cacheDependency = HostingEnvironment.VirtualPathProvider.GetCacheDependency(
//                virtualPath,
//                new[] { virtualPath },
//                _clock.UtcNow);
//
//            Logger.DebugFormat("Monitoring virtual path \"{0}\"", virtualPath);
//
//            cache.Set(
//                key,
//                virtualPath,
//                cacheDependency,
//                Cache.NoAbsoluteExpiration,
//                Cache.NoSlidingExpiration,
//                    CacheItemPriority.NotRemovable,
//                callback);
        }

        public void Signal(string key, object value) {
            var virtualPath = Convert.ToString(value);
            Logger.DebugFormat("Virtual path changed ({1}) '{0}'", virtualPath);

            var token = DetachToken(virtualPath);
            if (token != null)
                token.IsCurrent = false;
        }

        public class Token : IVolatileToken {
            public Token(string virtualPath) {
                IsCurrent = true;
                VirtualPath = virtualPath;
            }
            public bool IsCurrent { get; set; }
            public string VirtualPath { get; private set; }

            public override string ToString() {
                return string.Format("IsCurrent: {0}, VirtualPath: \"{1}\"", IsCurrent, VirtualPath);
            }
        }

        class Thunk {
            private readonly Weak<DefaultVirtualPathMonitor> _weak;

            public Thunk(DefaultVirtualPathMonitor provider) {
                _weak = new Weak<DefaultVirtualPathMonitor>(provider);
            }

            public void Signal(string key, object value) {
                var provider = _weak.Target;
                if (provider != null)
                    provider.Signal(key, value);
            }
        }
    }
}