
namespace ScalperPlus.Common.TradingServices;

internal interface IIndicators
{
    decimal Ma(List<IBinanceKline> list, int period = 5, int shift = 0);
    List<Candle> Rsi(List<Candle> list);
}
