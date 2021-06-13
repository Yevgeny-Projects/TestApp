using System;
using TestApp.BL;

namespace TestApp.EnforcmentConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            new IoCImpl().Configure();
            Console.WriteLine("Hello Enforcment API!");
            EnforcmentService service = new EnforcmentService("https://api.fda.gov/food/enforcement.json",
                "report_date:[20120101+TO+20121231]",
                "L6c0owIiFxWQxDl6xyLPWGefxt2uwuaBBtF5mfn8",
                0,
                1000
            );

            string Report_date = service.GetFewestRecalsReportDate();

            var list = service.GetAllrecallsForDate(Report_date);
            string mostOccuranceWord = service.GetMostOccuranceWord(list);
            Console.WriteLine($"The word with the highest number of occurrences under the reason_for_recall field: {mostOccuranceWord}");
        }
    }
}