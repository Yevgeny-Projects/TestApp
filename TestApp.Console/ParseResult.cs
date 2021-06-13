using System.Collections.Generic;

namespace TestApp.ConsoleApp
{
    public class ParseResult
    {
        public string Url { get; set; }

        public override string ToString()
        {
            return $"Item from {Url}";
        }
    }
}