using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalperPlus.Common.ChanceDetection
{
    internal class DropChanceTester : IChanceTester
    {
        private readonly IChart _chart;
        private readonly DeterminantsSettings? _determinations;
        public DropChanceTester(IChart chart)
        {
            _chart = chart;
            IDbContext dbContext = new DbContext();
            _determinations = dbContext.GetTable<DeterminantsSettings>()?.Find(item => item.MarketStateStatus == MarketStateStatus.UpTrend).GetAwaiter().GetResult();
        }
        public bool HaveChance()
        {
            if (_determinations == null) return false;
            Candle? lastCandle = _chart.Candles.LastOrDefault();
            if (lastCandle == null) return false;
            else
            {
                if (lastCandle.Rsi7 < _determinations.MaxRsi14) return true;
                else if (lastCandle.Rsi14 < 2 * _determinations.MaxRsi14)
                {
                    return CheckTrending();
                }
                return false;
            }
        }

        private bool CheckTrending()
        {
            Candle? firstCandle = null;
            Candle? lastCandle = null;
            List<Candle> candles = _chart.Candles;
            for (int i = 0; (i < candles.Count) && (firstCandle == null || lastCandle == null); i++)
            {
                if (candles[candles.Count - 1 - i].Rsi7 <= 30)
                {
                    if (candles[candles.Count - 2 - i].Rsi7 > 30)
                    {
                        if (firstCandle == null) firstCandle = candles[candles.Count - 1 - i];
                        else if (lastCandle == null) lastCandle = candles[candles.Count - 1 - i];
                    }
                }
            }
            if (firstCandle == null || lastCandle == null) return false;
            else
            {
                decimal firstPrice = ((2 * _determinations.MaxRsi14) / firstCandle.Rsi7) * firstCandle.ClosePrice;
                decimal lastPrice = ((2 * _determinations.MaxRsi14) / lastCandle.Rsi7) * lastCandle.ClosePrice;

                if (firstPrice > lastPrice) return true;
                else return false;
            }
        }
    }
}
