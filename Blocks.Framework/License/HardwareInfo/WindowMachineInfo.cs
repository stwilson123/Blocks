using Blocks.Framework.License.HardwareInfo;
using Blocks.Framework.Localization;
using Blocks.Framework.Tools;
using Blocks.Framework.Tools.Json;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Text;

namespace Blocks.Framework.License
{
    public class WindowMachineInfo : IMachineInfo
    {

        public WindowMachineInfo()
        {

        }

        /// <summary>  
        /// 获取主板序列号  
        /// </summary>  
        /// <returns></returns>  
        public string GetBIOSSerialNumber()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_BIOS");
                string sBIOSSerialNumber = "";
                foreach (ManagementObject mo in searcher.Get())
                {
                    sBIOSSerialNumber = mo["SerialNumber"].ToString().Trim();
                }
                return sBIOSSerialNumber;
            }
            catch
            {
                return "";
            }
        }

        public ProductInfo GetLicenseInfo()
        {
            var licenseFilePath = Path.Combine(AppContext.BaseDirectory, @"License");
            if (!File.Exists(licenseFilePath))
            {
                throw new LicenseException(StringLocal.Format("License file not found or no license."));
            }
            ProductInfo productInfo;
            try
            {
                var fileInfo = File.ReadAllText(licenseFilePath);
                productInfo = JsonHelper.DeserializeObject<ProductInfo>(CryptTools.Decrypt(fileInfo, LicenseManager.Secret));
            }
            catch (Exception)
            {
                throw new LicenseException(StringLocal.Format("License file not found or no license."));
            }

            if (string.IsNullOrEmpty(productInfo.MainBoardSerialNumber))
                throw new LicenseException(StringLocal.Format("Main board serial number is null or empty."));

            return productInfo;
        }

        public void GenernateLicense()
        {
            var licenseFilePath = Path.Combine(AppContext.BaseDirectory, @"License");

            var productInfo = new ProductInfo() {
                MainBoardSerialNumber = this.GetBIOSSerialNumber()
            };


            File.WriteAllText(licenseFilePath, CryptTools.Encrypt(JsonHelper.SerializeObject(productInfo), LicenseManager.Secret));

        }

        public void GenernateLicenseWithRegister()
        {
            var registerFilePath = Path.Combine(AppContext.BaseDirectory, @"Register");

            if (!File.Exists(registerFilePath))
            {
                throw new LicenseException(StringLocal.Format("Register file not found."));
            }
            ProductInfo productInfo;
            try
            {
                var fileInfo = File.ReadAllText(registerFilePath);
                productInfo = JsonHelper.DeserializeObject<ProductInfo>(CryptTools.Decrypt(fileInfo, LicenseManager.SecretRegister));
            }
            catch (Exception)
            {
                throw new LicenseException(StringLocal.Format("License file not found or no license."));
            }
            CryptTools.Encrypt(JsonHelper.SerializeObject(productInfo), LicenseManager.SecretRegister);
            var licenseFilePath = Path.Combine(AppContext.BaseDirectory, @"License");
            File.WriteAllText(licenseFilePath, CryptTools.Encrypt(JsonHelper.SerializeObject(productInfo), LicenseManager.Secret));

        }
        public void GenernateRegister()
        {
            var licenseFilePath = Path.Combine(AppContext.BaseDirectory, @"Register");

            var productInfo = new ProductInfo()
            {
                MainBoardSerialNumber = this.GetBIOSSerialNumber()
            };

            File.WriteAllText(licenseFilePath, CryptTools.Encrypt(JsonHelper.SerializeObject(productInfo), LicenseManager.SecretRegister));
        }
    }


}
