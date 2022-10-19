using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pascal_compiler { 
    class CBooleanVariant : CVariant
    {
        public bool value;
        public CBooleanVariant(string value)
        {
            this.value = Convert.ToBoolean(value);
        }
        public override void Show()
        {
            Console.Write(value + "{" + "Boolean.");
        }
    }
}

