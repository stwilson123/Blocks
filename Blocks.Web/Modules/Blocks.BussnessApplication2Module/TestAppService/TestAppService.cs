using System;
using System.Collections.Generic;
using System.Linq;
using Blocks.Framework.ApplicationServices;

namespace Blocks.BussnessApplication2Module.TestAppService2
{
    public class TestAppService : AppService,ITestAppService
    {
        private Framework.Logging.ILog log;

        public TestAppService(BussnessRespository2Module.ITestRepository testDomain, Framework.Utility.Encryption.EncryptionUtility encryptionUtility, Framework.Logging.ILog log)
        {
            this.testDomain = testDomain;
            this.log = log;
        }

        private BussnessRespository2Module.ITestRepository testDomain { get; set; }

        public string GetValue(string a)
        {
            log.Logger(new Framework.Logging.LogModel()
            {
                Message = "123123"
            });
           // encryptionUtility.Hash("123123123");
            return testDomain.FirstOrDefault(t => t.Id == "")?.Id;
        }


        public string Add(string id)
        {
            return "";
            //  return testDomain.AddValue(Guid.NewGuid().ToString());
        }

       

      
    }
}