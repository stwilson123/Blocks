using Blocks.Framework.Ioc.Dependency;
using Blocks.Framework.License.HardwareInfo;
using Blocks.Framework.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Blocks.Framework.License
{
    public class LicenseManager 
    {
        private readonly IMachineInfo machineInfo;
        internal static string Secret = "Blocks";
        internal static string SecretRegister = "BlocksRegister";

        string[] ignoredProcess = new[] { "testhost", "iisexpress" };
        public LicenseManager(IMachineInfo machineInfo)
        {
            this.machineInfo = machineInfo;
        }
        public void Granted()
        {
            string processName = Path.GetFileNameWithoutExtension(Process.GetCurrentProcess().ProcessName);
            if (Debugger.IsAttached || ignoredProcess.Any(p => processName.IndexOf(p, StringComparison.OrdinalIgnoreCase) > -1))
            {
                return;
            }

            if (this.machineInfo.GetLicenseInfo().MainBoardSerialNumber != this.machineInfo.GetBIOSSerialNumber())
                throw new LicenseException(StringLocal.Format("License file not found or no license."));
        }

        internal void Genernate()
        {

        }
    }
}
