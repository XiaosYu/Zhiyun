using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace Zhiyun.Nodes
{
    public class ParameterData
    {
        [ReadOnly(true)]
        public string Name { get; set; }

        [ReadOnly(true)]
        public string Type { get; set; }

        [ReadOnly(true)]
        public object Value { get; set; }
        public bool Settable { get; set; }
        public string Description { get; set; } = string.Empty;

        [ReadOnly(true)]
        public string ParentID { get; set; }
        [ReadOnly(true)]
        public string ParentType { get; set; }
    }

    public class ParameterDataCollection : List<ParameterData>, IEnumerable<ParameterData>
    {
        public ParameterDataCollection() { }
        public ParameterDataCollection(IEnumerable<ParameterData> collection) : base(collection) { }


        [NotMapped]
        public List<string> Keys => this.Select(s => s.Name).ToList();
        public ParameterData this[string key]
        {
            get => Find(s => s.Name == key);
            set
            {
                var index = FindIndex(s => s.Name == key);
                this[index] = value;
            }
        }
    }
}
