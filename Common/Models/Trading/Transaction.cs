namespace ScalperPlus.Common.Models.Trading;

public class Transaction : IModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [Required]
    public string Symbol { get; set; }
    [DefaultValue(TransactionStatus.None)]
    public TransactionStatus? Status { get; set; }
    [AllowNull]
    public long? BuyOrderId { get; set; }
    [AllowNull]
    public long? SellOrderId { get; set; }
    [AllowNull]
    public long? UpperSellOrderId { get; set; }
    [AllowNull]
    public long? LowerSellOrderId { get; set; }
    [AllowNull]
    public long? OcoOrderId { get; set; }
    [AllowNull]
    public decimal? Quantity { get; set; }
    [AllowNull]
    public decimal? BuyPrice { get; set; }
    [AllowNull]
    public decimal? BuyFees { get; set; }
    [AllowNull]
    public DateTime? BuyTime { get; set; }
    [AllowNull]
    public decimal? SellPrice { get; set; }
    [AllowNull]
    public decimal? TrailedPrice { get; set; }
    [AllowNull]
    public decimal? SellFeesEq { get; set; }
    [AllowNull]
    public DateTime? SellOrderOpenTime { get; set; }
    [AllowNull]
    public DateTime? SellOrderCloseTime { get; set; }
    [AllowNull]
    public decimal? Profits { get; set; }
    [AllowNull]
    public double? Duration { get; set; }
    [AllowNull]
    public MarketStateStatus? MarketStateAtOpen { get; set; }
    [AllowNull]
    public MarketStateStatus? MarketStateAtClose { get; set; }
}
