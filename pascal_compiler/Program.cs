using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace pascal_compiler {
	class Program
	{
		//struct Textposition
		//{
		//	uint linenumber; // номер строки
		//	uint charnumber; // номер позиции в строке
		//};

		//struct Error { //таблица ошибок текущей строки
		//	Textposition errorposition;
		//	uint errorcode;

		//};

		////const int ERRMAX = 1e5; // наибольшее количество ошибок в строке
		//static char ch; // текущая литера
		//static short ErrInx; //количество обнаруженных ошибок в текущей строке
		//const int MAXLINE = 100; // максимальная длина строки
		//static Textposition positionNow;
		//char line[MAXLINE]; //буфер ввода-вывода
		//short errorOverFlow; // true, если превышает ERRMAX
		//short lastInLine; // количество литер в текущей строке
		////void nextch()
		////{
		////	if (positionNow.charnumber == lastInLine)
		////	{

		////	}
		////}
		////void error(unsigned errorcode, Textposition position)
		////{

		////}
		static void Main(string[] args)
		{
			//List<Error> errors;
			string filename;
			
			filename = Console.ReadLine();
			IO_module module = new IO_module(filename);
			string s = "";
            while (true)
            {
				s = module.GetNext();
				if (s != "") Console.WriteLine(s);
				else break;
            }
			

		}
	}
}