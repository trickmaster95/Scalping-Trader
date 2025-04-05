using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalperPlus.Common.ChanceDetection
{
    internal class DownChanceTester : IChanceTester
    {
        private readonly IChart _chart;
        private readonly DeterminantsSettings? _determinations;
        public DownChanceTester(IChart chart)
        {
            _chart = chart;
            IDbContext db = new DbContext();
            _determinations = db.GetTable<DeterminantsSettings>()?.Find(item => item.MarketStateStatus == MarketStateStatus.UpTrend).GetAwaiter().GetResult();
        }
        public bool HaveChance()
        {
            if (_determinations == null) return false;
            if (_chart.Candles.LastOrDefault()?.Rsi7 < _determinations.MaxRsi14) return true;
            return false;
        }
    }
}
