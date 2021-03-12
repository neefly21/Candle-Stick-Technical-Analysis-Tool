using System.Collections.Generic;

namespace CandleStickTechnicalAnalysisTool.Core.Models.PatternScanning
{
    public class CompanyPatternResults
    {
        public CandleStickPattern Pattern { get; set; }

        public Record CompanyRecord { get; set; }

        public List<CandleStick> CandleSticks { get; set; } = new List<CandleStick>();

        public List<PatternScanResults> PatternScanResults { get; set; } = new List<PatternScanResults>();
        public bool ContainsSelectedPattern { get; set; }

        public static CompanyPatternResults Create(CandleStickPattern pattern, Record companyRecord, List<PatternScanResults> scanResults, List<CandleStick> candleSticks, bool containsSelectedPattern)
        => new CompanyPatternResults {
            PatternScanResults = scanResults,
            CompanyRecord = companyRecord,
            ContainsSelectedPattern = containsSelectedPattern,
            CandleSticks = candleSticks,
            Pattern = pattern
        };
    }
}
