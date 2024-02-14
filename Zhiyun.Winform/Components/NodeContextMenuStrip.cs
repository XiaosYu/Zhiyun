using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhiyun.Nodes.Modules;
using Zhiyun.Utilities.Extensions;

namespace Zhiyun.Winform.Components
{
    public class NodeContextMenuStrip: ContextMenuStrip
    {
        public NodeContextMenuStrip() 
        {
            var deleteDetail = new ToolStripMenuItem
            {
                Text = "删除节点",
                ForeColor = Color.White,
                BackColor = Color.FromArgb(200, 34, 34, 34)
            };

            deleteDetail.Click += (object? sender, EventArgs args) => OnClickDelete();

            BackColor = Color.FromArgb(200, 34, 34, 34);
            Items.Add(deleteDetail);
        }

        public Action OnClickDelete { get; set; } = () => { };

     

    }
}
