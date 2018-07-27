using System.Web.Mvc;
using Blocks.Core.Navigation.Services;
using Blocks.Framework.Event;
using Blocks.Framework.Security;
using Blocks.Framework.Web.Mvc.Controllers;
using Blocks.LayoutModule.ExtensionsModule.Event;
//using Blocks.LayoutModule.Extensions.Event;
using Blocks.Web.Models.Layout;

namespace Blocks.LayoutModule.Controllers
{
    public class LayoutController : BlocksWebMvcController
    {
        private readonly IUserNavigationManager _userNavigationManager;
        private IUserContext _userContext;

        public IDomainEventBus EventBus { get; set; }
        public LayoutController(IUserNavigationManager userNavigationManager, IUserContext userContext)
        {
            _userNavigationManager = userNavigationManager;
            _userContext = userContext;
        }

        [ChildActionOnly]
        public ActionResult SideBarNav(string activeMenu)
        {
            var model =  _userNavigationManager.GetMenuAsync(activeMenu, _userContext.GetCurrentUser()).Result;
            EventBus.Trigger<MenusSortEventData>(new MenusSortEventData() {  userNavigation = model});
            return PartialView(model);
        }


        [ChildActionOnly]
        public ActionResult RightSideBar()
        {
            return PartialView();
        }
    }
}