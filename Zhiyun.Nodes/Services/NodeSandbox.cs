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
using Zhiyun.Utilities.Extensions;

namespace Zhiyun.Nodes.Services
{
    public partial class NodeSandbox : Form
    {
        public STNodeEditor GetNodeEditor() => NodeEditor;
        private readonly bool Initialized = false;

        public NodeSandbox(byte[] graphs)
        {
            InitializeComponent();
            NodeEditor.ActiveChanged += (s, ea) => NodePropertyGrid.SetNode(NodeEditor.ActiveNode);
            NodePropertyGrid.ReadOnlyModel = true;

            NodeEditor.Initialize();
            NodeEditor.LoadCanvas(graphs);
            Initialized = true;
        }

        public MonolithicNode GetMonolithic() => new()
        {
            Nodes = NodeEditor.Nodes.ToArray().Select(s => (s as NodeBase)!.GetNodeData()).ToList()
        };

        public byte[] GetCanvasData()
        {
            using var stream = new MemoryStream();
            NodeEditor.SaveCanvas(stream);
            return stream.ToArray();
        }

        public DimensionType InputDimensionType => FindNode<Input>()?.FeaturesDim.DimensionType ?? DimensionType.OnlyBatch;
        public DimensionType OutputDimensionType => FindNode<Output>()?.OutDim.DimensionType ?? DimensionType.OnlyBatch;

        public TNode? FindNode<TNode>() where TNode : NodeBase => (TNode)NodeEditor.Nodes.Cast<NodeBase>().FirstOrDefault(s => s is TNode)!;
        public TNode? FindNode<TNode>(Func<NodeBase, bool> match) where TNode : NodeBase => (TNode)NodeEditor.Nodes.Cast<NodeBase>().FirstOrDefault(match)!;
        public NodeBase? FindNode(Func<NodeBase, bool> match) => NodeEditor.Nodes.Cast<NodeBase>().FirstOrDefault(match)!;

        public long GetParametersNumber() => NodeEditor.Nodes
            .Where(s => s is Module)
            .Cast<Module>()
            .Sum(s => s.ParametersNumber);

        public void SetNodeProperty(string nodeID, string propertyID, object value) => FindNode(s => s.Id == nodeID)?.SetProperty(propertyID, value);
        public void SetNodeField(string nodeID, string fieldID, object value) => FindNode(s => s.Id == nodeID)?.SetField(fieldID, value);
        public object? GetNodeProperty(string nodeID, string propertyName) => FindNode(s => s.Id == nodeID)?.GetProperty(propertyName);

        public Dimension Input(Dimension inputDimension)
        {
            var inputNode = FindNode<Input>();
            inputNode?.Modify(inputDimension);
            var outputNode = FindNode<Output>();
            return outputNode?.OutDim ?? Dimension.Empty;
        }

        private void NodeEditor_OptionConnecting(object sender, STNodeEditorOptionEventArgs e)
        {
            if (!Initialized)
            {
                if (e.CurrentOption.Owner is NodeBase current && e.TargetOption.Owner is NodeBase target)
                {
                    if (!e.CurrentOption.IsInput)
                    {
                        if (e.Status == ConnectionStatus.Connecting)
                        {
                            current.ChildNodes.Add(target);
                            target.ParentNodes.Add(current);
                        }
                    }
                    else
                    {
                        if (e.Status == ConnectionStatus.Connecting)
                        {
                            target.ChildNodes.Add(current);
                            current.ParentNodes.Add(target);
                        }
                    }
                }
            }
            else
            {
                e.Continue = false;
            }
        }

        private void NodeEditor_OptionDisConnecting(object sender, STNodeEditorOptionEventArgs e)
        {
            e.Continue = false;
        }

        private void NodeEditor_NodeAdded(object sender, STNodeEditorEventArgs e)
        {
            if (e.Node is CustomModule custom)
            {
                custom.ContextMenuStrip = new CustomModuleContextMenuStrip(custom);
            }
        }



        private class CustomModuleContextMenuStrip : ContextMenuStrip
        {

            public CustomModuleContextMenuStrip(CustomModule customModule)
            {
                var showDetail = new ToolStripMenuItem
                {
                    Text = "查看结构",
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(200, 34, 34, 34)
                };

                showDetail.Click += (object? sender, EventArgs args) => customModule.ShowDetailWindow();

                Items.Add(showDetail);
            }

        }
    }

  
}
