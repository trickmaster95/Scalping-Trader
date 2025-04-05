


namespace ScalperPlus.Common.TradingServices;

internal class TransactionManager:ITransactionManager
{
    public Transaction? Current { get; private set; }
    public Table<Transaction>? transactionsTable { get; private set; }

    public TransactionManager(IDbContext dbContext)
    {
        transactionsTable = dbContext.GetTable<Transaction>();
    }
    public void OpenTransaction(BinanceOrder buyOrder, MarketStateStatus status)
    {
        Transaction? last = transactionsTable?.Last().GetAwaiter().GetResult();
        if (last != null)
        {
            last.Status = TransactionStatus.Failed;
            transactionsTable?.Update(last);
        }
        Current = new Transaction()
        {
            Symbol = buyOrder.Symbol,
            BuyOrderId = buyOrder.Id,
            BuyPrice = buyOrder.Price,
            BuyTime = DateTime.Now,
            BuyFees = buyOrder.QuoteQuantityFilled * 0.001m,
            Quantity = Math.Floor(buyOrder.Quantity * 100000) / 100000,
            Status = TransactionStatus.Bought,
            MarketStateAtOpen = status
        };
        if(transactionsTable != null)transactionsTable.Add(Current);
    }
    public void CreateOco(BinanceOrderOcoList ocoOrder) {

        if (Current != null)
        {
            Current.OcoOrderId = ocoOrder.Id;
            Current.UpperSellOrderId = ocoOrder.Orders.FirstOrDefault()?.OrderId;
            Current.LowerSellOrderId = ocoOrder.Orders.LastOrDefault()?.OrderId;
            Current.Status = TransactionStatus.Selling;

            if (transactionsTable != null) transactionsTable.Update(Current);
        }
    }
    public void FinishTransaction(BinanceOrder sellOrder, MarketStateStatus status)
    {
        if (Current != null) {
            Current.SellOrderId = sellOrder.Id;
            Current.SellPrice = sellOrder.Price;
            Current.SellOrderOpenTime = sellOrder.CreateTime;
            Current.SellOrderCloseTime = DateTime.Now;
            Current.Duration = DateTime.Now.Subtract(sellOrder.CreateTime).TotalHours;
            Current.SellFeesEq = sellOrder.QuoteQuantityFilled * 0.001m;
            Current.Status = TransactionStatus.Finished;
            Current.MarketStateAtClose = status;
            Current.Profits = ((Current.SellPrice - Current.BuyPrice) * Current.Quantity) - Current.BuyFees - Current.SellFeesEq;

            if (transactionsTable != null) transactionsTable.Update(Current);
            ClearCurrent();
        }
    }
    public void Close(List<BinanceTrade> trades, MarketStateStatus status)
    {
        if (Current != null)
        {
            Current.SellOrderId = trades.FirstOrDefault()?.OrderId;
            Current.SellPrice = trades.Select(a => a.Price).Average();
            Current.SellOrderOpenTime = null;
            Current.SellOrderCloseTime = DateTime.Now;
            Current.Duration = Current.BuyTime != null ? DateTime.Now.Subtract(Current.BuyTime.Value).TotalHours : 0;
            Current.SellFeesEq = trades.Select(a => a.QuoteQuantity).Sum() * 0.001m;
            Current.Status = TransactionStatus.Finished;
            Current.MarketStateAtClose = status;
            Current.Profits = ((Current.SellPrice - Current.BuyPrice) * Current.Quantity) - Current.BuyFees - Current.SellFeesEq;

            if (transactionsTable != null) transactionsTable.Update(Current);
            ClearCurrent();
        }
    }
    public Transaction? Last()
    {
        return transactionsTable?.Last().GetAwaiter().GetResult();
    }
    public void ClearCurrent()=>Current = null;
}
