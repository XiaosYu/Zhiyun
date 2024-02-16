using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhiyun.Nodes;
using Zhiyun.Nodes.Interfaces;
using Zhiyun.Nodes.Modules;
using Zhiyun.Utilities.Extensions;

namespace Zhiyun.Winform.Components
{
    public class CustomModuleContextMenuStrip: NodeContextMenuStrip, ICustomModuleContextStripLinker
    {
        public CustomModuleContextMenuStrip(CustomModule customModule) 
        {
            this.customModule = customModule;
            this.showDetailWindow = new ShowDetailWindow(customModule.ModuleMessage.Graphs.FromBase64String());

            var showDetail = new ToolStripMenuItem
            {
                Text = "查看结构",
                ForeColor = Color.White,
                BackColor = Color.FromArgb(200, 34, 34, 34)
            };
            showDetail.Click += ShowDetail_Click;

            Items.Add(showDetail);
        }

        private CustomModule customModule;

        private ShowDetailWindow showDetailWindow;

        public MonolithicNode Modification => showDetailWindow.Current;

        public NodeBase FindNode(Func<NodeBase, bool> match) => showDetailWindow.FindNode(match);

        private void ShowDetail_Click(object? sender, EventArgs e)
        {
            showDetailWindow.ShowDialog();
        }

        public IEnumerable<NodeBase> ListAll() => showDetailWindow.ListAll();
    }
}
