using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Utilities.Extensions
{
    static public partial class Extension
    {
        static public byte ToByte(this bool boolean)
            => Convert.ToByte(boolean);
    }
}
