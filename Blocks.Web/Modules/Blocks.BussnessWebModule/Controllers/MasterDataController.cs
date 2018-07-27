using Blocks.Framework.ApplicationServices.Controller.Attributes;
using Blocks.Framework.Web.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blocks.BussnessWebModule.Controllers
{
    public class MasterDataController : BlocksWebMvcController
    {
        [BlocksActionName("Index")]
        public ActionResult Index()
        {
            return View();
        }


        [BlocksActionName("Add")]
        public ActionResult Add()
        {
            return PartialView();
        }
    }
}