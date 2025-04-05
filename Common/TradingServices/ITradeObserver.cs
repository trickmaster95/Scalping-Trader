using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalperPlus.Common.TradingServices
{
    internal interface ITradeObserver
    {
        public void ObserveOrder(BinanceStreamOrderList updatedOrder);
    }
}
