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
            this.customModule = customModule;

            var showDetail = new ToolStripMenuItem
            {
                Text = "查看结构",
                ForeColor = Color.White,
                BackColor = Color.FromArgb(200, 34, 34, 34)
            };
            showDetail.Click += ShowDetail_Click;

            Items.Add(showDetail);
        }

        private readonly CustomModule customModule;

        private void ShowDetail_Click(object? sender, EventArgs e)
        {
            var showDetailWindow = new ShowDetailWindow(customModule.GetBytes());
            showDetailWindow.ShowDialog();
        }

    }
}
