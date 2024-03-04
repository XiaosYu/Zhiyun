using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhiyun.Nodes.Services;

namespace Zhiyun.Nodes
{
    public static class Extensions
    {
        public static void Initialize(this STNodeEditor editor)
        {
            editor.Nodes.Clear();
            editor.LoadTypes(NodeTypeService.Shared.LoadedTypes);
        }

        public static void Initialize(this STNodeTreeView view) => view.LoadTypes("节点", NodeTypeService.Shared.LoadedTypes);
    }
}
