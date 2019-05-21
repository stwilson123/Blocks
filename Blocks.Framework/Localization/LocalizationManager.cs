using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Abp;
using Abp.Dependency;
using Blocks.Framework.Localization.Source;
using Castle.Core.Logging;
using ILocalizationConfiguration = Blocks.Framework.Localization.Configuartions.ILocalizationConfiguration;

namespace Blocks.Framework.Localization
{
    internal class LocalizationManager : ILocalizationManager
    {
        public ILogger Logger { get; set; }

        private readonly ILanguagesManager _languageManager;
        private readonly ILocalizationConfiguration _configuration;
        private readonly IIocResolver _iocResolver;
        private readonly IDictionary<string, ILocalizationSource> _sources;

        /// <summary>
        /// Constructor.
        /// </summary>
        public LocalizationManager(
            ILanguagesManager languageManager,
            ILocalizationConfiguration configuration, 
            IIocResolver iocResolver)
        {
            Logger = NullLogger.Instance;
            _languageManager = languageManager;
            _configuration = configuration;
            _iocResolver = iocResolver;
            _sources = new Dictionary<string, ILocalizationSource>();
        }

        public void Initialize()
        {
            InitializeSources();
        }

        private void InitializeSources()
        {
            if (!_configuration.IsEnabled)
            {
                Logger.Debug("Localization disabled.");
                return;
            }

            Logger.Debug(string.Format("Initializing {0} localization sources.", _configuration.Providers.Count));

            var sourceNames = _configuration.Providers.Select(p => p.SourceName);
            var doubleSourceNames = sourceNames.Except(sourceNames.Distinct());
            if (doubleSourceNames.Any())
            {
                throw new AbpException("There are more than one source with name: " + 
                                  string.Join(",",doubleSourceNames) + "! Source name must be unique!");

            }
             
            Task.WaitAll(_configuration.Providers.Select(p =>
            {
                
                return p.Initialize();
            }).ToArray());
            
            foreach (var provider in _configuration.Providers)
            {
             

                _sources[provider.SourceName] = new DefaultLocalizationSource(provider.SourceName,provider,_configuration);  

//                //Extending dictionaries
//                if (source is IDictionaryBasedLocalizationSource)
//                {
//                    var dictionaryBasedSource = source as IDictionaryBasedLocalizationSource;
//                    var extensions = _configuration.Sources.Extensions.Where(e => e.SourceName == source.Name).ToList();
//                    foreach (var extension in extensions)
//                    {
//                        extension.DictionaryProvider.Initialize(source.Name);
//                        foreach (var extensionDictionary in extension.DictionaryProvider.Dictionaries.Values)
//                        {
//                            dictionaryBasedSource.Extend(extensionDictionary);
//                        }
//                    }
//                }

                Logger.Debug("Initialized localization source: " + provider.SourceName);
            }
        }

        /// <summary>
        /// Gets a localization source with name.
        /// </summary>
        /// <param name="name">Unique name of the localization source</param>
        /// <returns>The localization source</returns>
        public ILocalizationSource GetSource(string name)
        {
            if (!_configuration.IsEnabled)
            {
                return null;
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            ILocalizationSource source;
            if (!_sources.TryGetValue(name, out source))
            {
                throw new AbpException("Can not find a source with name: " + name);
            }

            return source;
        }

        /// <summary>
        /// Gets all registered localization sources.
        /// </summary>
        /// <returns>List of sources</returns>
        public IReadOnlyList<ILocalizationSource> GetAllSources()
        {
            return _sources.Values.ToImmutableList();
        }
    }
}