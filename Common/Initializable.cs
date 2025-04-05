using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalperPlus.Common
{
    internal interface Initializable
    {
        public event VoidEventHandler? Initialized;
    }
}
