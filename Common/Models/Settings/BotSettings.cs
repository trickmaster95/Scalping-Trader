
namespace ScalperPlus.Common.Models.Settings;

public class BotSettings:IModel
{
    [AutoIncrement, PrimaryKey]
    public int Id { get; set; }
    public string BaseCurrency { get; set; }
    public string QuoteCurrency { get; set; }
    public decimal Balance { get; set; }
    public bool Running { get; set; }
}

