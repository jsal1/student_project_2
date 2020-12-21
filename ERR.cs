using System;

namespace course4
{
    public class ERR : Exception
    {
        public new string Message;
        public string Lexem;
        public int Position;
        public int Length;

        public override string ToString()
        {
            return Message;
        }
    }
    namespace Errors
    {
        public class WrongSymbol : ERR
        {
            public WrongSymbol(string lexem, string word)
            {
                SecLexem = word;
                Message = string.Format("Ошибка. Ожидалось '{0}', а получен '{1}'", word, lexem);
            }
            public string SecLexem;
        }
        public class EndCode : ERR
        {
            public EndCode(string lexem)
            {
                Message = string.Format("Ошибка. Ожидался конец программы после END, а получено {0}", lexem);
            }
        }
        public class NoneSymbol : ERR
        {
            public NoneSymbol(string lexem)
            {
                Message = string.Format("Ошибка. Идентификатор '{0}' не объявлен", lexem);
            }
        }

        public class EndLine : ERR
        {
            public EndLine(string word)
            {
                Message = string.Format("Ошибка. Получен конец строки, а символ {0} не был найден", word);
            }
        }
        public class EndLine2 : ERR
        {
            public EndLine2()
            {
                Message = string.Format("Ошибка. Получен конец строки.\n ЛЕКСИЧЕСКИЙ АНАЛИЗ");
            }
        }
    }
    public class Obj : ERR
    {
        public Obj(string word)
        {
            Message = string.Format("Используется не объявленный ранее идентификатор <"+word+">");
        }
    }
    public class skobk : ERR
    {
        public skobk()
        {
            Message = string.Format("Ожидалась скобка открытия");
        }
    }
    public class skobk2 : ERR
    {
        public skobk2()
        {
            Message = string.Format("Ожидалась скобка закрытия");
        }
    }
    public class LogicalRaz : ERR
    {
        public LogicalRaz()
        {
            Message = string.Format("В арифметическом выражении используется несколько знаков подряд");
        }
    }
    public class LogicalOper : ERR
    {
        public LogicalOper()
        {
            Message = string.Format("В арифметическом выражении используется несколько операндов подряд");
        }
    }
}

