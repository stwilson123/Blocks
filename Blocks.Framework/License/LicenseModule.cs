using Blocks.Framework.Ioc;
using Blocks.Framework.License.HardwareInfo;
using Blocks.Framework.Localization;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Blocks.Framework.License
{
    public class LicenseModule : BlocksModule
    {

        public override void PreInitialize()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                IocManager.Register<IMachineInfo, WindowMachineInfo>(Abp.Dependency.DependencyLifeStyle.Transient);
            else
                IocManager.Register<IMachineInfo, LinuxMachineInfo>(Abp.Dependency.DependencyLifeStyle.Transient);
            IocManager.Register<LicenseManager>();

            IocManager.Resolve<LicenseManager>().Granted();  
        }

        public override void PostInitialize()
        {
            
        }
    }
}
