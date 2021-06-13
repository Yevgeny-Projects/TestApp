using System;

namespace TestApp.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new AdvancedParser();
            parser.Parse().GetAwaiter().GetResult();
        }
    }
}
