using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.License.HardwareInfo
{
    public interface IMachineInfo
    {
        ProductInfo GetLicenseInfo();
        string GetBIOSSerialNumber();

        void GenernateLicense();


    }
}
