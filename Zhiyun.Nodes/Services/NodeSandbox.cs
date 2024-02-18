using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhiyun.Utilities.Extensions;

namespace Zhiyun.Nodes.Services
{
    public class NodeSandbox: STNodeControl
    {
        private readonly STNodeEditor NodeEditor = new();

        public NodeSandbox(byte[] graph)
        {
            Visable = false;
            NodeTypeService.Shared.Foreach(s => NodeEditor.LoadType(s));
            NodeEditor.LoadCanvas(graph);
        }

        public MonolithicNode GetMonolithic() => new()
        {
            Nodes = NodeEditor.Nodes.ToArray().Select(s => (s as NodeBase)!.GetNodeData()).ToList()
        };

        public byte[] GetCanvasData() => NodeEditor.GetCanvasData();

        public DimensionType InputDimensionType => FindNode<Input>()?.FeaturesDim.DimensionType ?? DimensionType.OnlyBatch;
        public DimensionType OutputDimensionType => FindNode<Output>()?.OutDim.DimensionType ?? DimensionType.OnlyBatch;

        public TNode? FindNode<TNode>() where TNode : NodeBase => (TNode)NodeEditor.Nodes.Cast<NodeBase>().FirstOrDefault(s=> s is TNode)!;
        public TNode? FindNode<TNode>(Func<NodeBase, bool> match) where TNode: NodeBase => (TNode)NodeEditor.Nodes.Cast<NodeBase>().FirstOrDefault(match)!;
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
    }
}
