using ScalperPlus.Common.TradingServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalperPlus.Common.ChanceDetection
{
    internal interface IChanceTester
    {
        public bool HaveChance();
    }
}
