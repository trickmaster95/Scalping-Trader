

namespace ScalperPlus.Common.TradingServices;

internal interface IChart:Initializable
{
    public event VoidEventHandler ChartUpdated;

    public List<Candle> Candles { get; set; }
}
