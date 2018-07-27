using System.Web.Mvc;
using Blocks.Framework.ApplicationServices.Controller.Attributes;
using Blocks.Framework.Web.Mvc.Controllers;

namespace Blocks.BussnessWebModule.Controllers
{
    public class TestsController : BlocksWebMvcController
    {
        [HttpGet]
        [BlocksActionName("Test")]
        public ActionResult Test()
        {
            return Json("123123213",JsonRequestBehavior.AllowGet);
        }

        [BlocksActionName("Index")]
        public ActionResult Index()
        {
            ViewBag.Index = "123123123aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            return View();
        }

        [BlocksActionName("Index2")]
        public ActionResult Index2()
        {
            ViewBag.Index = "123123123aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            return View();
        }

        [BlocksActionName("TranditionLayoutTest")]
        public ActionResult TranditionLayoutTest()
        {
            ViewBag.Index = "123123123aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            return View();
        }

        [BlocksActionName("TranditionLayoutTestNew")]
        public ActionResult TranditionLayoutTestNew()
        {
            ViewBag.Index = "123123123aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            return View();
        }
    }
}
