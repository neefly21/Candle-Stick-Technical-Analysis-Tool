using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CandleStickTechnicalAnalysisTool.Core
{
    public enum CandleStickPattern
    {
        [Description("Two Crows")]
        CDL2CROWS,

        [Description("Three Black Crows")]
        CDL3BLACKCROWS,

        [Description("Three Inside Up/Down")]
        CDL3INSIDE,

        [Description("Three-Line Strike")]
        CDL3LINESTRIKE,

        [Description("Three Outside Up/Down")]
        CDL3OUTSIDE,

        [Description("Three Stars In The South")]
        CDL3STARSINSOUTH,

        [Description("Three Advancing White Soldiers")]
        CDL3WHITESOLDIERS,

        [Description("Abandoned Baby")]
        CDLABANDONEDBABY,

        [Description("Advance Block")]
        CDLADVANCEBLOCK,

        [Description("Belt-hold")]
        CDLBELTHOLD,

        [Description("Breakaway")]
        CDLBREAKAWAY,

        [Description("Closing Marubozu")]
        CDLCLOSINGMARUBOZU,

        [Description("Concealing Baby Swallow")]
        CDLCONCEALBABYSWALL,

        [Description("Counterattack")]
        CDLCOUNTERATTACK,

        [Description("Dark Cloud Cover")]
        CDLDARKCLOUDCOVER,

        [Description("Doji")]
        CDLDOJI,

        [Description("Doji Star")]
        CDLDOJISTAR,

        [Description("Dragonfly Doji")]
        CDLDRAGONFLYDOJI,

        [Description("Engulfing Pattern")]
        CDLENGULFING,

        [Description("Evening Doji Star")]
        CDLEVENINGDOJISTAR,

        [Description("Evening Star")]
        CDLEVENINGSTAR,

        [Description("Up/Down-gap side-by-side white lines")]
        CDLGAPSIDESIDEWHITE,

        [Description("Gravestone Doji")]
        CDLGRAVESTONEDOJI,

        [Description("Hammer")]
        CDLHAMMER,

        [Description("Hanging Man")]
        CDLHANGINGMAN,

        [Description("Harami Pattern")]
        CDLHARAMI,

        [Description("Harami Cross Pattern")]
        CDLHARAMICROSS,

        [Description("High-Wave Candle")]
        CDLHIGHWAVE,

        [Description("Hikkake Pattern")]
        CDLHIKKAKE,

        [Description("Modified Hikkake Pattern")]
        CDLHIKKAKEMOD,

        [Description("Homing Pigeon")]
        CDLHOMINGPIGEON,

        [Description("Identical Three Crows")]
        CDLIDENTICAL3CROWS,

        [Description("In-Neck Pattern")]
        CDLINNECK,

        [Description("Inverted Hammer")]
        CDLINVERTEDHAMMER,

        [Description("Kicking")]
        CDLKICKING,

        [Description("Kicking - bull/bear determined by the longer marubozu")]
        CDLKICKINGBYLENGTH,

        [Description("Ladder Bottom")]
        CDLLADDERBOTTOM,

        [Description("Long Legged Doji")]
        CDLLONGLEGGEDDOJI,

        [Description("Long Line Candle")]
        CDLLONGLINE,

        [Description("Marubozu")]
        CDLMARUBOZU,

        [Description("Matching Low")]
        CDLMATCHINGLOW,

        [Description("Mat Hold")]
        CDLMATHOLD,

        [Description("Morning Doji Star")]
        CDLMORNINGDOJISTAR,

        [Description("Morning Star")]
        CDLMORNINGSTAR,

        [Description("On-Neck Pattern")]
        CDLONNECK,

        [Description("Piercing Pattern")]
        CDLPIERCING,

        [Description("Rickshaw Man")]
        CDLRICKSHAWMAN,

        [Description("Rising/Falling Three Methods")]
        CDLRISEFALL3METHODS,

        [Description("Separating Lines")]
        CDLSEPARATINGLINES,

        [Description("Shooting Star")]
        CDLSHOOTINGSTAR,

        [Description("Short Line Candle")]
        CDLSHORTLINE,

        [Description("Spinning Top")]
        CDLSPINNINGTOP,

        [Description("Stalled Pattern")]
        CDLSTALLEDPATTERN,

        [Description("Stick Sandwich")]
        CDLSTICKSANDWICH,

        [Description("Takuri (Dragonfly Doji with very long lower shadow)")]
        CDLTAKURI,

        [Description("Tasuki Gap")]
        CDLTASUKIGAP,

        [Description("Thrusting Pattern")]
        CDLTHRUSTING,

        [Description("Tristar Pattern")]
        CDLTRISTAR,

        [Description("Unique 3 River")]
        CDLUNIQUE3RIVER,

        [Description("Upside Gap Two Crows")]
        CDLUPSIDEGAP2CROWS,

        [Description("Upside/Downside Gap Three Methods")]
        CDLXSIDEGAP3METHODS

    }
}
