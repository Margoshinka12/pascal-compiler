using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pascal_compiler
{
    abstract class CType
    {
        public CType() { }
        public abstract bool isDerivedTo(CType type);
    }
}

