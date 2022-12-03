using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace pascal_compiler {
	class Program
	{
		
		static void Main(string[] args)
		{
			
			string filename;
			
			filename = Console.ReadLine();
			IO_module module = new IO_module(filename);
			LexicalAnalyzer la = new LexicalAnalyzer(module);
			//CToken token;
			//do
			//{
			//	token = la.NextCToken();
			//	if (token != null) token.Show();
			//}
			//while (token != null);
			SyntaxAnalyzer sa = new SyntaxAnalyzer(la);
			sa.Program();
			/*string s = ""; s = module.GetLetter();
			while (s != "\0")
            {
                Console.WriteLine(s);
				s = module.GetLetter();
            }*/


		}
	}
}