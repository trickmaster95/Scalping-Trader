using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalperPlus.Common.TradingServices
{
    internal interface IOrder
    {
        public BinanceOrder BuyMarket();
        public BinanceOrderOcoList? SellOco(decimal quantity, decimal takeProfitLimit, decimal stopLoseLimit);
        public void cancleOrders(List<long> orderIds);
    }
}
