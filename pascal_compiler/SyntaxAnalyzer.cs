using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace pascal_compiler
{
    internal class SyntaxAnalyzer
    {
        Dictionary<string, CType> tableOfIdentifiers = new Dictionary<string, CType>(); //описанные переменные
        List<string> ctokenIdentifierNames = new List<string>(); //неописанные переменные 
        LexicalAnalyzer la;
        CToken curToken;
        static public List<int> variableType = new List<int>
        {
            LexicalAnalyzer.keyWords["char"],
            LexicalAnalyzer.keyWords["real"],
            LexicalAnalyzer.keyWords["boolean"],
            LexicalAnalyzer.keyWords["integer"],
            LexicalAnalyzer.keyWords["string"]
        };
        Dictionary<int, CType> tableOfTypes = new Dictionary<int, CType>
        {
            {LexicalAnalyzer.keyWords["boolean"], new CTypeBoolean()},
            {LexicalAnalyzer.keyWords["char"], new CTypeChar()},
            {LexicalAnalyzer.keyWords["integer"], new CTypeInt()},
            {LexicalAnalyzer.keyWords["real"], new CTypeReal()},
            {LexicalAnalyzer.keyWords["string"], new CTypeString()}
        };
        public SyntaxAnalyzer(LexicalAnalyzer la)
        {

            this.la = la;
            GetNextToken();
        }
        public void Program()
        {
           if(! Accept(TokenType.Operation, "program")) la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 8, "program"));
            GetNextToken();
            if (! Accept(TokenType.Identifier)) la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 2));
            GetNextToken();
            if (! Accept(TokenType.Operation, ";")) la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 3));
            GetNextToken();
            Block();
            if(!Accept(TokenType.Operation, ".")) la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 3));

           
        }
        public bool Accept(TokenType type, string keyWord)
        {
            if (!(curToken is CTokenOperation) || ((CTokenOperation)curToken).code != LexicalAnalyzer.keyWords[keyWord])
            
                  return false;
            
            
            return true;
        }
        public bool Accept(TokenType type)
        {
            if (curToken.tt != type)
            {
                if (type == TokenType.Identifier)
                {
                    la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 2));

                }
                if (type == TokenType.Value)
                {
                    la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 3));

                }
                return false;
            }
            
            return true;
        }

        void GetNextToken()
        {
            try
            {
                curToken = la.NextCToken();

                if (curToken is CTokenIdentifier)
                    Console.WriteLine((curToken as CTokenIdentifier).name);
                else
                {
                    if (curToken == null) Console.WriteLine(    "null");
                    else if (curToken is CTokenOperation)
                        Console.WriteLine(LexicalAnalyzer.keyWords.FirstOrDefault(x => x.Value == (curToken as CTokenOperation).code).Key);
                    else
                        Console.WriteLine(curToken.tt);
                }
            }
            catch
            {
                
            }
        }


        void Block() // <блок>::=<раздел констант><раздел переменных><оператор>
        {
            try
            {
                ConstArea();
            }
            catch
            {
                
            }


            try
            {
                VarArea();
            }
            catch
            {
                la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 9));
            }


            try
            {
                Operator();
            }
            catch
            {
                
            }

        }

        void ConstArea() // <раздел констант>::=<пусто>|const <определение константы>;{<определение константы>;}
        {
            if (Accept(TokenType.Operation, "const"))
            {
                GetNextToken();
                ConstDeter();
                if (!Accept(TokenType.Operation, ";"))
                    la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 3));
                GetNextToken();
                while (true)
                {
                    try
                    {
                        ConstDeter();
                    }
                    catch
                    {
                        break;
                    }
                    if (!Accept(TokenType.Operation, ";"))
                        la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 3));
                    GetNextToken();
                }
            }
        }

        void ConstDeter() // <определение константы>::=<имя>=<константа>
        {
            if (curToken.tt != TokenType.Identifier)
            {
                
                throw new Exception();
            }
            GetNextToken();
            if (!Accept(TokenType.Operation, "="))
            {
                la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 3));
            }
            GetNextToken();
            Const();
        }
        void Const()  // <константа>::=<число без знака>|<знак><число без знака>|
                        //<имя константы>|<знак><имя константы>|<строка>
                        //<число без знака>::=<целое без знака>|<вещественное без знака>
                        //<целое без знака>::=<цифра>{<цифра>}
                        //<вещественное без знака>::=<целое без знака>.<цифра>{<цифра>}|< целое без знака>.<цифра>{<цифра>}E<порядок> |< целое без знака>E<порядок>
                        //<порядок>::=<целое без знака>|<знак><целое без знака>
                        //<знак>::=+|-
                        //<имя константы>::=<имя>
                        //<строка>::='<символ>{<символ>}'

        {
            if (Accept(TokenType.Operation, "-"))
            {
                GetNextToken();
                if (curToken.tt == TokenType.Value || curToken.tt == TokenType.Identifier)
                {
                    GetNextToken();
                }
                else
                {
                    la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 9));
                }
            }
            else
            {
                if (curToken.tt == TokenType.Value || curToken.tt == TokenType.Identifier)
                {
                    GetNextToken();
                }
                else
                {
                    la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 9));
                }
            }
        }

        void VarArea() //<раздел переменных>::= var <описание однотипных переменных>;{<описание однотипных переменных>;}|<пусто>
        {
            if (Accept(TokenType.Operation, "var"))
            {
                GetNextToken();
                Var();
                if (!Accept(TokenType.Operation, ";"))
                    la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 3));
                GetNextToken();
                while (true)
                {
                    try
                    {
                        Var();
                    }
                    catch
                    {
                        break;
                    }
                    if (!Accept(TokenType.Operation, ";"))
                        la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 3));
                    GetNextToken();
                }
            }
        }

        void Var() // <описание однотипных переменных>::=<имя>{,<имя>}:<тип>
        {
            if (curToken.tt != TokenType.Identifier)
                throw new Exception();

            /*имена переменных*/

            ctokenIdentifierNames.Add(((CTokenIdentifier)curToken).name);

            GetNextToken();

            while (Accept(TokenType.Operation, ","))
            {
                GetNextToken();
                if (curToken.tt != TokenType.Identifier)
                    la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 2));
                /*имена переменных*/
                ctokenIdentifierNames.Add(((CTokenIdentifier)curToken).name);

                GetNextToken();
            }

            if (!Accept(TokenType.Operation, ":"))
                la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 3));

            GetNextToken();

            if (curToken.tt != TokenType.Operation)
                la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 3));


            CType t;
            /*добавление в таблицу имен*/
            int code = ((CTokenOperation)curToken).code;
            if (code == LexicalAnalyzer.keyWords["integer"]) t = new CTypeInt();
            else if (code == LexicalAnalyzer.keyWords["real"]) t = new CTypeReal();
            else if (code == LexicalAnalyzer.keyWords["string"]) t = new CTypeString();
            else if (code == LexicalAnalyzer.keyWords["bool"]) t = new CTypeBoolean();
            else  throw new Exception();
            

            foreach (string egge in ctokenIdentifierNames)
            {
                tableOfIdentifiers.Add(egge, t);
            }
            ctokenIdentifierNames.Clear();
            GetNextToken();
        }
       
        void Operator() //<оператор>::=<простой оператор>|<сложный оператор>
        {
           
           
                if (curToken is CTokenOperation)
                {
                    int code = ((CTokenOperation)curToken).code;
                if (code == LexicalAnalyzer.keyWords["begin"])
                {
                    CompositeOperator();
                    return;
                }
                else

                    if (code == LexicalAnalyzer.keyWords["while"])
                {


                    CycleOpertor(); return;
                }
                else

                        if (code == LexicalAnalyzer.keyWords["if"])
                {


                    ChoiseOperator();
                    return;
                }
                else
                            if (code == LexicalAnalyzer.keyWords["writeln"])
                {
                    Write();
                    return;
                }


                }
              
                SimpleOperator();

        }
       
        void Write() // вывод ::= <writeln>'('<выражение>')'> ;
        {
            GetNextToken();
            if (!Accept(TokenType.Operation, "("))
                la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 3));
            GetNextToken();

            var a = Excpression();


            if (!Accept(TokenType.Operation, ")"))
                la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 16));

            GetNextToken();

           

        }
        void SimpleOperator() // <простой оператор>::=<оператор присваивания>|<пустой оператор>
        {
            try
            {
                AssigmentOperator();
            }
            catch
            {

            }
        }
        
        void AssigmentOperator() // <оператор присваивания>::=<имя>:=<выражение>
        {
            if (curToken.tt != TokenType.Identifier) {
               
                throw new Exception(); }
           

            CType left = tableOfIdentifiers[(curToken as CTokenIdentifier).name];

            GetNextToken();

            if (!Accept(TokenType.Operation, ":="))
                la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 3));

            GetNextToken();

            CType right = Excpression();

            isDerivetToAssignemt(left, right);

        }

        public CType DerivetTo(CType left, CType right)
        {

            if (left is CTypeInt && right is CTypeInt)
                return left;

            if (left is CTypeBoolean && right is CTypeBoolean)
                return left;

            if (left is CTypeString && right is CTypeString)
                return left;

            return new CTypeReal();
        }

        public void isDerivetToAssignemt(CType left, CType right)
        {

            if (left is CTypeInt && right is CTypeInt)
                return;

            if (left is CTypeBoolean && right is CTypeBoolean)
                return;

            if (left is CTypeString && right is CTypeString)
                return;
            if (left is CTypeReal && (right is CTypeInt || right is CTypeReal))
                return;

            la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 14));
            GetNextToken();
            
        }

        CType Excpression() // <выражение>::=<простое выражение>|<простое выражение><операция отношения><простое выражение>
        {
            CType left = SimpleExpression();
            if (Realat())
            {
                GetNextToken();
                CType right = SimpleExpression();
                if (!left.isDerivedTo(right))
                {
                    la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 14));
                    GetNextToken();
                }
                return new CTypeBoolean();
            }
            return left;

        }
       
        CType SimpleExpression() // <простое выражение>::=<знак><слагаемое>{<аддитивная операция><слагаемое>}
        {
            if (Accept(TokenType.Operation, "-"))
                GetNextToken();


            CType left = Term();

            while (true)
            {
                if (Add())
                {
                    GetNextToken();
                    CType right = Term();

                    if (!left.isDerivedTo(right))
                    {
                        la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 14));
                        GetNextToken();
                    }
                    left = DerivetTo(left, right);
                }
                else
                {
                    break;
                }
            }
            return left;
        }
      
        CType Term() // <слагаемое>::=<множитель>{<мультипликативная операция><множитель>}
        {
            CType left = Factor();
            while (true)
            {
                if (Mult())
                {
                    GetNextToken();
                    CType right = Factor();
                    if (!left.isDerivedTo(right))
                    {
                        la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 7));
                        GetNextToken();
                    }
                    left = DerivetTo(left, right);
                }
                else
                {
                    break;
                }
            }
            return left;
        }

        bool Mult()
        {
            if ((curToken is CTokenOperation) && ((curToken as CTokenOperation).code == LexicalAnalyzer.keyWords["*"] ||
                    (curToken as CTokenOperation).code == LexicalAnalyzer.keyWords["/"] ||
                    (curToken as CTokenOperation).code == LexicalAnalyzer.keyWords["div"] ||
                    (curToken as CTokenOperation).code == LexicalAnalyzer.keyWords["mod"] ||
                    (curToken as CTokenOperation).code == LexicalAnalyzer.keyWords["and"]))

            {
                return true;
            }
            return false;
        }


        bool Add() // <аддитивная операция>::=+|-|or		*
        {
            if (curToken.tt == TokenType.Operation)
            {
                if ((curToken as CTokenOperation).code == LexicalAnalyzer.keyWords["+"] ||
                    (curToken as CTokenOperation).code == LexicalAnalyzer.keyWords["-"] ||
                    (curToken as CTokenOperation).code == LexicalAnalyzer.keyWords["or"])
                {
                    return true;
                }
            }
            return false;
        }


        bool Realat() // <операция отношения>::==|<>|<|<=|>=|>|in		*
        {

            if (curToken.tt == TokenType.Operation)
            {
                if ((curToken as CTokenOperation).code == LexicalAnalyzer.keyWords["="] ||
                    (curToken as CTokenOperation).code == LexicalAnalyzer.keyWords["<>"] ||
                    (curToken as CTokenOperation).code == LexicalAnalyzer.keyWords["<"] ||
                    (curToken as CTokenOperation).code == LexicalAnalyzer.keyWords[">"] ||
                    (curToken as CTokenOperation).code == LexicalAnalyzer.keyWords[">="] ||
                    (curToken as CTokenOperation).code == LexicalAnalyzer.keyWords["<="] ||
                    (curToken as CTokenOperation).code == LexicalAnalyzer.keyWords["in"])

                {
                    return true;
                }
            }
            return false;
        }

        CType Factor() //<множитель>::=<имя>|<константа без знака>|(<выражение>)
       
        {
            if (curToken.tt == TokenType.Identifier)
            {
                CType a = tableOfIdentifiers[(curToken as CTokenIdentifier).name];
                GetNextToken();
                if (a != null)
                    return a;
                else
                    la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 12));
            }

            if (curToken.tt == TokenType.Value)
            {
                if (((CTokenValue)curToken).value is CIntVariant)
                {
                    GetNextToken();
                    return new CTypeInt();
                }
                if (((CTokenValue)curToken).value is CRealVariant)
                {
                    GetNextToken();
                    return new CTypeReal();
                }
                if (((CTokenValue)curToken).value is CStringVariant)
                {
                    GetNextToken();
                    return new CTypeString();
                }
                if (((CTokenValue)curToken).value is CBooleanVariant)
                {
                    GetNextToken();
                    return new CTypeBoolean();
                }
            }

            return Excpression();
            la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 9));
            GetNextToken();
            //return Excpression();
        }

        void CompositeOperator() // <составной оператор>::= begin <оператор>;{<оператор>;} end
        {
            GetNextToken();

            Operator();
            if (!Accept(TokenType.Operation, ";"))
                la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 3));

            while (true)
            {
                if (Accept(TokenType.Operation, ";"))
                {
                    GetNextToken();


                    Operator();
                }
                else
                {
                    break;
                }
            }
            if (!Accept(TokenType.Operation, "end"))
            {
                la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 3));
            }
            GetNextToken();
        }

        void ChoiseOperator() // <выбирающий оператор>::= if <выражение> then <оператор>|if <выражение> then <оператор> else <оператор>
        {
            GetNextToken();

            CType left = Excpression();
            if (!(left is CTypeBoolean))
                la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 9));

            if (!Accept(TokenType.Operation, "then"))
                la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 3));

            GetNextToken();

            Operator();

            if (Accept(TokenType.Operation, "else"))
            {
                GetNextToken();
                Operator();
            }
        }

        void CycleOpertor() // <оператор цикла>::= while <выражение> do <оператор>
        {
            GetNextToken();

            CType left = Excpression();

            if (!(left is CTypeBoolean))
                la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 9));

            if (!Accept(TokenType.Operation, "do"))
                la.IO.errorsInLine.Add(new Error(la.IO.curPosition, 3));
            GetNextToken();

            Operator();
        }
      
    }
   

}

