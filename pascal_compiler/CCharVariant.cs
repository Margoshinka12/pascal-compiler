using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pascal_compiler
{
    class CCharVariant : CVariant
    {
        public char value;
        public CCharVariant(string value)
        {
            this.value = value[0];
        }
        public override void Show()
        {
            Console.Write(value + "{" + "Char.");
        }
    }
}
