using course4.Errors;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace course4
{
    public class Analiz//lexical
    {
        #region 
        static public string[] SLOVA = { "begin", "integer", "for", "step", "until", "do", "end", "real" };
        static char[] Razdeliteli = { '(', ')', ',', ':', ';', '=', '+', '*','/','-' };
        static List<string> AllSymbol = new List<string>();
        static List<string> AllType = new List<string>();
        static List<string> Table1 = SLOVA.ToList();
        static List<string> TABLE2;
        static List<string> TABLE3;
        static List<string> TABLE4;
        static List<string> TABLE5;
        #endregion

        public static List<string> LexAnalizText(string COD)
        {
            #region
            string copyCOD = COD,
                BUFFER = string.Empty;
            int lengthCOD = COD.Length,
                symbol = 0;
            AllSymbol = new List<string>();
            AllType = new List<string>();
            #endregion
            Clear();
            while (symbol != lengthCOD)
            {
                if (char.IsDigit(copyCOD[symbol]))
                {
                    Add();
                    Next();
                    bool Dig=false;
                    if (symbol != lengthCOD)
                        Dig = true;                        
                    while (Dig)
                    {
                        if (char.IsDigit(copyCOD[symbol]))
                        {
                            Add();
                            Next();
                            if (symbol >= lengthCOD)
                                break;
                        }
                        else Dig = false;
                    }
                    Out("lit");
                    Clear();
                }
                else if (char.IsLetter(copyCOD[symbol]))
                {
                    Add();
                    Next();
                    bool Ide = false;
                    if (symbol != lengthCOD)
                        Ide = true;
                    while (Ide && symbol < lengthCOD)
                    {
                        if (char.IsLetter(copyCOD[symbol]) || char.IsDigit(copyCOD[symbol]))
                        {
                            Add();
                            Next();
                            if (symbol >= lengthCOD)
                                break;
                        }
                        else Ide = false;
                    }
                    Out("ide");
                    Clear();
                }
                else if (RazdelitelSearch(copyCOD[symbol]))
                {
                    Add();
                    Next();
                    Out("raz");
                    Clear();
                }
                else if (copyCOD[symbol] == ' ' || copyCOD[symbol] == '\n' || copyCOD[symbol] == '\t')
                {
                    Next();
                }
                else { MessageBox.Show("unknown symlol"+copyCOD[symbol], "error"); }
            }

            bool RazdelitelSearch(char rez)
            {
                return Razdeliteli.Contains(rez);
            }
            void Add()
            {
                BUFFER += copyCOD[symbol];
            }
            void Next()
            {
                symbol++;
            }
            void Clear()
            {
                BUFFER = string.Empty;
            }
            void Out(string tip)
            {
                if (tip == "ide")
                {
                    AllSymbol.Add(BUFFER.ToLower());
                    AllType.Add("ide");
                }
                if (tip == "lit")
                {
                    AllSymbol.Add(BUFFER.ToLower());
                    AllType.Add("lit");
                }
                if (tip == "raz")
                {
                    AllSymbol.Add(BUFFER.ToLower());
                    AllType.Add("raz");
                }
            }

            #region
            TABLE2 = new List<string>();//razdelitel
            TABLE3 = new List<string>();//id
            TABLE4 = new List<string>();//lit
            TABLE5 = new List<string>();//tss
            #endregion
            for (int i = 0; i < Razdeliteli.Length; i++)
            {
                TABLE2.Add(Razdeliteli[i].ToString());
            }

            for (int i = 0; i < AllSymbol.Count; i++)
            {
                if (AllType[i] == "ide")
                {
                    if (Table1.Contains(AllSymbol[i]))
                    {
                        int number = Table1.LastIndexOf(AllSymbol[i]) + 1;
                        TABLE5.Add(string.Format("{0}?1,{1}", AllSymbol[i], number));
                    }
                    else
                    {
                        int number = -1;
                        if (TABLE4.Contains(AllSymbol[i]))
                        {
                            number = TABLE4.LastIndexOf(AllSymbol[i]);
                        }
                        else
                        {
                            TABLE4.Add(AllSymbol[i]);
                            number = TABLE4.Count;
                        }
                        TABLE5.Add(string.Format("{0}?4,{1}", AllSymbol[i], number));
                    }
                }
                if (AllType[i] == "raz")
                {
                    int number = TABLE2.LastIndexOf(AllSymbol[i]) + 1;
                    TABLE5.Add(string.Format("{0}?2,{1}", AllSymbol[i], number));
                }
                if (AllType[i] == "lit")
                {
                    int number;
                    if (!TABLE3.Contains(AllSymbol[i]))
                    {
                        TABLE3.Add(AllSymbol[i]);
                        number = TABLE3.Count + 1;
                    }
                    else
                    {
                        number = TABLE3.LastIndexOf(AllSymbol[i]);
                    }
                    TABLE5.Add(string.Format("{0}?3,{1}", AllSymbol[i], number));
                }
            }
            return TABLE5;
        }
        //outTable
        public static void OutTable0(DataGridView table0)
        {
            table0.Rows.Clear();
            for (int i = 0; i < AllSymbol.Count; i++)
            {
                string type = "";
                if (AllType[i] == "ide")
                {
                    type = "Идентификатор";
                }
                if (AllType[i] == "lit")
                {
                    type = "Литерал";
                }
                if (AllType[i] == "raz")
                {
                    type = "Разделитель";
                }
                table0.Rows.Add(AllSymbol[i], type);
            }
        }

        public static void OutTable12345(DataGridView spec, DataGridView raz, DataGridView lit, DataGridView var, DataGridView res)
        {
            for (int i = 0; i < Table1.Count; i++)
            {
                spec.Rows.Add(spec.Rows.Count + 1, Table1[i]);
            }
            for (int i = 0; i < TABLE2.Count; i++)
            {
                raz.Rows.Add(raz.Rows.Count + 1, TABLE2[i]);
            }
            for (int i = 0; i < TABLE3.Count; i++)
            {
                lit.Rows.Add(lit.Rows.Count + 1, TABLE3[i]);
            }
            for (int i = 0; i < TABLE4.Count; i++)
            {
                var.Rows.Add(var.Rows.Count + 1, TABLE4[i]);
            }
            for (int i = 0; i < TABLE5.Count; i++)
            {
                string[] temp = TABLE5[i].Split('?');
                res.Rows.Add(temp[0], string.Format("({0})", temp[1]));
            }
        }
    }
    

    public class SNX//sintax
    {
        #region
        static int SymbolNum = 0,
            TableNum = 0,
            TableNum1 = 0,
            TableNum2 = 0,
            lexemEND = 1;
        static string
            lexem0 = string.Empty,
            lexem = string.Empty,
            lexem1 = string.Empty,
            lexem2 = string.Empty;
        static List<string> table,
            peremen;
        static bool skobki=false;
        #endregion

        public static void AnalyzeText(List<string> table5)
        {
            SymbolNum = 0;
            lexemEND = 1;
            table = table5;
            peremen = new List<string>();
            var OldLex = table[0].Split('?');//разделяем таблицу по знаку '?'
            lexem = OldLex[0];
            var OldLexemNum = OldLex[1];
            TableNum = int.Parse(OldLexemNum.Split(',')[0]);
            //---
            program();
            skobki = false;
        }

        static void NEXT(string word)
        {
            SymbolNum++;
            if (SymbolNum == table.Count)
                throw new EndLine(word);
            var OldLex = table[SymbolNum].Split('?');
            lexem = OldLex[0].ToLower();
            var OldLexemNum = OldLex[1];
            TableNum = int.Parse(OldLexemNum.Split(',')[0]);
        }
        static void NEXT2(string word)
        {
            SymbolNum++;
            if (SymbolNum == table.Count)
                throw new EndLine(word);
            var OldLex = table[SymbolNum].Split('?');
            lexem = OldLex[0].ToLower();
            var OldLexemNum = OldLex[1];
            TableNum = int.Parse(OldLexemNum.Split(',')[0]);
            if (SymbolNum < table.Count - 2)
            {
                OldLex = table[SymbolNum + 1].Split('?');
                lexem1 = OldLex[0].ToLower();
                OldLexemNum = OldLex[1];
                TableNum1 = int.Parse(OldLexemNum.Split(',')[0]);
                OldLex = table[SymbolNum + 2].Split('?');
                lexem2 = OldLex[0].ToLower();
                OldLexemNum = OldLex[1];
                TableNum2 = int.Parse(OldLexemNum.Split(',')[0]);
            }
        }
        static void program() //<programm> ::= BEGIN <spisok_op> END
        {
            if (lexem != "begin")
                throw new WrongSymbol(lexem, "BEGIN");
            NEXT("spisok");
            spisok_op();
            if (lexem != "end")
                throw new WrongSymbol(lexem, "END");
            bool endcode = true;
            try
            {
                NEXT("Конец программы после END");
                endcode = false;
            }
            catch{}
            if (!endcode)
            {
                throw new EndCode(lexem);
            }            
        }
        static void spisok_op() //<spisok_op> ::= <oper> , <spisok_op> ::= <spisok_op><oper>
        {
            oper();
            NEXT2("Оператор или END");
            if ((TableNum == 1 || TableNum == 4) && lexem != "end")
            {
                if (SymbolNum >= table.Count - lexemEND) return;
                spisok_op();
            }
        }
        static void oper() //<oper> ::= <objav> , <oper> ::= <cikl> ,<oper> ::= <prisv>
        {
            if (TableNum == 1 || TableNum == 4)
            {
                if (TableNum == 1)
                {
                    if (lexem == "integer" || lexem == "real" || lexem == "boolean")
                    {
                        objav(); return;
                    }
                    if (lexem == "for")
                    { lexemEND++; cikl(); }
                }
                else//table4
                {
                    if (TableNum1 == 4)
                    {
                        if (lexem2 == ":")
                        { lexemEND++; cikl(); }
                        else objav();
                    }
                    else prisv();
                }
            }
            else
                throw new WrongSymbol(lexem, "Оператор или переменная");
        }
        static void objav() //<objav> ::= <tip><spisok_id>;
        {
            tip();
            spisok_id();
            if (lexem != ";")
                throw new WrongSymbol(lexem, "Разделитель");
        }
        static void tip() //<tip> ::= REAL , <tip> ::= integer , <tip> ::= BOOLEAN
        {
            if (lexem != "real" && lexem != "integer" && lexem != "boolean")
                throw new WrongSymbol(lexem, "ТИП");
        }
        static void spisok_id() //<spisok_id> ::= id , <spisok_id> ::= id, <spisok_id>
        {
            NEXT("Список идентификаторов");
            if (TableNum != 4)
                throw new WrongSymbol(lexem, "Идентификатор");
            peremen.Add(lexem);//объявленные идентификаторы
            NEXT(", или ;");
            if (lexem == ",")
                spisok_id();
        }
        static void cikl() //<cikl> ::= FOR id := <operand> STEP <operand> UNTIL <operand> DO BEGIN <spisok_op> END
        {
            if (lexem != "for")
                throw new WrongSymbol(lexem, "FOR");
            NEXT("ID");
            if (TableNum != 4)
                throw new WrongSymbol(lexem, "Идентификатор");
            peremen.Add(lexem);//индетификатор от FOR
            NEXT(":");
            if (lexem != ":")
                throw new WrongSymbol(lexem, ":");
            NEXT("=");
            if (lexem != "=")
                throw new WrongSymbol(lexem, "=");
            operand();
            NEXT("STEP");
            if (lexem != "step")
                throw new WrongSymbol(lexem, "step");
            operand();
            NEXT("UNTIL");
            if (lexem != "until")
                throw new WrongSymbol(lexem, "until");
            operand();
            NEXT("DO");
            if (lexem != "do")
                throw new WrongSymbol(lexem, "do");
            NEXT("BEGIN");
            if (lexem != "begin")
                throw new WrongSymbol(lexem, "begin");
            NEXT2("Список");
            spisok_op();//---
            if (lexem != "end")
                throw new WrongSymbol(lexem, "end");
        }
        static void operand() //<operand> ::= lit , <operand> ::= id
        {
            NEXT("Литерал или идентификатор или скобки");
            if (TableNum == 2)
            {
                if (lexem == "(")
                {
                    skobki = true;
                    NEXT("Литерал или идентификатор");
                }
            }
            if (TableNum != 3 && TableNum != 4)
                throw new WrongSymbol(lexem, "Литерал или идентификатор");
            if (TableNum == 4)
            {
                if (!peremen.Contains(lexem))
                    throw new Obj(lexem);
            }
        }
        static void prisv() // <prisv> ::= id := <viraj>;
        {
            if (!peremen.Contains(lexem))
                throw new Obj(lexem);
            NEXT(":");
            if (lexem != ":")
                throw new WrongSymbol(lexem, ":");
            NEXT("=");
            if (lexem != "=")
                throw new WrongSymbol(lexem, "=");
            viraj();
            if (lexem != ";")
                throw new WrongSymbol(lexem, "разделитель");
        }
        static void viraj() // <viraj> ::= <operand>,  <viraj> ::= <operand><sloj_viraj>
        {
            //--
            operand();//идентификатор или литерал
            if (TableNum == 4)//проверка в таблице переменных
            {
                if (!peremen.Contains(lexem))//проверка в переменных
                {
                    throw new Obj(lexem);//исключение
                }
            }
            lexem0 = lexem;//-
            NEXT("Арифметикое действие или разделитель");//если следующий символ знак = сложное выражение
            if (TableNum == 2)//проверка в таблице знаков
            {
                if (lexem == "+" || lexem == "-" || lexem == "*" || lexem == "/")
                {
                    sloj_viraj(lexem0);
                }
            }
        }
        static void sloj_viraj(string lex)
        {
            List<string> logical = new List<string>();
            if (skobki)
            {
                logical.Add("(");
            }
            logical.Add(lex);
            while (lexem != ";")
            {
                if (TableNum == 1)
                    throw new WrongSymbol(lexem, "Литерал или идентификатор");
                if (TableNum == 4)
                {
                    if (!peremen.Contains(lexem))
                    {
                        throw new Obj(lexem);
                    }
                }
                logical.Add(lexem);
                NEXT(";");
            }
            Logical.Log(logical);
        }
    }

    public class Logical//разбор
    {
        static bool flag = false,
                    raz = false,
                    oper = true;                     
        static Stack<string> E = new Stack<string>();
        static Stack<string> T = new Stack<string>();
        static int operandNUM = 0, skobka=0;
        static List<string> expr = null;
        static string[] Arifmet = { "(", ")", "+", "-", "/", "*" };
        static List<string> Matrix = new List<string>();

        public static void Log(List<string> ex)
        {
            flag = false;
            raz = false;
            oper = true;
            skobka = 0;
            E = new Stack<string>();
            T = new Stack<string>();
            Matrix = new List<string>();
            expr = ex;//массив сложного выражения
            expr.Add("<$>");//символ конца выражения
            
            for (operandNUM = 0; !flag;)
            {
                deikstra1();
            }
            while (flag)
            {
                deikstra2();
            }
            expr.Remove("<$>");
        }

        public static void deikstra1()
        {
            //е - выход
            //t - стек
            if (expr[operandNUM] != "<$>")
            {
                if (!Arifmet.Contains(expr[operandNUM]))
                {
                    if (oper)
                    {
                        e_push(expr[operandNUM]);
                        operandNUM++;
                        raz = true;
                        oper = false;
                    }
                    else throw new LogicalOper();
                }
                else if (Arifmet.Contains(expr[operandNUM]))
                {
                    if (expr[operandNUM] == "(")
                    {
                        skobka++;
                        t_push(expr[operandNUM]);
                        operandNUM++;
                    }
                    else if (expr[operandNUM] == ")")
                    {
                        try
                        {
                            while (T.Peek() != "(")
                            {
                                e_push(T.Pop());
                            }
                            T.Pop();
                        }
                        catch { throw new skobk(); }
                        skobka--;
                        operandNUM++;
                    }
                    else if (raz)
                    {
                        raz = false;
                        oper = true;
                        if (T.Count == 0)
                        {
                            t_push(expr[operandNUM]);
                            operandNUM++;
                        }                         
                        else if (expr[operandNUM] == "*")
                        {
                            if (T.Peek() == "/")
                            {
                                e_push(T.Pop());
                            }
                            t_push(expr[operandNUM]);
                            operandNUM++;
                        }
                        else if (expr[operandNUM] == "/")
                        {
                            if (T.Peek() == "*")
                            {
                                e_push(T.Pop());
                            }
                            t_push(expr[operandNUM]);
                            operandNUM++;
                        }
                        else if (expr[operandNUM] == "-")
                        {
                            if (T.Peek() == "+"|| T.Peek() == "*" || T.Peek() == "/")
                            {
                                e_push(T.Pop());
                            }
                            t_push(expr[operandNUM]);
                            operandNUM++;
                        }
                        else if (expr[operandNUM] == "+" )
                        {
                            if (T.Peek() == "-" || T.Peek() == "*" || T.Peek() == "/")
                            {
                                e_push(T.Pop());
                            }
                            t_push(expr[operandNUM]);
                            operandNUM++;
                        }
                        else throw new LogicalRaz();
                    }
                }
            }
            else
            {if (skobka>0) throw new skobk2();
                 e_to_t(); flag = true; }
        }
        public static void deikstra2()
        {
            if (T.Count != 0)
            {
                if (!Arifmet.Contains(T.Peek()))
                {
                   e_push(T.Pop());
                }
                else 
                {
                    add_matrix(T.Pop());
                }
            }
            else { flag = false; }
        }
        static void t_push(string t)
        {
            T.Push(t);
        }
        static void e_push(string e)
        {
            E.Push(e);
        }
        static void t_to_e()
        {
            while (T.Count != 0)
                E.Push(T.Pop());
        }
        static void e_to_t()
        {
            while (E.Count != 0)
                T.Push(E.Pop());
        }
        static void add_matrix(string znak)
        {
            if (E.Count>=2)
            {
                Matrix.Add("M" + (Matrix.Count + 1) + ": " + znak + " " + E.Pop() + " " + E.Pop());
                E.Push("M" + (Matrix.Count));
            }
        }
        //out
        public static void Out(DataGridView dgv, RichTextBox rtb)
        {
            rtb.Text = string.Empty;
            dgv.Rows.Clear();
            if (expr == null)
            {
                rtb.Text = "Логическое выражение не найдено.";
                return;
            }
            else
            {
                foreach (var item in expr)
                {
                    rtb.Text += item.ToUpper() + " ";
                }
                foreach (var item in Matrix)
                {
                    dgv.Rows.Add(item);
                }
                expr = null;
            }
        }

    }
}
