using System;
using System.Web;
using Blocks.Framework.Web.Mvc.UI.Resources;

namespace Blocks.Framework.Web.Mvc {
    /// <summary>
    /// This interface ensures all base view pages implement the 
    /// same set of additional members
    /// </summary>
    public interface IBlocksViewPage {
//        Localizer T { get; }
        ScriptRegister Script { get;  }
        ResourceRegister Style { get; }
        dynamic Display { get; }
        dynamic Layout1 { get; }
//        IHtmlString DisplayChildren(object shape);
//        WorkContext WorkContext { get; }
        //IDisposable Capture(Action<IHtmlString> callback);

        void RegisterLink(LinkEntry link);
        void SetMeta(string name, string content, string httpEquiv, string charset);
        void SetMeta(MetaEntry meta);
        void AppendMeta(string name, string content, string contentSeparator);
        void AppendMeta(MetaEntry meta, string contentSeparator);
        
//        bool HasText(object thing);
//        OrchardTagBuilder Tag(dynamic shape, string tagName);
    }
}
