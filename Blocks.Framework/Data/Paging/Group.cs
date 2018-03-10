using System.Collections.Generic;
using Blocks.Framework.Services.DataTransfer;
using Castle.Components.DictionaryAdapter;

namespace Blocks.Framework.Data.Paging
{
    public class Group : IDataTransferObject
    {
        public string groupOp { set; get; }
        
        public List<Rule> rules { set; get; } = new List<Rule>();

        public List<Group> groups { set; get; } = new List<Group>();
    }
    
    public class Filters : IDataTransferObject
    {
        public string groupOp { set; get; }
        
        public List<Rule> rules { set; get; } = new List<Rule>();
        
        public List<Group> groups { set; get; } = new List<Group>();
    }


    public class Rule : IDataTransferObject
    {
        public string field { get; set; }
        
        public string op { get; set; }

        public string data { get; set; }
    }
}