using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TestApp.Shared.Models
{
    public class EnforcmentResult
    {
        [JsonProperty("meta")]
        public EnforcmentMeta meta { get; set; }
        public IEnumerable<EnforcmentData> results { get; set; }
    }
}