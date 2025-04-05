

namespace ScalperPlus.Common.Models.Settings;

public class DeterminantsSettings : IModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [Unique]
    public MarketStateStatus MarketStateStatus { get; set; }
    public bool Active { get; set; }
    public decimal TakeProfit { get; set; }
    public decimal StopLose { get; set; }
    public bool TraillingActivation { get; set; }
    public decimal Trailling { get; set; }
    public decimal MaxRsi14 { get; set; }
}
