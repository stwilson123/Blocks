using System.Web.Http;
using Abp.WebApi.Controllers;

namespace Blocks.WebAPIModules.API.Controllers
{
    public class AccountTestController : AbpApiController
    {
        [HttpGet]
        public string Authenticate(string loginModel)
        {
            return "hellp";

        }
    }
}