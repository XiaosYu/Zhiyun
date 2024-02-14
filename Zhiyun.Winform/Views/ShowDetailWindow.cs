using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zhiyun.Winform.Views
{
    public partial class ShowDetailWindow : Form
    {
        public ShowDetailWindow(byte[] canvas)
        {
            InitializeComponent();
            NodeEditor.ActiveChanged += (s, ea) => NodePropertyGrid.SetNode(NodeEditor.ActiveNode);
            NodeEditor.LoadNodes();
            NodeEditor.LoadCanvas(canvas);
            NodePropertyGrid.ReadOnlyModel = true;
        }

        private void ShowDetailWindow_Load(object sender, EventArgs e)
        {
            
         
        }
    }
}
