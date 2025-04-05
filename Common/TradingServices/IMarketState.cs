using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalperPlus.Common.TradingServices
{
    internal interface IMarketState
    {
        public delegate void MarketStatusChangedEventHandler(MarketStateStatus status);
        public event MarketStatusChangedEventHandler MarketStatusChanged;
        public MarketStateStatus Current { get; }
        public void Determine();
    }
}
