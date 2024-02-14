using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Nodes.Operations.Unary
{
    [Name("扁平化")]
    public class Flatten : UnaryOperation
    {
        protected override Dimension CalculateDimensions(Dimension first)
        {
            var totalShape = first.DimensionWithoutBatch.ValueCount;
            return Dimension.Create(totalShape);
        }

        public static List<Dimension> Compute(List<Dimension> inputDimensions, Dictionary<string, int> parameters)
        {
            var first = inputDimensions[0];
            var totalShape = first.DimensionWithoutBatch.ValueCount;
            return [Dimension.Create(totalShape)];
        }
    }
}
