using System.Collections.Generic;
using Blocks.Framework.Services.DataTransfer;
using Castle.Components.DictionaryAdapter;

namespace Blocks.Framework.Data.Paging
{
    public class Group : IDataTransferObject
    {
        private string _groupOp;
        public string groupOp {
            set { _groupOp = value; }
            get { return opend[_groupOp]; }
        }
        public static readonly  Dictionary<string,string> opend = new Dictionary<string, string>()
        {
            
            { "AND" ,"&&"},{ "OR","||"},{"lt","<"},{"le","<="},{"gt",">"},{"ge",">="},{"bw","^"},{"bn","!^"},{"in","="},{"ni","!="},{"ew","|"},{"en","!@"},{"cn","~"},{"nc","!~"},{"nu","#"},{"nn","!#"},{ "bt","..."}
        }; 
        public List<Rule> rules { set; get; } = new List<Rule>();

        public List<Group> groups { set; get; } = new List<Group>();
    }
    
    public class Filters : IDataTransferObject
    {
        public string groupOp { set; get; }
        
        public List<Rule> rules { set; get; } = new List<Rule>();
        
      
    }


    public class Rule : IDataTransferObject
    {
        public static readonly  Dictionary<string,string> opend = new Dictionary<string, string>()
        {
//            { "eq" ,"=="},{ "ne","!"},{"lt","<"},{"le","<="},{"gt",">"},{"ge",">="},{"bw","^"},{"bn","!^"},{"in","="},{"ni","!="},{"ew","|"},{"en","!@"},{"cn","~"},{"nc","!~"},{"nu","#"},{"nn","!#"},{ "bt","..."}

            { "eq" ,"{0}=={1}"},{ "ne","{0}!={1}"},{"lt","{0}<{1}"},{"le","{0}<={1}"},{"gt","{0}>{1}"},{"ge","{0}>={1}"},{"bw","{0}.StartsWith{1}"},{"bn","!{0}.StartsWith{1}"},{"in","="},{"ni","!="},{"ew","{0}.EndsWith{1}"},{"en","!{0}.EndsWith{1}"},{"cn","{0}.Contains{1}"},{"nc","!{0}.Contains{1}"},{"nu","{0} == null"},{"nn","{0} != null"},{ "bt","..."}
        }; 
        public string field { get; set; }
        
        public string op { get; set; }

        public string data { get; set; }
    }
}