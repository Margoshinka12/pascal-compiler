using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pascal_compiler
{
    class CTypeString : CType
    {
        public CTypeString() { }
        public override bool isDerivedTo(CType type)
        {
            return type is CTypeString;
        }
    }
}
