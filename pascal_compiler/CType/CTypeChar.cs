using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pascal_compiler
{
    class CTypeChar : CType
    {
        public CTypeChar() { }
        public override bool isDerivedTo(CType type)
        {
            return type is CTypeChar;
        }
    }
}
