using System;
using System.Collections.Generic;

namespace CalcConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {

                Console.Write("Enter expression (ENTER to exit): ");
                string line = Console.ReadLine();

                if (string.IsNullOrEmpty(line))
                    break;

                char[] exp = line.ToCharArray();
                //int value = EvaluateExpression(exp);
                Console.WriteLine("");
                //Console.WriteLine("{0}={1}", line, value);

            }
        }

       
    }
}
