﻿using System;
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