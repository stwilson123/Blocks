using System.Web.Http;
using Abp.WebApi.Controllers;

namespace Blocks.WebAPIModules.API.Controllers
{
    public class AccountTestController : AbpApiController
    {
        
        [HttpGet]
        public string Get111(string id)
        {
            return "hellp";

        }
    }
}