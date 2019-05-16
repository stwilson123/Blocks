using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abp.Localization;
using Abp.Runtime.Session;
using Blocks.Core.Navigation.Services;
using Blocks.Framework.AutoMapper;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.Event;
using Blocks.Framework.Localization;
using Blocks.Framework.Security;
using Blocks.Framework.Services;
using Blocks.Framework.Web.Api.Controllers.Dynamic;
using Blocks.Framework.Web.Mvc.Controllers;
using Blocks.Framework.Web.Web.Configuration;
using Blocks.LayoutModule.ExtensionsModule.Event;
using Blocks.LayoutModule.ViewModels;
//using Blocks.LayoutModule.Extensions.Event;

namespace Blocks.LayoutModule.Controllers
{
    public class LayoutController : BlocksWebMvcController
    {
        private readonly IUserNavigationManager _userNavigationManager;
        private IUserContext _userContext;
        private ILanguagesManager _languageManager;
        public IDomainEventBus EventBus { get; set; }
        private readonly IBlocksWebLocalizationConfiguration _webLocalizationConfiguration;
        public IClock Clock { get; set; }
        public IExtensionsWrapper extensionsWrapper { get; set; }
        public LayoutController(IUserNavigationManager userNavigationManager, IUserContext userContext, ILanguagesManager languageManager,
            IBlocksWebLocalizationConfiguration webLocalizationConfiguration)
        {
            _userNavigationManager = userNavigationManager;
            _userContext = userContext;
            _languageManager = languageManager;
            _webLocalizationConfiguration = webLocalizationConfiguration;
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


        [ChildActionOnly]
        public PartialViewResult PrivateSetting()
        {
             
            return PartialView(new UserInfo { UserAccount = _userContext.GetCurrentUser().UserAccount });
        }



        [ChildActionOnly]
        public PartialViewResult LanguageSelection()
        {
            var model = new LanguageSelectionViewModel
            {
                CurrentLanguage = new ViewModels.LanguageInfo(_languageManager.CurrentLanguage),
                Languages = _languageManager.GetLanguages().Select(l => new ViewModels.LanguageInfo(l)).ToArray()
            };

            return PartialView(model);
        }


        public virtual  ActionResult ChangeCulture(string cultureName, string returnUrl = "")
        {
            //if (!GlobalizationHelper.IsValidCultureCode(cultureName))
            //{
            //    throw new AbpException("Unknown language: " + cultureName + ". It must be a valid culture!");
            //}

            Response.Cookies.Add(
                new HttpCookie(_webLocalizationConfiguration.CookieName, cultureName)
                {
                    Expires = Clock.Now.AddYears(2),
                    Path = Request.ApplicationPath
                }
            );

            if (AbpSession.UserId.HasValue)
            {
                  SettingManager.ChangeSettingForUserAsync(
                    AbpSession.ToUserIdentifier(),
                    LocalizationSettingNames.DefaultLanguage,
                    cultureName
                ).Wait();
            }

            //if (Request.IsAjaxRequest())
            //{
            //    return Json(new AjaxResponse(), JsonRequestBehavior.AllowGet);
            //}

            if (!string.IsNullOrWhiteSpace(returnUrl) && Request.Url != null )//&&  AbpUrlHelper.IsLocalUrl(Request.Url, returnUrl))
            {
                return Redirect(returnUrl);
            }

            return Redirect(Request.ApplicationPath);
        }
    }
}