using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CandleStickTechnicalAnalysisTool.Core
{
    public enum CandleStickPattern
    {
        [Description("Two Crows")]
        Cdl2Crows,
        
        [Description("Three Black Crows")]
        Cdl3BlackCrows,
        
        [Description("Three Inside Up/Down")]
        Cdl3Inside,
        
        [Description("Three-Line Strike")]
        Cdl3LineStrike,
        
        [Description("Three Outside Up/Down")]
        Cdl3Outside,
        
        [Description("Three Stars In The South")]
        Cdl3StarsInSouth,
        
        [Description("Three Advancing White Soldiers")]
        Cdl3WhiteSoldiers,
        
        [Description("Abandoned Baby")]
        CdlAbandonedBaby,
        
        [Description("Advance Block")]
        CdlAdvanceBlock,
        
        [Description("Belt-hold")]
        CdlBeltHold,
        
        [Description("Breakaway")]
        CdlBreakaway,
        
        [Description("Closing Marubozu")]
        CdlClosingMarubozu,
        
        [Description("Concealing Baby Swallow")]
        CdlConcealBabysWall,
        
        [Description("Counterattack")]
        CdlCounterAttack,
        
        [Description("Dark Cloud Cover")]
        CdlDarkCloudCover,
        
        [Description("Doji")]
        CdlDoji,
        
        [Description("Doji Star")]
        CdlDojiStar,
        
        [Description("Dragonfly Doji")]
        CdlDragonflyDoji,
        
        [Description("Engulfing Pattern")]
        CdlEngulfing,
        
        [Description("Evening Doji Star")]
        CdlEveningDojiStar,
        
        [Description("Evening Star")]
        CdlEveningStar,
        
        [Description("Up/Down-gap side-by-side white lines")]
        CdlGapSideSideWhite,
        
        [Description("Gravestone Doji")]
        CdlGravestoneDoji,
        
        [Description("Hammer")]
        CdlHammer,
        
        [Description("Hanging Man")]
        CdlHangingMan,
        
        [Description("Harami Pattern")]
        CdlHarami,
        
        [Description("Harami Cross Pattern")]
        CdlHaramiCross,
        
        [Description("High-Wave Candle")]
        CdlHignWave,
        
        [Description("Hikkake Pattern")]
        CdlHikkake,
        
        [Description("Modified Hikkake Pattern")]
        CdlHikkakeMod,
        
        [Description("Homing Pigeon")]
        CdlHomingPigeon,
        
        [Description("Identical Three Crows")]
        CdlIdentical3Crows,
        
        [Description("In-Neck Pattern")]
        CdlInNeck,
        
        [Description("Inverted Hammer")]
        CdlInvertedHammer,
        
        [Description("Kicking")]
        CdlKicking,
        
        [Description("Kicking - bull/bear determined by the longer marubozu")]
        CdlKickingByLength,
        
        [Description("Ladder Bottom")]
        CdlLadderBottom,
        
        [Description("Long Legged Doji")]
        CdlLongLeggedDoji,
        
        [Description("Long Line Candle")]
        CdlLongLine,
        
        [Description("Marubozu")]
        CdlMarubozu,
        
        [Description("Matching Low")]
        CdlMatchingLow,
        
        [Description("Mat Hold")]
        CdlMatHold,
        
        [Description("Morning Doji Star")]
        CdlMorningDojiStar,
        
        [Description("Morning Star")]
        CdlMorningStar,
        
        [Description("On-Neck Pattern")]
        CdlOnNeck,
        
        [Description("Piercing Pattern")]
        CdlPiercing,
        
        [Description("Rickshaw Man")]
        CdlRickshawMan,
        
        [Description("Rising/Falling Three Methods")]
        CdlRiseFall3Methods,
        
        [Description("Separating Lines")]
        CdlSeperatingLines,
        
        [Description("Shooting Star")]
        CdlShootingStar,
        
        [Description("Short Line Candle")]
        CdlShortLine,
        
        [Description("Spinning Top")]
        CdlSpinningTop,
        
        [Description("Stalled Pattern")]
        CdlStalledPattern,
        
        [Description("Stick Sandwich")]
        CdlStickSandwhich,
        
        [Description("Takuri (Dragonfly Doji with very long lower shadow)")]
        CdlTakuri,
        
        [Description("Tasuki Gap")]
        CdlTasukiGap,
        
        [Description("Thrusting Pattern")]
        CdlThrusting,
        
        [Description("Tristar Pattern")]
        CdlTristar,
        
        [Description("Unique 3 River")]
        CdlUnique3River,
        
        [Description("Upside Gap Two Crows")]
        CdlUpsideGap2Crows,
        
        [Description("Upside/Downside Gap Three Methods")]
        CdlXSideGap3Methods

    }
}
