using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pascal_compiler
{
    class CTokenOperation : CToken
    {
        public int code;
        public CTokenOperation(TokenType tt, int code) : base(tt)
        {
            this.code = code;
        }
        public override void Show()
        {
            string name = "";
            foreach (KeyValuePair<string, int> pair in LexicalAnalyzer.keyWords)
                if (code == pair.Value)
                {
                    name = pair.Key;
                    break;
                }
            Console.WriteLine(name + "{" + tt + "} ");
        }
    }
}