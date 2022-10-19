using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pascal_compiler
{
     class IO_module
    {
        internal string curLine;
        internal  int curPosition;
        internal char curLetter;
        private StreamReader inputFile;
        private StreamWriter outputFile;
        internal  List<Error> errorsInLine;
        private string filename;
        public IO_module(string filename)
        {
           


            inputFile = new StreamReader(filename);
            outputFile = new StreamWriter("output.txt");
            curPosition = -1;
            curLine = "";
            errorsInLine = new List<Error>();
            GetNext();
        }
        public void GetNext()
        {

            if ( curPosition == curLine.Length - 1)
            {
                if (errorsInLine != null)
                {
                    foreach (Error e in errorsInLine)
                        if (e.position < 0)
                            e.InputError(outputFile);
                }
                outputFile.WriteLine(curLine);
                curLine = inputFile.ReadLine();
                curPosition = -1;

                if (errorsInLine != null)
                {
                    foreach (Error e in errorsInLine)
                        if (e.position >= 0)
                            e.InputError(outputFile);
                    errorsInLine.Clear();
                }
                curLetter = '\0';
            }
            else 
            {
                curPosition++;
                curLetter = curLine[curPosition];
            }
            if (curLine == null)
            {
                
                inputFile.Close();
                outputFile.Close();
            }
        }



        internal string GetLetter()
        {
           
            string str = "";
            while (curLine != null) {
                switch (curLetter)
                {
                    case ';':
                   
                    case ')':
                    case '{':
                    case '}':
                    case '[':
                    case ']':
                    case '(':
                    case '.':
                    case ',':
                    case '/':
                    case '*':
                    case '+':
                    case '-':
                    case '=':

                        if (str != "")
                        
                            return str;
                        
                        str = "" + curLetter;
                        GetNext();
                        return str;


                   
                        
                    
                    case '\"':

                        if (str != "")
                        
                           return str;
                        
                        str = "" + curLetter; GetNext();
                        while (curLine != null && curLetter != '\"')
                        {
                            str += curLetter; GetNext();
                        }
                        if (curLine != null ) { str += curLetter; GetNext(); return str; }
                         
                            break;
                    case '\'':

                        if (str != "")
                        
                           return str;
                        
                        str = "" + curLetter; GetNext();
                        while (curLine != null && curLetter != '\'')
                        {
                            str += curLetter; GetNext();
                        }
                        if (curLine != null ) { str += curLetter; GetNext(); return str; }
                        

                            break;
                    case ':':

                        if (str != "")
                        
                            return str;
                        
                        GetNext();
                        if (curLine != null && curLetter == '=')
                        {
                            GetNext();
                            return ":=";
                        }

                        else
                            return ":";




                    case '<':
                        if (str != "")   return str; 
                        GetNext();
                        if (curLine != null && curLetter == '=')
                        {
                            GetNext();
                            return "<=";
                        }

                        else if (curLine != null && curLetter == '>')
                        {
                            GetNext();
                            return "<>";
                        }
                        else

                            return "<";




                    case '>':
                        if (str != "")
                        
                             return str;
                        
                        GetNext();
                        if (curLine != null && curLetter == '=')
                        {
                            GetNext();
                            return ">=";
                        }

                        else

                            return ">";




                    case ' ':
                    case '\0':
                    case '\t':
                    case '\n':
                        if (str != "")
                        {
                            GetNext(); return str;
                        }
                        GetNext();



                        break;
                    default:
                        str += curLetter;
                        GetNext();
                        break;

                }
                
            }
            if (curLine == null && str != "") return str;
            return curLetter.ToString();


        }
        
       
    }
}
