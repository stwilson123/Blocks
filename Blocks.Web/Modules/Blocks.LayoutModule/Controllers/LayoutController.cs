using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Blocks.Framework.Web.Mvc.Controllers;

namespace Blocks.LayoutModule.Controllers
{
    public class LayoutController : BlocksWebMvcController 
    {
        public ActionResult GetTestPartialView()
        {
            return PartialView(@"~\Template\_LayoutPartialViewFirst");
        }
    }
}