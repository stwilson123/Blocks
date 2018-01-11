using System.Web.Mvc;
using Blocks.Framework.Web.Mvc.Controllers;

namespace Blocks.BussnessWebModule.Controllers
{
    public class TestsController : BlocksWebMvcController
    {
        [HttpGet]
        public ActionResult Test()
        {
            return Json("123123213",JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            ViewBag.Index = "123123123aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            return View();
        }
        
        public ActionResult Index2()
        {
            ViewBag.Index = "123123123aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            return View();
        }
        
        public ActionResult TranditionLayoutTest()
        {
            ViewBag.Index = "123123123aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            return View();
        }
    }
}
