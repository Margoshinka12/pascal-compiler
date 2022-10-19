using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pascal_compiler
{
    class CStringVariant : CVariant
    {
        public string value;
        public CStringVariant(string value, IO_module IO)
        {
            if (value.Length > 255)
               IO.errorsInLine.Add(new Error(IO.curPosition + 1 - value.Length, 11));
            else
                this.value = value;
        }
        public override void Show()
        {
            Console.Write(value + "{" + "String.");
        }
    }
}
