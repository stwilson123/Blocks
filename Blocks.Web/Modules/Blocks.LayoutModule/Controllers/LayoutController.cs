using System.Web.Mvc;
using Blocks.Core.Navigation.Services;
using Blocks.Framework.AutoMapper;
using Blocks.Framework.Event;
using Blocks.Framework.Security;
using Blocks.Framework.Web.Api.Controllers.Dynamic;
using Blocks.Framework.Web.Mvc.Controllers;
using Blocks.LayoutModule.ExtensionsModule.Event;
using Blocks.LayoutModule.ViewModels;
//using Blocks.LayoutModule.Extensions.Event;

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
            var model =  _userNavigationManager.GetMenuAsync("MainMenu", _userContext.GetCurrentUser()).Result;
            EventBus.Trigger<MenusSortEventData>(new MenusSortEventData() {  userNavigation = model});
            var viewModel = new Menus(model.Name,model.Items);
            viewModel.ActiveMenuItemName = activeMenu;
            return PartialView(viewModel);
        }


        [ChildActionOnly]
        public ActionResult RightSideBar()
        {
            return PartialView();
        }
    }
}