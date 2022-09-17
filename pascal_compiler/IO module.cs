using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pascal_compiler
{
    public class IO_module
    {
        List<string> list;
        int position;
        string filename;
        public IO_module(string filename)
        {
            position = 0;
            list = new List<string>();
            this.filename = filename;
            ReadText();
        }
        public string GetNext()
        {
            if (position == list.Count) return "";
            else
            {
                string s = list[position];
                ++position;
                return s;
            }
        }

        private void WriteInList(string s)
        {
            string str = "";
            for (int i = 0; i < s.Length; i++)
            {
                switch (s[i])
                {
                    case ';':
                    case '(': case ')':
                    case '{':
                    case '}':
                    case '[': case ']':
                    case '\'':
                    case '\"':
                    case '.':
                    case ',':
                    case '/':
                    case '*':
                    case '+':
                    case '-':
                    case '=':
                        {
                            if (str != "") list.Add(str);
                            str = "" + s[i];
                            list.Add(str);
                            str = "";
                        }
                        break;
                    case ':':
                        {

                            if (str != "") list.Add(str);
                            if (i < s.Length - 1 && s[i + 1] == '=')
                            {
                                list.Add(":="); i++;
                            }
                            else {
                                list.Add(":"); 
                            }
                            str = "";
                        }
                        break;
                    case '<':
                        {
                            if (str != "") list.Add(str);
                            if (i < s.Length - 1 && s[i + 1] == '=')
                            {
                                list.Add("<="); i++;
                            }
                            else if (i < s.Length - 1 && s[i + 1] == '>')
                            {
                                list.Add("<>"); i++;
                            }
                            else
                            {
                                list.Add("<");
                            }
                            str = "";
                        }
                        break;
                    case '>':
                        {
                            if (str != "") list.Add(str);
                            if (i < s.Length - 1 && s[i + 1] == '=')
                            {
                                list.Add(">="); i++;
                            }
                            
                            else
                            {
                                list.Add(">");
                            }
                            str = "";
                        }
                        break;
                    case ' ':
                        {
                            if (str != "") list.Add(str); str = "";
                        }
                        break;
                    default:
                           str += s[i];
                        if (i == s.Length - 1 && str != "") list.Add(str);
                        break;

                        
                
                }



            }
        }
        private void ReadText()
        {
            try
            {
                using (StreamReader f = new StreamReader(filename))
                {
                    string s;
                    while ((s = f.ReadLine()) != null)  WriteInList(s);

                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Error:" + e.Message);
                return;
            }

        }
    }
}
