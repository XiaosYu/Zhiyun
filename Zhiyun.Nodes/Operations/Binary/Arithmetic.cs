using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Nodes.Operations.Binary
{
    [STNode("运算符号/二元运算/算术")]
    public abstract class Arithmetic: BinaryOperation
    {
        public static List<Dimension> Compute(List<Dimension> inputDimensions, Dictionary<string, int> parameters)
        {
            return [inputDimensions[0].Clone()];
        }
    }
}
