

namespace ScalperPlus.Common.BinanceEssentials;
public class Exchange
{
    #region subscriptions
    public void SubscribeLiveCandle(string symbol, Action<IBinanceStreamKlineData> action, KlineInterval interval = KlineInterval.FiveMinutes)
    {
        BinanceClients clients = new BinanceClients();
        clients.Socket.SpotApi.ExchangeData.SubscribeToKlineUpdatesAsync(symbol, interval, (a) => action(a.Data)).GetAwaiter().GetResult();
    }
    public string SubscribeLiveAccount(string symbol, Action<BinanceStreamOrderUpdate> onOrderUpdate, Action<BinanceStreamOrderList> onOcoOrder, Action<BinanceStreamPositionsUpdate> accountUpdate)
    {
        BinanceClients clients = new BinanceClients();

        string ListenKey = clients.Socket.SpotApi.Account.StartUserStreamAsync().GetAwaiter().GetResult().Data.Result;

        clients.Socket.SpotApi.Account.SubscribeToUserDataUpdatesAsync(listenKey: ListenKey,
            order => onOrderUpdate(order.Data),
            ocoOrder => onOcoOrder(ocoOrder.Data),
            account => accountUpdate(account.Data)
        );
        return ListenKey;
    }
    public void KeepAlive(string listenKey)
    {
        BinanceClients clients = new BinanceClients();
        clients.Socket.SpotApi.Account.KeepAliveUserStreamAsync(listenKey);
    }
    #endregion
    #region rest
    public List<IBinanceKline> GetCandles(GetCandlesOptions options)
    {
        BinanceClients clients = new BinanceClients();
        return clients.Rest.SpotApi.ExchangeData.GetKlinesAsync(options.Symbol, options.Interval, options.StartTime, options.EndTime, options.Limit).Result.Data.ToList<IBinanceKline>();
    }
    public BinanceBalance? GetBalance(string asset)
    {
        BinanceClients clients = new BinanceClients();
        return clients.Rest.SpotApi.Account.GetAccountInfoAsync().Result.Data.Balances.FirstOrDefault(item => item.Asset == asset);
    }
    public List<BinanceBalance> GetBalances(List<string> assets)
    {
        BinanceClients clients = new BinanceClients();
        return clients.Rest.SpotApi.Account.GetAccountInfoAsync().Result.Data.Balances.Where(item => assets.Contains(item.Asset)).ToList();
    }
    #endregion
}
