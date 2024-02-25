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
        public List<string> Connected { set; get; }
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
                if (output != null) return output.Parameters["FeatureDim"].Value as Dimension;
                else return Dimension.Empty;
            }
        }

        [JsonIgnore]
        public Dimension OutputDimension
        {
            get
            {
                var output = Nodes.FirstOrDefault(s => s.Type.Contains("Output"));
                if (output != null) return output.Parameters["OutDim"].Value as Dimension;
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
