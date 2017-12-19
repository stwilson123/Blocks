using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blocks.Framework.Web.Mvc;
using Blocks.BussenssWebModule.Models;
using Blocks.Framework.Web.Mvc.Controllers;

namespace Blocks.BussenssWebModule.Controllers
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
    }
}
