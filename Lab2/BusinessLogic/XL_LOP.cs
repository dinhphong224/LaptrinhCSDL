using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace BusinessLogic
{
    public class XL_LOP : XL_BANG
    {
        public XL_LOP() : base("LOP") { }
        public XL_LOP(string chuoiSQL) : base("LOP", chuoiSQL) { }
    }
}
