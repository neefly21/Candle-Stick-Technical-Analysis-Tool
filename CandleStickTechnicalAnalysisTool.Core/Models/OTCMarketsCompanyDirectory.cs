using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CandleStickTechnicalAnalysisTool.Core.Models
{
    public class OTCMarketsCompanyDirectory
    {
        [JsonPropertyName("totalRecords")]
        public int TotalRecords { get; set; }
        
        [JsonPropertyName("pages")]
        public int Pages { get; set; }

        [JsonPropertyName("currentPage")]
        public int CurrentPage { get; set; }

        [JsonPropertyName("companyName")]
        public int PageSize { get; set; }

        public List<Record> Records { get; set; }
    }
}
