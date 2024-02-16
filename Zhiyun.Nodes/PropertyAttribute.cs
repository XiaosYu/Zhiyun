using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Nodes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyAttribute: Attribute
    {
        public bool Default = true;
    }
}
