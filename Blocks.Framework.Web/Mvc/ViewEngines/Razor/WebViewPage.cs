using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Abp.Dependency;
using Blocks.Framework.Environment.Configuration;
using Blocks.Framework.Web.Mvc.Spooling;
using Blocks.Framework.Web.Mvc.UI.Extensions;
using Blocks.Framework.Web.Mvc.UI.Resources;

namespace Blocks.Framework.Web.Mvc.ViewEngines.Razor
{
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>, IBlocksViewPage
    {
        private string[] _commonLocations;

//        private Localizer _localizer = NullLocalizer.Instance;
        private object _display;

        private object _layout;

        private IResourceManager _resourceManager;
        private ScriptRegister _scriptRegister;
        private ResourceRegister _stylesheetRegister;

//        public OrchardTagBuilder Tag(dynamic shape, string tagName) {
//            return Html.GetWorkContext().Resolve<ITagBuilderFactory>().Create(shape, tagName);
//        }
//
//        public IHtmlString DisplayChildren(dynamic shape) {
//            var writer = new HtmlStringWriter();
//            foreach (var item in shape) {
//                writer.Write(Display(item));
//            }
//            return writer;
//        }

        private string _tenantPrefix;

//        public WorkContext WorkContext { get; set; }
        public IIocManager WorkContext { get; set; }

        public IResourceManager ResourceManager
        {
            get
            { 
                if (_resourceManager != null)
                    return _resourceManager;

                _resourceManager = WorkContext.Resolve<IResourceManager>();
                
                //HtmlStringWriter w = new HtmlStringWriter();
                //w.WriteLine("1111111111111");
                //this.RenderBody().WriteTo(w);
                ViewBagExtensions.SetResourceManager(Context,_resourceManager);
                
                return _resourceManager;
            }
        }

        public string[] CommonLocations => _commonLocations ??
                                           (_commonLocations =
                                               WorkContext.Resolve<ExtensionLocations>().CommonLocations);

//        public Localizer T { 
//            get {
//                // first time used, create it
//                if(_localizer == NullLocalizer.Instance) {
//                
//                    // if the Model is a shape, get localization scopes from binding sources
//                    // e.g., Logon.cshtml in a theme, overriging Users/Logon.cshtml, needs T to 
//                    // fallback to the one in Users
//                    var shape = Model as IShape;
//                    if(shape != null && shape.Metadata.BindingSources.Count > 1) {
//                        var localizers = shape.Metadata.BindingSources.Reverse().Select(scope => LocalizationUtilities.Resolve(ViewContext, scope)).ToList();
//                        _localizer = (text, args) => { 
//                            foreach(var localizer in localizers) {
//                                var hint = localizer(text, args);
//                                // if not localized using this scope, use next scope
//                                if(hint.Text != text) {
//                                    return hint;
//                                }
//                            }
//
//                            // no localization found, return default value
//                            return new LocalizedString(text, VirtualPath, text, args);
//                        };
//                    }
//                    else {
//                        // not a shape, use the VirtualPath as scope
//                        _localizer = LocalizationUtilities.Resolve(ViewContext, VirtualPath);
//                    }
//                }
//
//                return _localizer;
//            } 
//        }

        public dynamic Display => _display;

        // review: (heskew) is it going to be a problem?
        public new dynamic Layout1 => _layout;

//        public dynamic New { get { return ShapeFactory; } }

//        private IDisplayHelperFactory _displayHelperFactory;
//        public IDisplayHelperFactory DisplayHelperFactory {
//            get {
//                return _displayHelperFactory ?? (_displayHelperFactory = WorkContext.Resolve<IDisplayHelperFactory>());
//            }
//        }
//
//        private IShapeFactory _shapeFactory;
//        public IShapeFactory ShapeFactory {
//            get {
//                return _shapeFactory ?? (_shapeFactory = WorkContext.Resolve<IShapeFactory>());
//            }
//        }
//
//        private IAuthorizer _authorizer;
//        public IAuthorizer Authorizer { 
//            get {
//                return _authorizer ?? (_authorizer = WorkContext.Resolve<IAuthorizer>());
//            }
//        }
//
//        private IContentManager _contentManager;
//        public dynamic BuildDisplay(IContent content, string displayType = "", string groupId = "") {
//            if (_contentManager == null) {
//                _contentManager = WorkContext.Resolve<IContentManager>();
//            }
//
//            return _contentManager.BuildDisplay(content, displayType, groupId);
//        }

        public ScriptRegister Script => _scriptRegister ??
                                        (_scriptRegister = new WebViewScriptRegister(this, Html.ViewDataContainer,
                                            ResourceManager));

        public ResourceRegister Style => _stylesheetRegister ??
                                         (_stylesheetRegister = new ResourceRegister(Html.ViewDataContainer,
                                             ResourceManager, "stylesheet"));

        public virtual void RegisterLink(LinkEntry link)
        {
            ResourceManager.RegisterLink(link);
        }

        public void SetMeta(string name = null, string content = null, string httpEquiv = null, string charset = null)
        {
            var metaEntry = new MetaEntry(name, content, httpEquiv, charset);
            SetMeta(metaEntry);
        }

        public virtual void SetMeta(MetaEntry meta)
        {
            ResourceManager.SetMeta(meta);
        }

        public void AppendMeta(string name, string content, string contentSeparator)
        {
            AppendMeta(new MetaEntry {Name = name, Content = content}, contentSeparator);
        }

        public virtual void AppendMeta(MetaEntry meta, string contentSeparator)
        {
            ResourceManager.AppendMeta(meta, contentSeparator);
        }

        public void RegisterImageSet(string imageSet, string style = "", int size = 16)
        {
            // hack to fake the style "alternate" for now so we don't have to change stylesheet names when this is hooked up
            // todo: (heskew) deal in shapes so we have real alternates 
            var imageSetStylesheet = !string.IsNullOrWhiteSpace(style)
                ? string.Format("{0}-{1}.css", imageSet, style)
                : string.Format("{0}.css", imageSet);
            Style.Include(imageSetStylesheet);
        }

        public override void InitHelpers()
        {
            base.InitHelpers();

            WorkContext = ViewContext.GetWorkContext();

//            _display = DisplayHelperFactory.CreateHelper(ViewContext, this);
//            _layout = WorkContext.Layout;
        }

//        public bool AuthorizedFor(Permission permission) {
//            return Authorizer.Authorize(permission);
//        }
//
//        public bool AuthorizedFor(Permission permission, IContent content) {
//            return Authorizer.Authorize(permission, content);
//        }

        public bool HasText(object thing)
        {
            return !string.IsNullOrWhiteSpace(Convert.ToString(thing));
        }

        public override string Href(string path, params object[] pathParts)
        {
            if (path.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
                || path.StartsWith("https://", StringComparison.OrdinalIgnoreCase)) return path;

            if (_tenantPrefix == null)
            {
                //  _tenantPrefix = WorkContext.Resolve<ShellSettings>().RequestUrlPrefix ?? "";
            }

            if (!string.IsNullOrEmpty(_tenantPrefix)
                && path.StartsWith("~/")
                && !CommonLocations.Any(gpp => path.StartsWith(gpp, StringComparison.OrdinalIgnoreCase))
            ) return base.Href("~/" + _tenantPrefix + path.Substring(2), pathParts);

            return base.Href(path, pathParts);
        }

//        public IDisposable Capture(Action<IHtmlString> callback) {
//            return new CaptureScope(this, callback);
//        }
//
//        public IDisposable Capture(dynamic zone, string position = null) {
//            return new CaptureScope(this, html => zone.Add(html, position));
//        }

        private class CaptureScope : IDisposable
        {
            private readonly Action<IHtmlString> _callback;
            private readonly WebPageBase _viewPage;

            public CaptureScope(WebPageBase viewPage, Action<IHtmlString> callback)
            {
                _viewPage = viewPage;
                _callback = callback;
                _viewPage.OutputStack.Push(new HtmlStringWriter());
            }

            void IDisposable.Dispose()
            {
                var writer = (HtmlStringWriter) _viewPage.OutputStack.Pop();
                _callback(writer);
            }
        }

        private class WebViewScriptRegister : ScriptRegister
        {
            private readonly WebPageBase _viewPage;

            public WebViewScriptRegister(WebPageBase viewPage, IViewDataContainer container,
                IResourceManager resourceManager)
                : base(container, resourceManager)
            {
                _viewPage = viewPage;
            }

            public override IDisposable Head()
            {
                return new CaptureScope(_viewPage, s => ResourceManager.RegisterHeadScript(s.ToString()));
            }

            public override IDisposable Foot()
            {
                return new CaptureScope(_viewPage, s => ResourceManager.RegisterFootScript(s.ToString()));
            }
        }
    }

    public abstract class WebViewPage : WebViewPage<dynamic>
    {
    }
}