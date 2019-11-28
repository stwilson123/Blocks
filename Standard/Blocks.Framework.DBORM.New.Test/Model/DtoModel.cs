using System.Collections;
using System.Collections.Generic;

namespace EntityFramework.Test.Model
{
    public class DtoModel
    {
        public string Id { set ; get ; }
        public decimal COLNUMINT { set; get; }
        public ICollection<DtoModel3> dtoModel3s { set; get; }

    }

    public class DtoModel3
    {
        public string Id { get; set; }
        public string CREATER { get; set; }
    }
}