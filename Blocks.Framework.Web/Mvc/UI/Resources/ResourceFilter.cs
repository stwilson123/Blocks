 

using System.Web.Mvc;
using Abp.Dependency;
using Blocks.Framework.Web.Mvc.UI.Extensions;
using IFilterProvider = Blocks.Framework.Web.Mvc.Filters.IFilterProvider;

namespace Blocks.Framework.Web.Mvc.UI.Resources {
    public class ResourceFilterProvider : IFilterProvider, IResultFilter {
        private readonly IIocManager _iocManager;
        private readonly dynamic _shapeFactory;

        public ResourceFilterProvider(
            IIocManager workContextAccessor){
           // IShapeFactory shapeFactory) {
            _iocManager = workContextAccessor;
           // _shapeFactory = shapeFactory;
        }

        public void OnResultExecuting(ResultExecutingContext filterContext) {
            // should only run on a full view rendering result
            if (!(filterContext.Result is ViewResult))
                return;

           
//            var ctx = _workContextAccessor.GetContext();
//            var head = ctx.Layout.Head;
//            var tail = ctx.Layout.Tail;
//            head.Add(_shapeFactory.Metas());
//            head.Add(_shapeFactory.HeadLinks());
//            head.Add(_shapeFactory.StylesheetLinks());
//            head.Add(_shapeFactory.HeadScripts());
//            tail.Add(_shapeFactory.FootScripts());
        }

        public void OnResultExecuted(ResultExecutedContext filterContext) {
            
            if (!(filterContext.Result is ViewResult))
                return;

            IResourceManager resourceManager = ViewBagExtensions.GetResourceManager(filterContext.HttpContext);
            if (resourceManager != null)
            {

                var a = resourceManager.BuildRequiredResources("script");
                resourceManager.GetRegisteredHeadScripts();
                resourceManager.GetRegisteredFootScripts();
                resourceManager.GetRegisteredLinks();
                resourceManager.GetRegisteredMetas();


                // resourceManager.
            }
        }
    }
}