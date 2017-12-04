using System.Web.Http;
using Blocks.Framework.Web.Api;
using Blocks.Framework.Web.Api.Controllers;

namespace Blocks.BussenssWebModule.API.Controllers
{
    public class AccountTestController : BlockWebApiController
    {
        
        [HttpGet]
        public string Get111(string id)
        {
            return "hellp";

        }
    }
}