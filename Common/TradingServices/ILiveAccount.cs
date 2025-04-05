

namespace ScalperPlus.Common.TradingServices;

internal interface ILiveAccount:Initializable
{
    public BinanceBalance? BaseBalance { get; set; }
    public BinanceBalance? QuoteBalance { get; set; }
}
