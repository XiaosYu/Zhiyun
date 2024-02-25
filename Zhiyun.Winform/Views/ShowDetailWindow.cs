using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zhiyun.Nodes;

namespace Zhiyun.Winform.Views
{
    public partial class ShowDetailWindow : Form
    {
        public ShowDetailWindow(byte[] canvas)
        {
            InitializeComponent();
            NodeEditor.ActiveChanged += (s, ea) => NodePropertyGrid.SetNode(NodeEditor.ActiveNode);
            NodePropertyGrid.ReadOnlyModel = true;

            NodeEditor.Initialize();
            NodeEditor.LoadCanvas(canvas);
        }

        public ShowDetailWindow(STNodeEditor nodeEditor)
        {
            InitializeComponent();
            NodeEditor = nodeEditor;
        }

        public IEnumerable<NodeBase> ListAll() => NodeEditor.Nodes.ToArray().Select(s=>(s as NodeBase)!);

        public NodeBase FindNode(Func<NodeBase, bool> match) => NodeEditor.Nodes.ToArray().Select(s =>(s as NodeBase)!).First(match);

        public MonolithicNode Current => new(){ Nodes = NodeEditor.Nodes.ToArray().Select(s => s as NodeBase).Select(s => s!.GetNodeData()).ToList()};


        private void ShowDetailWindow_Load(object sender, EventArgs e)
        {


        }
    }
}
