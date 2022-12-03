using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pascal_compiler
{
    class CIntVariant : CVariant
    {
        public int? value;
        public CIntVariant(string value, IO_module IO)
        {
            try
            {
                this.value = Convert.ToInt32(value);
            }
            catch
            {
                IO.errorsInLine.Add(new Error(IO.curPosition + 1 - value.Length, 7));
            }
        }
        public override void Show()
        {
            Console.Write(value + "{" + "Integer.");
        }
    }
}
