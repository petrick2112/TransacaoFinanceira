using System;

namespace TransacaoFinanceira.Utils
{
    public class SimpleLogger
    {
        public void Info(string message)
        {
            Console.WriteLine(message);
        }

        public void Error(string message)
        {
            Console.WriteLine(message);
        }
    }
}
