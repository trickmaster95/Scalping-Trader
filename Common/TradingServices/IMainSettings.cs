namespace ScalperPlus.Common.TradingServices;

internal interface IMainSettings
{
    public BotSettings Settings { get; }
    public string Symbol { get; }
    public void Save();
}
