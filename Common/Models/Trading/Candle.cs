
namespace ScalperPlus.Common.Models.Trading;

public class Candle : IModel, IBinanceKline
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public DateTime OpenTime { get; set; }
    public decimal OpenPrice { get; set; }
    public decimal HighPrice { get; set; }
    public decimal LowPrice { get; set; }
    public decimal ClosePrice { get; set; }
    public decimal Volume { get; set; }
    public DateTime CloseTime { get; set; }
    public decimal QuoteVolume { get; set; }
    public int TradeCount { get; set; }
    public decimal TakerBuyBaseVolume { get; set; }
    public decimal TakerBuyQuoteVolume { get; set; }
    public DateTime LocalOpenTime { get; set; }
    public DateTime LocalCloseTime { get; set; }
    public decimal Ma5 { get; set; }
    public decimal Ma25 { get; set; }
    public decimal Ma99 { get; set; }
    public decimal Rsi7 { get; set; }
    public decimal Rsi14 { get; set; }
}
