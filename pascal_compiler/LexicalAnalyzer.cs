using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pascal_compiler
{
    public enum TokenType
    {
        Operation,
        Identifier,
        Value
    }
    internal class LexicalAnalyzer
    {
        static public Dictionary<string, int> keyWords = new Dictionary<string, int>
    {
        {"do",1},
        {"if",2},
        {"or",3},
        {"and",4},
        {"div",5},
        {"end",6},
        {"mod",7},
        {"not",8},
        {"var",9},
        {"xor",10},
        {"char",11},
        {"else",12},
        {"real",13},
        {"then",14},
        {"type",15},
        {"begin",16},
        {"while",17},
        {"boolean",18},
        {"integer",19},
        {"program",20},
        {"string", 40},
        {",",21},
        {";",22},
        {".",23},
        {":",24},
        {"+",25},
        {"-",26},
        {"*",27},
        {"/",28},
        {"(",29},
        {")",30},
        {"{",31},
        {"}",32},
        {":=",33},
        {"<",34},
        {">",35},
        {"=",36},
        {"<=",37},
        {">=",38},
        {"<>",39},
        {"writeln",40},
            {"[",39},
        {"]",40}
    };


        IO_module IO;
        public LexicalAnalyzer(IO_module IO)
        {
            this.IO = IO;
            // IO.GetNext();

        }



        public CToken NextCToken()
        {
            CToken curToken;
            string s = IO.GetLetter();
            int value;
            if (keyWords.TryGetValue(s, out value))
            {
                curToken = new CTokenOperation(TokenType.Operation, keyWords[s]); return curToken;
            }
            else
                 if (s[0] >= 'A' && s[0] <= 'Z' || s[0] >= 'a' && s[0] <= 'z')
            {
                string name = "";
                int i = 0;
                while (i < s.Length && (s[i] >= 'A' && s[i] <= 'Z' || s[i] >= 'a' && s[i] <= 'z' || s[i] >= '0' && s[i] <= '9'))
                {
                    name += s[i].ToString();
                    ++i;
                }
                name = name.ToLower();
                if (name == "false" || name == "true")
                {
                    curToken = new CTokenValue(TokenType.Value, new CBooleanVariant(name), new CTypeBoolean());
                    return curToken;

                }
                else if (keyWords.ContainsKey(name))
                    curToken = new CTokenOperation(TokenType.Operation, keyWords[name]);

                else
                    curToken = new CTokenIdentifier(TokenType.Identifier, name);
                return curToken;

            }
            else if (s[0] >= '0' && s[0] <= '9')
            {
                string number = "";
                int i = 0;
                while (i < s.Length && s[i] >= '0' && s[i] <= '9')
                {
                    number += s[i].ToString();
                    ++i;
                }
                if (i < s.Length && s[i] == '.')
                {
                    number += s[i].ToString();
                    ++i;
                    while (i < s.Length)
                    {
                        if (s[i] >= '0' && s[i] <= '9')
                            number += s[i].ToString();
                        else IO.errorsInLine.Add(new Error(IO.curPosition + 1, 5));
                        ++i;
                    }
                    number = number.Replace('.', ',');
                    curToken = new CTokenValue(TokenType.Value, new CRealVariant(number, IO), new CTypeReal());
                }
                else
                    curToken = new CTokenValue(TokenType.Value, new CIntVariant(number, IO), new CTypeInt());
              
            }
            else if (s[0] == '\'' && s.Length <= 3 && s[s.Length-1] == '\'')
            
                curToken = new CTokenValue(TokenType.Value, new CCharVariant(s), new CTypeChar());
            else if (s[0] == '\"' && s[s.Length - 1] == '\"')

                curToken = new CTokenValue(TokenType.Value, new CStringVariant(s, IO), new CTypeString());

            else if (s == "\0")
            

                curToken = null;


                
            


            else
            {
                IO.errorsInLine.Add(new Error(IO.curPosition + 1, 5, s));

                 curToken = new CToken(TokenType.Identifier);
                
                
               
            }
            return curToken;
        }

    }
}

