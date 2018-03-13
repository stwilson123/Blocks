﻿using System;
using Blocks.BussnessApplicationModule.TestAppService.DTO;
using Blocks.BussnessDomainModule;
using Blocks.Framework.ApplicationServices;
using Blocks.Framework.Data.Paging;

namespace Blocks.BussnessApplicationModule.TestAppService
{
    public class MasterDataAppService : AppService,IMasterDataAppService
    {
        public MasterDataAppService(TestDomain testDomain)
        {
            this.testDomain = testDomain;
        }

        private TestDomain testDomain { get; set; }
        
        public  PageList<PageResult>  GetPageList(SearchModel a)
        {
            
            return testDomain.GetPageList(a);
            //return testDomain.GetValue(a);
        }

        public string Add(string id)
        {
            return testDomain.AddValue(Guid.NewGuid().ToString());
        }
    }
}