using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalperPlus.Common.TradingServices
{
    internal class MarketState:IMarketState
    {
        public event IMarketState.MarketStatusChangedEventHandler? MarketStatusChanged;
        protected virtual void OnMarketStatusChanged(MarketStateStatus status) => MarketStatusChanged?.Invoke(status); 

        private MarketStateStatus current;
        public MarketStateStatus Current {  get { return current; } private set { if (current != value) { current = value; OnMarketStatusChanged(value); } } }
        private readonly IChart _chart;
        public MarketState(IChart chart)
        {
            _chart = chart;
        }
        public void Determine()
        {
            MarketStateStatus status = Current;
            if (_chart.Candles.Count > 99)
            {
                Candle candle = _chart.Candles.Last();
                if (candle.Ma25 > candle.Ma99)
                {
                    if (candle.Ma5 > candle.Ma25) Current = MarketStateStatus.UpTrend;
                    else Current = MarketStateStatus.Moderate;
                }
                else
                {
                    if (candle.Ma5 > candle.Ma25) Current = MarketStateStatus.Down;
                    else Current = MarketStateStatus.Drop;
                }
            }

        }
    }
}
