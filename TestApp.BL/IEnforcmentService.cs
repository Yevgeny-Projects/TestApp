using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.Shared.Models;

namespace TestApp.BL
{
    public interface IEnforcmentService
    {
        string GetFewestRecalsReportDate();
        List<EnforcmentData> GetAllrecallsForDate(string reportDate);
        Task<EnforcmentResult> GetEnforcmentResult(string apiUrl,
                string search,
                string apiKey,
                int skip,
                int limit);
        string GetMostOccuranceWord(List<EnforcmentData> list);

    }
}