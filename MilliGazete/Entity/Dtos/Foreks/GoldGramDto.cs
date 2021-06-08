using Newtonsoft.Json;

namespace Entity.Dtos
{
    public partial class GoldGramDto
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("Precision")]
        public long Precision { get; set; }

        [JsonProperty("YearlyChange")]
        public double YearlyChange { get; set; }

        [JsonProperty("YOYearlyChange")]
        public double YoYearlyChange { get; set; }

        [JsonProperty("YearlyChangePercent")]
        public double YearlyChangePercent { get; set; }

        [JsonProperty("YOYearlyChangePercent")]
        public double YoYearlyChangePercent { get; set; }

        [JsonProperty("YOYTurnover")]
        public long YoyTurnover { get; set; }

        [JsonProperty("YOYVolume")]
        public long YoyVolume { get; set; }

        [JsonProperty("SMA5")]
        public double Sma5 { get; set; }

        [JsonProperty("SMA10")]
        public double Sma10 { get; set; }

        [JsonProperty("SMA20")]
        public double Sma20 { get; set; }

        [JsonProperty("SMA50")]
        public double Sma50 { get; set; }

        [JsonProperty("SMA100")]
        public double Sma100 { get; set; }

        [JsonProperty("ADX14")]
        public double Adx14 { get; set; }

        [JsonProperty("SMA200")]
        public double Sma200 { get; set; }

        [JsonProperty("PreviousYearClose")]
        public double PreviousYearClose { get; set; }

        [JsonProperty("YTDLowDate")]
        public long YtdLowDate { get; set; }

        [JsonProperty("YTDHigh")]
        public double YtdHigh { get; set; }

        [JsonProperty("YTDHighDate")]
        public long YtdHighDate { get; set; }

        [JsonProperty("YTDLow")]
        public double YtdLow { get; set; }

        [JsonProperty("BOLDOWN2002")]
        public double Boldown2002 { get; set; }

        [JsonProperty("DailyChangePercent")]
        public double DailyChangePercent { get; set; }

        [JsonProperty("Date")]
        public long Date { get; set; }

        [JsonProperty("Low")]
        public double Low { get; set; }

        [JsonProperty("Open")]
        public double Open { get; set; }

        [JsonProperty("BOLUP2002")]
        public double Bolup2002 { get; set; }

        [JsonProperty("Ask")]
        public double Ask { get; set; }

        [JsonProperty("Bid")]
        public double Bid { get; set; }

        [JsonProperty("DailyChange")]
        public double DailyChange { get; set; }

        [JsonProperty("Direction")]
        public long Direction { get; set; }

        [JsonProperty("MACD122609")]
        public double Macd122609 { get; set; }

        [JsonProperty("SecurityType")]
        public string SecurityType { get; set; }

        [JsonProperty("High")]
        public double High { get; set; }

        [JsonProperty("Close")]
        public double Close { get; set; }

        [JsonProperty("Last")]
        public double Last { get; set; }

        [JsonProperty("EMA5")]
        public double Ema5 { get; set; }

        [JsonProperty("EMA10")]
        public double Ema10 { get; set; }

        [JsonProperty("EMA20")]
        public double Ema20 { get; set; }

        [JsonProperty("EMA50")]
        public double Ema50 { get; set; }

        [JsonProperty("EMA100")]
        public double Ema100 { get; set; }

        [JsonProperty("EMA200")]
        public double Ema200 { get; set; }

        [JsonProperty("Currency")]
        public string Currency { get; set; }

        [JsonProperty("Time")]
        public long Time { get; set; }

        [JsonProperty("MOMPreviousClose")]
        public double MomPreviousClose { get; set; }

        [JsonProperty("MOMLowDate")]
        public long MomLowDate { get; set; }

        [JsonProperty("TimeMs")]
        public long TimeMs { get; set; }

        [JsonProperty("MOMHigh")]
        public double MomHigh { get; set; }

        [JsonProperty("MOMHighDate")]
        public long MomHighDate { get; set; }

        [JsonProperty("MOMLow")]
        public double MomLow { get; set; }

        [JsonProperty("DailyDirection")]
        public long DailyDirection { get; set; }

        [JsonProperty("MTDTurnover")]
        public long MtdTurnover { get; set; }

        [JsonProperty("MTDVolume")]
        public long MtdVolume { get; set; }

        [JsonProperty("FGoldD")]
        public long FGoldD { get; set; }

        [JsonProperty("FGold60m")]
        public long FGold60M { get; set; }

        [JsonProperty("FGold15m")]
        public long FGold15M { get; set; }

        [JsonProperty("FGold5m")]
        public long FGold5M { get; set; }

        [JsonProperty("DateTime")]
        public long DateTime { get; set; }

        [JsonProperty("Ticker")]
        public string Ticker { get; set; }

        [JsonProperty("MonthlyChange")]
        public double MonthlyChange { get; set; }

        [JsonProperty("MOMonthlyChange")]
        public double MoMonthlyChange { get; set; }

        [JsonProperty("WOWTurnover")]
        public long WowTurnover { get; set; }

        [JsonProperty("WOWVolume")]
        public long WowVolume { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("MonthlyChangePercent")]
        public long MonthlyChangePercent { get; set; }

        [JsonProperty("MOMonthlyChangePercent")]
        public double MoMonthlyChangePercent { get; set; }

        [JsonProperty("MarketSector")]
        public string MarketSector { get; set; }

        [JsonProperty("MOMTurnover")]
        public long MomTurnover { get; set; }

        [JsonProperty("MOMVolume")]
        public long MomVolume { get; set; }

        [JsonProperty("WOWPreviousClose")]
        public double WowPreviousClose { get; set; }

        [JsonProperty("WOWLowDate")]
        public long WowLowDate { get; set; }

        [JsonProperty("WOWHigh")]
        public double WowHigh { get; set; }

        [JsonProperty("WOWHighDate")]
        public long WowHighDate { get; set; }

        [JsonProperty("Exchange")]
        public string Exchange { get; set; }

        [JsonProperty("WOWLow")]
        public double WowLow { get; set; }

        [JsonProperty("PreviousMonthClose")]
        public double PreviousMonthClose { get; set; }

        [JsonProperty("MTDLowDate")]
        public long MtdLowDate { get; set; }

        [JsonProperty("Volatility")]
        public double Volatility { get; set; }

        [JsonProperty("MTDHigh")]
        public double MtdHigh { get; set; }

        [JsonProperty("MTDHighDate")]
        public long MtdHighDate { get; set; }

        [JsonProperty("WTDTurnover")]
        public long WtdTurnover { get; set; }

        [JsonProperty("MTDLow")]
        public double MtdLow { get; set; }

        [JsonProperty("WTDVolume")]
        public long WtdVolume { get; set; }

        [JsonProperty("WOWeeklyChange")]
        public double WoWeeklyChange { get; set; }

        [JsonProperty("WeeklyChange")]
        public double WeeklyChange { get; set; }

        [JsonProperty("WOWeeklyChangePercent")]
        public double WoWeeklyChangePercent { get; set; }

        [JsonProperty("WeeklyChangePercent")]
        public double WeeklyChangePercent { get; set; }

        [JsonProperty("HisVol21")]
        public double HisVol21 { get; set; }

        [JsonProperty("YOYPreviousClose")]
        public double YoyPreviousClose { get; set; }

        [JsonProperty("HisVol42")]
        public double HisVol42 { get; set; }

        [JsonProperty("YOYLowDate")]
        public long YoyLowDate { get; set; }

        [JsonProperty("HisVol63")]
        public double HisVol63 { get; set; }

        [JsonProperty("PreviousWeekClose")]
        public double PreviousWeekClose { get; set; }

        [JsonProperty("HisVol126")]
        public double HisVol126 { get; set; }

        [JsonProperty("WTDLowDate")]
        public long WtdLowDate { get; set; }

        [JsonProperty("HisVol252")]
        public double HisVol252 { get; set; }

        [JsonProperty("HisVol30")]
        public double HisVol30 { get; set; }

        [JsonProperty("YOYHigh")]
        public double YoyHigh { get; set; }

        [JsonProperty("YOYHighDate")]
        public long YoyHighDate { get; set; }

        [JsonProperty("WTDHigh")]
        public double WtdHigh { get; set; }

        [JsonProperty("WTDHighDate")]
        public long WtdHighDate { get; set; }

        [JsonProperty("PreviousClose")]
        public double PreviousClose { get; set; }

        [JsonProperty("YOYLow")]
        public double YoyLow { get; set; }

        [JsonProperty("PreviousLast")]
        public double PreviousLast { get; set; }

        [JsonProperty("RSI14")]
        public double Rsi14 { get; set; }
    }
}
