using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalperPlus.Common.TradingServices
{
    internal class TradeObserver:ITradeObserver
    {
        private readonly IMarketState marketState;
        private readonly ITransactionManager transactionManager;
        private readonly IDbContext dbContext;
        private readonly IMainSettings botSettings;


        public TradeObserver(IMarketState marketState , ITransactionManager transactionManager , IDbContext dbContext, IMainSettings botSettings)
        {
            this.marketState = marketState;
            this.transactionManager = transactionManager;
            this.dbContext = dbContext;
            this.botSettings = botSettings;
        }

        private void FinishTransaction()
        {
            List<BinanceTrade> trades = new BinanceClients().Rest.SpotApi.Trading.GetUserTradesAsync(transactionManager.Current.Symbol, endTime: DateTime.UtcNow.AddSeconds(5), limit: 100).GetAwaiter().GetResult().Data.Where(trade => trade.OrderListId == transactionManager.Current.OcoOrderId).ToList();
            if (trades != null)
            {
                if (trades.Count > 0)
                {
                    transactionManager.Close(trades, marketState.Current);
                    botSettings.Settings.Balance += transactionManager.Current.Profits ?? 0;
                    botSettings.Save();
                    transactionManager.ClearCurrent();
                }
                else
                {
                    dbContext.GetTable<Transaction>().DeleteLast();
                    transactionManager.ClearCurrent();
                }
            }
        }
        public void ObserveOrder(BinanceStreamOrderList updatedOrder)
        {
            if (transactionManager.Current != null)
            {
                if (transactionManager.Current.Status == TransactionStatus.Selling)
                {
                     if (transactionManager.Current.BuyOrderId == updatedOrder.Id)
                     {
                         if (updatedOrder.ListOrderStatus == ListOrderStatus.Done)
                         {
                             FinishTransaction();
                         }
                     }
                }
            }
        }
    }
}
