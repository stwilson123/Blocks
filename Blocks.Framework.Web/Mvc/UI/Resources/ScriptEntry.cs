using System.Collections.Generic;
using System.Web.Mvc;

namespace Blocks.Framework.Web.Mvc.UI.Resources
{
    public class ScriptEntry
    {
        public ScriptEntry()
        {

        }
        /// <summary>
        /// alias name
        /// </summary>
        public string Name { get; set; }

        public IList<string> Dependencies {get;set;}

        public bool IsAMD { get; set; } = false;
        private readonly TagBuilder _builder = new TagBuilder("script");
        public string Condition { get; set; }
        public string Src {
            get {
                string value;
                _builder.Attributes.TryGetValue("src", out value);
                return value;
            }
            set { SetAttribute("src", value); }
        }
        
        public string Type {
            get {
                string value;
                _builder.Attributes.TryGetValue("type", out value);
                return value;
            }
            set { SetAttribute("type", value); }
        }
        
        public ScriptEntry SetAttribute(string name, string value) {
            _builder.MergeAttribute(name, value, true);
            return this;
        }

        public ScriptEntry SetAttributes(IDictionary<string, string> dic)
        {
            _builder.MergeAttributes(dic, true);
            return this;
        }

        public string GetTag()
        {
            string tag = _builder.ToString();
            if (!string.IsNullOrEmpty(Condition))
            {
                return "<!--[if " + Condition + "]>" + tag + "<![endif]-->";
            }
            return tag;
        }

    }
}