using System.Web.Mvc;

namespace Blocks.Web.Controllers
{
    public class AboutController : BlocksControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}