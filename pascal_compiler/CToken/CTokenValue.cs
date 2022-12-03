using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pascal_compiler
{
    class CTokenValue : CToken
    {
        public CVariant value;
       
        public CTokenValue(TokenType tt, CVariant value) : base(tt)
        {
            this.value = value;
           
        }
        public override void Show()
        {
            value.Show();
            Console.WriteLine(tt + "} ");
        }
    }
}
