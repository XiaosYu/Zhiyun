using System.Text.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace Zhiyun.Nodes
{
    public class NodeData
    {
        public string Name { set; get; }
        public string Type { set; get; }
        public string Id { set; get; }
        public ParameterDataCollection Parameters { set; get; }
        public int LearnableParameters { get; set; }

        public List<Dimension> InputDimensions { set; get; }
        public List<Dimension> OutputDimensions { set; get; }

        public List<string> Connected { set; get; }
    }

    public class MonolithicNode
    {
        public List<NodeData> Nodes { set; get; }

        public List<Dimension> Compute(List<Dimension> inputDimensions)
        {
            var graphs = Nodes.Select(s => s.Connected).ToList();
            var inputs = new Dictionary<string, List<string>>();
            foreach(var graph in graphs)
            {
                var index = graphs.IndexOf(graph);
                graph.ForEach(s =>
                {
                    if (inputs.TryGetValue(s, out List<string> value))
                        value.Add(Nodes[index].Id);
                });
            }

            var startNode = Nodes.First(s => s.Type.Contains("Input"));
            var endNode = Nodes.First(s => s.Type.Contains("Output"));

            var startIndex = Nodes.IndexOf(startNode);
            var endIndex = Nodes.IndexOf(endNode);

            var loop = true;
            while(loop)
            {

            }
        }

        [JsonIgnore]
        public Dimension InputDimension => Nodes.First(s => s.Type.Contains("Input")).OutputDimensions[0];

        [JsonIgnore]
        public Dimension OutputDimension => Nodes.First(s => s.Type.Contains("Output")).InputDimensions[0];

        [JsonIgnore]
        public int LearnableParameters => Nodes.Sum(s => s.LearnableParameters);
        [JsonIgnore]
        public List<ParameterData> SettableParameters 
        {   get
            {
                List<ParameterData> datas = [];
                Nodes.ForEach(node => datas.AddRange(node.Parameters.Where(s => s.Settable)));
                return datas;
            }
        }

        public NodeData this[string id]
        {
            get => Nodes.First(s => s.Id == id);
            set => Nodes[Nodes.FindIndex(s => s.Id == id)] = value;
        }
    }
}
