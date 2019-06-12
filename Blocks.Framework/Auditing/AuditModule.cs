using Abp.Modules;

namespace Blocks.Framework.Auditing
{
    public class AuditModule: AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IAuditingConfiguration, AuditingConfiguration>();

            var auditingConfig = IocManager.Resolve<IAuditingConfiguration>();
            auditingConfig.IsEnabled = true;
            auditingConfig.IsEnabledForAnonymousUsers = true;
            
        }
    }
}