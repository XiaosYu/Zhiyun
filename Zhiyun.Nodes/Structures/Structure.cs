using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Nodes.Structures
{
    [STNode("基本结构")]
    public abstract class Structure : NodeBase
    {
        public Structure()
        {
            TitleColor = Color.FromArgb(200, Color.BlanchedAlmond);
        }

        protected override void OnInitializePort()
        {
            AddInPort("IN", true);
            AddOutPort("OUT", true);
        }
    }
}
