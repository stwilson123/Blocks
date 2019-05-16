using Blocks.Framework.ApplicationServices.Controller.Attributes;
using Blocks.Framework.Exceptions;
using Blocks.Framework.Localization;
using Blocks.Framework.Security.Authorization.Permission.Attributes;
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

        public Localizer L { get; set; }
        [BlocksActionName("Index")]
        [BlocksAuthorize("index")]
        public ActionResult Index()
        {
            throw new BlocksBussnessException("101", L("123123"), null);
            return View();
        }
        [BlocksActionName("PDA")]
        public ActionResult PDA()
        {
            return View();
        }


        [BlocksActionName("Add")]
        public ActionResult Add()
        {
            return PartialView();
        }
        
        
        public ActionResult JsonResult()
        {
            return Json(new { aa = 1 });
        }
    }
}