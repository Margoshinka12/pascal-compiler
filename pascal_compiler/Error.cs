using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace pascal_compiler
{
    class Error
    {
        static public Dictionary<int, string> errors = new Dictionary<int, string>
        {
            {1,"Отсутствует знак операции"},
            {2,"Должно идти имя"},
            {3,"Должен идти операнд"},
            {4,"Знак операции отустсвует или неверного типа"},
            {5,"В исходном тексте найден неверный символ"},
            {6,"Ошибка в вещественной константе"},
            {7,"Ошибка в целой константе"},
            {8,"Должно идти слово" },
            {9,"Недопустимый тип"},
            {10,"Повторное описание переменной"},
            {11,"Строка слишком длиннная"},
            {12,"Неописанная переменная"},
            {13,"Ожидалось выражение логического типа"},
            {14,"Несоответствие типов"},
            {15,"Неверный тип операндов"},
            {16, "Нет закрывающего знака" }
          
        };
        //public void output()
        //{
        //    Console.WriteLine(errors[code] + " code: " + code.ToString());
        //}
        public int position;
        public int code;
        private string word;
        public Error(int position, int code)
        {
            this.position = position;
            this.code = code;
            //output();
        }
        public Error(int position, int code, string word)
        {
            this.position = position;
            this.code = code;
            this.word = word;
            //output();
        }
        public void InputError(StreamWriter outputFile)
        {
            outputFile.WriteLine("Ошибка! Код: " + code);
            outputFile.WriteLine(errors[code] + " " + word);
            outputFile.WriteLine();
        }
    }
}
