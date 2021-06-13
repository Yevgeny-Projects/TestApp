using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Core;
using TestApp.Core.Deserializer;
using TestApp.DI;
using TestApp.Shared.Models;

namespace TestApp.BL
{
    public class EnforcmentService: IEnforcmentService
    {
        #region Fields

        public List<EnforcmentData> EnforcmentTotal { get; set; }
        private IHttpGetRequestSender _sender;

        #endregion Fields

        #region Constructor

        public EnforcmentService(string apiUrl,
            string search,
            string apiKey,
            int skip,
            int limit)
        {

            _sender = IoCC.Instance.Resolve<IHttpGetRequestSender>();
            EnforcmentTotal = new List<EnforcmentData>();

            EnforcmentResult enforcmentresult = GetEnforcmentResult(apiUrl,
              search,
              apiKey,
              skip,
              limit).GetAwaiter().GetResult();

            EnforcmentMeta enforcmentMeta = enforcmentresult.meta;

            EnforcmentTotal.AddRange(enforcmentresult.results);
        }

        #endregion Constructor

        #region Public methods

        /// <summary>
        /// Print/return the report_date with the fewest recalls in the year
        /// </summary>
        /// <returns>Report date</returns>
        public string GetFewestRecalsReportDate()
        {
            var groups = EnforcmentTotal
                 .GroupBy(n => n.Report_date)
                 .Select(n => new
                 {
                     Report_date = n.Key,
                     Count = n.Count()
                 }
                 )
                 .OrderBy(n => n.Count);

            return groups.FirstOrDefault().Report_date;
        }

        /// <summary>
        /// Print/return a json of all the recalls from the day you found
        /// </summary>
        /// <param name="reportDate"></param>
        /// <returns></returns>
        public List<EnforcmentData> GetAllrecallsForDate(string reportDate)
        {
            var list = EnforcmentTotal
                .Where(g => g.Report_date == reportDate)
                .OrderBy(n => n.Recall_initiation_date)
                .ToList();

            return list;
        }

        /// <summary>
        /// Read from enforcement API
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="search"></param>
        /// <param name="apiKey"></param>
        /// <param name="skip"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<EnforcmentResult> GetEnforcmentResult(string apiUrl,
                string search,
                string apiKey,
                int skip,
                int limit)
        {
            string url = $"{apiUrl}?search={search}&api_key={apiKey}&limit={limit}&skip={skip}";

            var response = await _sender.GetAsync(url);

            var stringContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<EnforcmentResult>(stringContent, new CaseSensitiveDeserializer());
        }

        /// <summary>
        /// Print/return the word with the highest number of occurrences
        /// under the reason_for_recall field, across all recalls
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string GetMostOccuranceWord(List<EnforcmentData> list)
        {
            string all = String.Join<string>(String.Empty, list.Select(g => g.Reason_for_recall));
            var arr = all.Split(' ').Where(c => c.Length >= 4).ToArray();
            var word = FindWord(arr);
            return word;
        }

        #endregion Public methods

        #region Constructor

        /// <summary>
        /// Function returns word with highest frequency
        /// </summary>
        private string FindWord(String[] arr)
        {
            // Create Dictionary to store word
            // and it's frequency
            Dictionary<string, int> hs =
                new Dictionary<string, int>();

            // Iterate through array of words
            for (int i = 0; i < arr.Length; i++)
            {
                // If word already exist in Dictionary
                // then increase it's count by 1
                if (hs.ContainsKey(arr[i]))
                {
                    hs[arr[i]] = hs[arr[i]] + 1;
                }

                // Otherwise add word to Dictionary
                else
                {
                    hs.Add(arr[i], 1);
                }
            }

            // Create set to iterate over Dictionary
            String key = "";
            int value = 0;

            foreach (KeyValuePair<String, int> me in hs)
            {
                // Check for word having highest frequency
                if (me.Value > value)
                {
                    value = me.Value;
                    key = me.Key;
                }
            }

            // Return word having highest frequency
            return key;
        }

        #endregion Constructor
    }
}