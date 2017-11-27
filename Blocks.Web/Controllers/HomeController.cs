using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace Blocks.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : BlocksControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}