using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pascal_compiler
{
    class CToken
    {
        public TokenType tt;
        public CToken(TokenType tt)
        {
            this.tt = tt;
        }
        public virtual void Show()
        {
            Console.Write("");
        }
    }
}
