using System;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Logging;
using Blocks.Framework.Environment.Configuration;
using Blocks.Framework.Ioc.Dependency;
using Blocks.Framework.Localization;
using Castle.Core.Logging;

namespace Blocks.Framework.Configurations
{
    public class AppConfigManager
    {
        private static IAbpStartupConfiguration AbpConfig;
        static AppConfigManager()
        {
            if (!IocManager.Instance.IsRegistered(typeof(IAbpStartupConfiguration)))
                throw new ConfigurationException(StringLocal.Format($"Blocks system must be init IAbpStartupConfiguration first"));
                
            AbpConfig = IocManager.Instance.Resolve<IAbpStartupConfiguration>();
        }
        
        public static T  GetAppstartConfig<T>() where T : IConfiguration
        {
            return (T)AbpConfig.GetOrCreate(typeof(T).FullName, () => AbpConfig.IocManager.Resolve(typeof(T)));
        }
    }
}