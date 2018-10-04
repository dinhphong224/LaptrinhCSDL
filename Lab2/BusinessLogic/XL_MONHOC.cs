using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace BusinessLogic
{
    public class XL_MONHOC : XL_BANG
    {
        
            public XL_MONHOC() : base("MONHOC") { }
            public XL_MONHOC(string chuoiSQL) : base("MONHOC", chuoiSQL) { }
        
    }
}
