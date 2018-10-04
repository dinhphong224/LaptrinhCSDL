using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace BusinessLogic
{
    public class XL_KHOA :XL_BANG
    {
        public XL_KHOA() : base("KHOA") { }
        public XL_KHOA(string chuoiSQL) : base("KHOA", chuoiSQL) { }
    }
}
