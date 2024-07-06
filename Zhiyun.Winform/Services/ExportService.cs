using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhiyun.Nodes;
using Zhiyun.Utilities.Extensions;

namespace Zhiyun.Winform.Services
{
    public class ExportService
    {
        public string Export(byte[] graphsBytes, List<NodeData> nodes, Dictionary<string, List<string>> settables)
        {
            var modules = nodes.ToDictionary(key => key.Id, value => value);
            var graphs = graphsBytes.ToBase64String();


            return new
            {
                Modules = modules,
                Graphs = graphs,
                Settable = settables
            }.ToJson();
        }
    }
}
