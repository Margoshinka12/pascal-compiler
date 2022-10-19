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
        public CType type;
        public CTokenValue(TokenType tt, CVariant value, CType type) : base(tt)
        {
            this.value = value;
            this.type = type;
        }
        public override void Show()
        {
            value.Show();
            Console.WriteLine(tt + "} ");
        }
    }
}
