using System;
using System.Collections.Generic;
using System.Text;

namespace CandleStickTechnicalAnalysisTool.Core.Models
{
    public class CandleStick
    {
        public DateTime DateTime { get; set; }
        public int Date { get; set; }
        public double? Open { get; set; }
        public double? High { get; set; }
        public double? Low { get; set; }
        public double? Close { get; set; }
        public double? AdjClose { get; set; }
        public double? Volume { get; set; }

        public static CandleStick FromCsv(string csvLine)
        {
            try
            {
                string[] values = csvLine.Split(',');
                CandleStick dailyValues = new CandleStick();

                double? open = Convert.ToDouble(values[1]);
                double? high = Convert.ToDouble(values[2]);
                double? low = Convert.ToDouble(values[3]);
                double? close = Convert.ToDouble(values[4]);
                double? volume = Convert.ToDouble(values[5]);
                double? adjClose = Convert.ToDouble(values[6]);

                if (open == null || high == null || low == null || close == null || volume == null || adjClose == null)
                    return null;

                dailyValues.DateTime = Convert.ToDateTime(values[0]);
                dailyValues.Date = dailyValues.DateTime.Day;
                dailyValues.Open = open;
                dailyValues.High = high;
                dailyValues.Low = low;
                dailyValues.Close = close;
                dailyValues.Volume = volume;
                dailyValues.AdjClose = adjClose;
                return dailyValues;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
