using Blocks.Framework.License;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseGenernate
{
    class Program
    {
        static void Main(string[] args)
        {
            var machineInfo = new WindowMachineInfo();
            if (args != null && args.Length > 0)
                machineInfo.GenernateLicense();

            else
                machineInfo.GenernateLicenseWithRegister();
        }
    }
}
