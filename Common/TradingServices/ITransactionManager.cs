using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalperPlus.Common.TradingServices
{
    internal interface ITransactionManager
    {
        public Transaction? Current { get; }
        public void ClearCurrent();
        public void OpenTransaction(BinanceOrder buyOrder, MarketStateStatus status);
        public void CreateOco(BinanceOrderOcoList ocoOrder);
        public void FinishTransaction(BinanceOrder sellOrder, MarketStateStatus status);
        public void Close(List<BinanceTrade> trades, MarketStateStatus status);
        public Transaction? Last();
    }
}
