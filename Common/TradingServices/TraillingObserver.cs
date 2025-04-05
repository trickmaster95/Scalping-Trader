

namespace ScalperPlus.Common.TradingServices;

internal class TraillingObserver:ITraillingObserver
{
    private readonly IMarketState marketState;
    private MarketStateStatus currentStatus;
    private readonly IOrder order;
    private readonly ITransactionManager transactionManager;
    private readonly IDbContext dbContext;
    private DeterminantsSettings determinantsSettings;
    public TraillingObserver(IMarketState marketState, IOrder order , ITransactionManager transactionManager, IDbContext dbContext)
    {
        this.marketState = marketState;
        this.order = order;
        this.transactionManager = transactionManager;
        this.dbContext = dbContext;
        Update();
    }

    private void Update()
    {
        Table<DeterminantsSettings>? table = dbContext.GetTable<DeterminantsSettings>();
        if (table == null) throw new Exception("table not found");
        determinantsSettings = table.Find(a => a.MarketStateStatus == marketState.Current).GetAwaiter().GetResult();
        currentStatus = this.marketState.Current;
    }
    public void ObserveTrailling(IBinanceStreamKlineData liveCandle)
    {
        if (transactionManager.Current != null)
        {
            if (transactionManager.Current.Status == TransactionStatus.Selling)
            {
                if(currentStatus != marketState.Current) Update();
                if (determinantsSettings.TraillingActivation)
                {
                    decimal traillingStop = (Math.Max((transactionManager.Current.BuyPrice ?? 0m), (transactionManager.Current.TrailedPrice ?? 0m)) * (1 + determinantsSettings.Trailling * 0.01m));
                    if (liveCandle.Data.ClosePrice >= traillingStop)
                    {
                        List<long> ids = new List<long>();
                        if (transactionManager.Current.UpperSellOrderId != null) ids.Add((long)transactionManager.Current.UpperSellOrderId);
                        if (transactionManager.Current.LowerSellOrderId != null) ids.Add((long)transactionManager.Current.LowerSellOrderId);
                        order.cancleOrders(ids);
                        decimal? quantity = transactionManager.Current.Quantity;
                        decimal? takeProfitLimit = liveCandle.Data.ClosePrice * (1m + determinantsSettings.TakeProfit * 0.01m);
                        decimal? stopLimit = liveCandle.Data.ClosePrice * (1m + determinantsSettings.StopLose * 0.01m);
                        BinanceOrderOcoList? sellOrder = order.SellOco((decimal)quantity, (decimal)takeProfitLimit, (decimal)stopLimit);
                        if (sellOrder == null) throw new Exception("unhandled error");
                        transactionManager.CreateOco(sellOrder);
                    }
                }
            }
        }
    }
}
