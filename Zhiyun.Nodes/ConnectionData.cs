using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Nodes
{
    public class ConnectionData
    {
        public const int Batch = -10;
        public Dimension Dimension { get; set; } = Dimension.Create();
        public Dictionary<string, object> Contents { get; set; } = [];
        public int OptionIndex { get; set; }
    }
}
