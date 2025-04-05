using ScalperPlus.Common.ChanceDetection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalperPlus.Common.TradingServices
{
    internal class ChanceDetector: IChanceDetector
    {
        public delegate void ChanceDetectorEventHandler();
        public event ChanceDetectorEventHandler? ChanceDetected;
        protected virtual void OnChanceDetected() => ChanceDetected?.Invoke();

        private IChanceTester _chanceTester;
        private IChart _chart;
        private IMarketState _marketState;
        public ChanceDetector(IChart chart ,  IMarketState marketState)
        {
            _chart = chart;
            UpdateTester();
            _marketState = marketState;
        }
        public void UpdateTester()
        {
            _chanceTester = Provide();
        }
        public bool HaveChance()
        {
            return _chanceTester.HaveChance();
        }
        private IChanceTester Provide()
        {
            switch (_marketState.Current)
            {
                case MarketStateStatus.UpTrend: return new UpTrendChanceTester(_chart);
                case MarketStateStatus.Moderate: return new ModerateChanceTester(_chart);
                case MarketStateStatus.Down: return new DownChanceTester(_chart);
                case MarketStateStatus.Drop: return new DropChanceTester(_chart);
                default: throw new Exception("wrong market status value");
            }
        }
    }
}
