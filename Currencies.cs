using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp
{
    public class Currencies
    {
        [JsonProperty("Date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("PreviousDate")]
        public DateTimeOffset PreviousDate { get; set; }

        [JsonProperty("PreviousURL")]
        public string PreviousUrl { get; set; }

        [JsonProperty("Timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        [JsonProperty("Valute")]
        public Dictionary<string, Currency> Valute { get; set; }
    }
}
