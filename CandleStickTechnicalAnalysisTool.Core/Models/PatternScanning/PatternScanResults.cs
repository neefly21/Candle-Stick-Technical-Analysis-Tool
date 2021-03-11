namespace CandleStickTechnicalAnalysisTool.Core.Models.PatternScanning
{
    public class PatternScanResults
    {
        public CandleStick CandleStick { get; set; }
        public bool IsPatternTriggered { get; set; }

        public static PatternScanResults Create(CandleStick candle, bool patternTriggered)
            => new PatternScanResults { CandleStick = candle, IsPatternTriggered = patternTriggered};
    }
}
