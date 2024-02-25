using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhiyun.Nodes;
using Zhiyun.Nodes.Modules;
using Zhiyun.Utilities.Extensions;

namespace Zhiyun.Winform.Components
{
    public class CustomModuleContextMenuStrip: NodeContextMenuStrip
    {
        public CustomModuleContextMenuStrip(CustomModule customModule) 
        {
            var showDetail = new ToolStripMenuItem
            {
                Text = "查看结构",
                ForeColor = Color.White,
                BackColor = Color.FromArgb(200, 34, 34, 34)
            };
            showDetail.Click += (object? sender, EventArgs args) => new ShowDetailWindow(customModule.GetBytes()).ShowDialog();

            Items.Add(showDetail);
        }

    }
}
