using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalperPlus.Common.TradingServices
{
    internal class TradeTester:ITradeTester
    {
        private readonly IChart chart;
        private readonly IChanceDetector chanceDetector;
        private readonly IMarketState marketState;
        private readonly IOrder order;
        private readonly ITransactionManager transactionManager;
        private readonly IDbContext dbContext;

        private bool liveCandleIsReady;
        private bool liveAccountIsReady;
        private bool chartIsReady;
        private bool IsReady => liveCandleIsReady&&liveAccountIsReady&&chartIsReady;

        public TradeTester( IChart chart , IChanceDetector chanceDetector , IMarketState marketState , IOrder order, ITransactionManager transactionManager , IDbContext dbContext)
        {
            this.chart = chart;
            this.chanceDetector = chanceDetector;
            this.marketState = marketState;
            this.order = order;
            this.transactionManager = transactionManager;
            this.dbContext = dbContext;

            this.chart.Initialized += () => chartIsReady = true;
            this.chart.ChartUpdated += OnChartUpdated;
        }
        public void OnChartUpdated() {
            if (IsReady)
            {
                TryTrade();
            }
        }
        #region Try Trade
        private void TryTrade()
        {
            if (transactionManager.Current == null)
            {
                UpdateMarketStatus();
                UpdateChanceTester();
                if (CheckChance())
                {
                    MakeOrder();
                    OpenSellingOcoOrder();
                }
            }
        }
        private void UpdateMarketStatus()
        {
            marketState.Determine();
        }
        private void UpdateChanceTester()
        {
            chanceDetector.UpdateTester();
        }
        private bool CheckChance() 
        { 
            return chanceDetector.HaveChance();
        }
        private void MakeOrder()
        {
            BinanceOrder buyOrder = order.BuyMarket();
            transactionManager.OpenTransaction(buyOrder, marketState.Current);
        }
        private void OpenSellingOcoOrder() {
            if(transactionManager.Current != null)
            {
                Table<DeterminantsSettings>? table = dbContext.GetTable<DeterminantsSettings>();
                if (table == null) throw new Exception("unhandled error");
                DeterminantsSettings? settings = table.Find(a => a.MarketStateStatus == marketState.Current).GetAwaiter().GetResult();
                if (settings == null) throw new Exception("unhandled error");

                decimal? quantity = transactionManager.Current.Quantity;
                decimal? takeProfitLimit = transactionManager.Current.BuyPrice * (1m + settings.TakeProfit * 0.01m);
                decimal? stopLimit = transactionManager.Current.BuyPrice * (1m + settings.StopLose * 0.01m);

                BinanceOrderOcoList? ocoList = order.SellOco((decimal)quantity, (decimal)takeProfitLimit, (decimal)stopLimit);
                if (ocoList == null) throw new Exception("unhandled error");
                transactionManager.CreateOco(ocoList);
            }
        }
        #endregion
    }
}
