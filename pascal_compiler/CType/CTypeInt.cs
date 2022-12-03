using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pascal_compiler
{
    class CTypeInt : CType
    {
        public CTypeInt() { }
        public override bool isDerivedTo(CType type)
        {
            return type is CTypeReal || type is CTypeInt;
        }
    }
}
