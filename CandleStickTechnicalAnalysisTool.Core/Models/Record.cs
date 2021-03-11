using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CandleStickTechnicalAnalysisTool.Core.Models
{
    public class Record
    {
        [JsonPropertyName("Symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("CompanyName")]
        public string CompanyName { get; set; }

        [JsonPropertyName("TierCode")]
        public string TierCode { get; set; }

    }
}
