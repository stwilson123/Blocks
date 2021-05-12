using Blocks.Framework.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Text;

namespace Blocks.Framework.License
{
    public class WindowMachineInfo//: IMachineInfo
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
                throw;
                return "";
            }
        }
        public void GenernateRegister()
        {
            var licenseFilePath = Path.Combine(AppContext.BaseDirectory, @"Register");

            var productInfo = new ProductInfo()
            {
                MainBoardSerialNumber = this.GetBIOSSerialNumber()
            };

            if (string.IsNullOrWhiteSpace(productInfo.MainBoardSerialNumber))
                throw new Exception("Open application with administrators");

            File.WriteAllText(licenseFilePath, CryptTools.Encrypt(JsonConvert.SerializeObject(productInfo), "BlocksRegister"));
        }
    }


}
