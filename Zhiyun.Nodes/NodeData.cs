using System.Text.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Nodes;
using System.Configuration;
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
        public List<string> Outputs { set; get; }
        public List<string> Inputs { set; get; }
        public object this[string parameterName]
        {
            get => Parameters.FirstOrDefault(s => s.Name == parameterName);
        }
    }

    public class MonolithicNode
    {
        public List<NodeData> Nodes { set; get; }

        [JsonIgnore]
        public Dimension InputDimension
        {
            get
            {
                var output = Nodes.FirstOrDefault(s => s.Type.Contains("Input"));
                if (output != null)
                {
                    var element = (JsonElement)output.Parameters["FeaturesDim"].Value;
                    return element.Deserialize<Dimension>();
                }
                else return Dimension.Empty;
            }
        }

        [JsonIgnore]
        public Dimension OutputDimension
        {
            get
            {
                var output = Nodes.FirstOrDefault(s => s.Type.Contains("Output"));
                if (output != null) return ((JsonElement)output.Parameters["OutDim"].Value).Deserialize<Dimension>();
                else return Dimension.Empty;
            }
        }

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
