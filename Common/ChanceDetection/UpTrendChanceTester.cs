

namespace ScalperPlus.Common.ChanceDetection;

internal class UpTrendChanceTester : IChanceTester
{
    private readonly IChart _chart;
    private readonly DeterminantsSettings? _determinations;
    public UpTrendChanceTester(IChart chart)
    {
        _chart = chart;
        IDbContext db = new DbContext();
        _determinations = db.GetTable<DeterminantsSettings>()?.Find(item=>item.MarketStateStatus == MarketStateStatus.UpTrend).GetAwaiter().GetResult();
    }
    public bool HaveChance()
    {
        if ( _determinations == null ) return false;
        if ( _chart.Candles.LastOrDefault()?.Rsi14 < _determinations.MaxRsi14 ) return true;
        else return false;
    }
}
