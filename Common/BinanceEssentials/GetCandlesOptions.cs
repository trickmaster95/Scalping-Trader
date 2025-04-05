namespace ScalperPlus.Common.BinanceEssentials;

public class GetCandlesOptions
{
    public string Symbol { get; set; }
    public KlineInterval Interval { get; set; } = KlineInterval.FiveMinutes;
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int Limit { get; set; } = 100;
}
