using System.Collections.Generic;
using System.Linq;
using Blocks.Framework.Localization;
using Blocks.Framework.Security.Authorization.Permission;
using Blocks.Framework.Types;
using Microsoft.AspNetCore.Routing;

namespace Blocks.Framework.Navigation.Builder
{
    public class NavigationItemBuilder
    {
        protected NavigationItemDefinition _item;


        public NavigationItemBuilder()
        {
            _item = new NavigationItemDefinition();
        }

        public NavigationItemBuilder Name(string name)
        {
            _item.Name = name;
            return this;
        }


        public NavigationItemBuilder DisplayName(ILocalizableString displayName)
        {
            _item.DisplayName = displayName;
            return this;
        }


        public NavigationItemDefinition Build()
        {
            //     _item.Items = base.Build();
            Check.NotNullOrEmpty(_item.Name, "The name of navigation item can't be null or empty ");
            Check.NotNull(_item.DisplayName, "The displayName of navigation item can't be null ");
            Check.NotNull(_item.RouteValues, "The RouteValues of navigation item can't be null ");
           

            return _item;
        }

        NavigationItemBuilder Action(RouteValueDictionary values)
        {
            return values != null
                ? Action(values["action"] as string, values["controller"] as string, values)
                : Action(null, null, new RouteValueDictionary());
        }

        NavigationItemBuilder Action(string actionName)
        {
            return Action(actionName, null, new RouteValueDictionary());
        }

        NavigationItemBuilder Action(string actionName, string controllerName)
        {
            return Action(actionName, controllerName, new RouteValueDictionary());
        }

        NavigationItemBuilder Action(string actionName, string controllerName, object values)
        {
            return Action(actionName, controllerName, new RouteValueDictionary(values));
        }

        public NavigationItemBuilder Action(string actionName, string controllerName, string areaName)
        {
            return Action(actionName, controllerName, new RouteValueDictionary(new {area = areaName}));
        }

        NavigationItemBuilder Action(string actionName, string controllerName, RouteValueDictionary values)
        {
            _item.RouteValues = new RouteValueDictionary(values);
            if (!string.IsNullOrEmpty(actionName))
                _item.RouteValues["action"] = actionName;
            if (!string.IsNullOrEmpty(controllerName))
                _item.RouteValues["controller"] = controllerName;
            return this;
        }

        public NavigationItemBuilder HasPermissions(params string[] permissionName)
        {
            _item.HasPermissions = permissionName?.Select(p => Permission.Create(p,_item.Name,new LocalizableString(_item.DisplayName.SourceName, p) )).ToArray();
            return this;
        }
    }

}