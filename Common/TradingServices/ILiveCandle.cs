
namespace ScalperPlus.Common.TradingServices;

internal interface ILiveCandle:Initializable
{
    public event VoidEventHandler? CandleOpened;

    public delegate void CandleUpdatedEventHandler(IBinanceStreamKlineData? candle);
    public event CandleUpdatedEventHandler? CandleUpdated;

    public Candle LastCandle { get; set; }
}
