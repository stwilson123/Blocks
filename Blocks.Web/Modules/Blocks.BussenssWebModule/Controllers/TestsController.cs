using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blocks.Framework.Web.Mvc;

namespace Blocks.BussenssWebModule.Controllers
{
    public class TestsController : BlockWebController
    {
        [HttpGet]
        public ActionResult Test()
        {
            return Json("123123213",JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
