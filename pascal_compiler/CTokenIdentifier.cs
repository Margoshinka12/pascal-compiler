using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pascal_compiler
{
    class CTokenIdentifier : CToken
    {
        public string name;
        public CType type;
        public int offset;
        public CTokenIdentifier(TokenType tt, string name) : base(tt)
        {
            this.name = name;
        }
        public CTokenIdentifier(TokenType tt, string name, CType type, int offset) : base(tt)
        {
            this.name = name;
            this.type = type;
            this.offset = offset;
        }
        public override void Show()
        {
            Console.WriteLine(name + "{" + tt + "} ");
        }
    }
}
