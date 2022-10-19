using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pascal_compiler
{
    class CRealVariant : CVariant
    {
        public double? value;
        public CRealVariant(string value, IO_module IO)
        {
            try
            {
                this.value = Convert.ToDouble(value);
            }
            catch
            {
                IO.errorsInLine.Add(new Error(IO.curPosition + 1 - value.Length, 6));
            }
        }
        public override void Show()
        {
            Console.Write(value + "{" + "Real.");
        }
    }
}

