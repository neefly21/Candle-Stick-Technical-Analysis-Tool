using System.Collections.Generic;

namespace CandleStickTechnicalAnalysisTool.Core.Models.PatternScanning
{
    public class CompanyPatternResults
    {
        public CandleStickPattern Pattern { get; set; }
        public Record CompanyRecord { get; set; }
        public List<PatternScanResults> PatternScanResults { get; set; }
        public bool ContainsSelectedPattern { get; set; }

        public static CompanyPatternResults Create(CandleStickPattern pattern, Record companyRecord, List<PatternScanResults> scanResults, bool containsSelectedPattern)
        => new CompanyPatternResults {
            PatternScanResults = scanResults,
            CompanyRecord = companyRecord,
            ContainsSelectedPattern = containsSelectedPattern,
            Pattern = pattern
        };
    }
}
