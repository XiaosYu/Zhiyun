using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Nodes.Interfaces
{
    public interface ICustomModuleContextStripLinker
    {
        public NodeBase FindNode(Func<NodeBase, bool> match);
        public IEnumerable<NodeBase> ListAll();
        public MonolithicNode Modification { get; }
    }
}
