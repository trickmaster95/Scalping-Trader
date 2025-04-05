﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalperPlus.Common.TradingServices
{
    internal interface ITraillingObserver
    {
        public void ObserveTrailling(IBinanceStreamKlineData liveCandle);
    }
}
