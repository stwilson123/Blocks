using Blocks.Framework.Services.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blocks.Framework.DBORM.New.Test.Model
{
	public class ProcessPageResult : IDataTransferObject
	{
		public string Id { set; get; }
		public string OP_NO { set; get; }
		public string OP_NAME { set; get; }
		public string STOREROOM_CODE { set; get; }
		public string STORESITE_CODE { set; get; }
		public long IS_CHECK { set; get; }
		public long IS_LABORATORY_CHECK { set; get; }
		public long IS_PRODUCE_CHECK { set; get; }
		public string CODE_NO { set; get; }
		public long ACTIVITY { set; get; }
	}
}
