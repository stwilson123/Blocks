using System;
using System.Diagnostics;
using System.Reflection;
using Abp;
using Abp.AutoMapper;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Reflection;
using AutoMapper;
using Castle.MicroKernel.Registration;

namespace Blocks.Framework.AutoMapper
{
    [DependsOn(typeof(AbpKernelModule))]
    public class BlocksAutoMapperModule : AbpModule
    {
        private readonly ITypeFinder _typeFinder;

        private static volatile bool _createdMappingsBefore;
        private static readonly object SyncObj = new object();
        
        public BlocksAutoMapperModule(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        public override void PreInitialize()
        {
            IocManager.Register<IBlocksAutoMapperConfiguration, BlocksAutoMapperConfiguration>();

            Configuration.ReplaceService<Abp.ObjectMapping.IObjectMapper, AutoMapperObjectMapper>();
        }

        public override void Initialize()
        {
            CreateMappings();

        }

        public override void PostInitialize()
        {
        }

        private void CreateMappings()
        {
            lock (SyncObj)
            {
                Action<IMapperConfigurationExpression> configurer = configuration =>
                {
                    //FindAndAutoMapTypes(configuration);
                    foreach (var configurator in Configuration.Modules.BlocksAutoMapper().Configurators)
                    {
                        configurator(configuration);
                    }
                };

                if (Configuration.Modules.BlocksAutoMapper().UseStaticMapper)
                {
                    //We should prevent duplicate mapping in an application, since Mapper is static.
                    if (!_createdMappingsBefore)
                    {
                        Stopwatch stopWatch = Stopwatch.StartNew();
                        Mapper.Initialize(configurer);
                        stopWatch.Stop();
                        Logger.Debug($"Mapper Initalize cost time {stopWatch.ElapsedMilliseconds}ms");
                        _createdMappingsBefore = true;
                    }

                    IocManager.IocContainer.Register(
                        Component.For<IMapper>().Instance(Mapper.Instance).LifestyleSingleton()
                    );
                }
                else
                {
                    var config = new MapperConfiguration(configurer);
                    IocManager.IocContainer.Register(
                        Component.For<IMapper>().Instance(config.CreateMapper()).LifestyleSingleton()
                    );
                }
            }
        }

        private void FindAndAutoMapTypes(IMapperConfigurationExpression configuration)
        {
            var types = _typeFinder.Find(type =>
                {
                    var typeInfo = type.GetTypeInfo();
                    return typeInfo.IsDefined(typeof(AutoMapAttribute)) ||
                           typeInfo.IsDefined(typeof(AutoMapFromAttribute)) ||
                           typeInfo.IsDefined(typeof(AutoMapToAttribute));
                }
            );

            Logger.DebugFormat("Found {0} classes define auto mapping attributes", types.Length);

            foreach (var type in types)
            {
                Logger.Debug(type.FullName);
                configuration.CreateAutoAttributeMaps(type);
            }
        }

       
    }
}
