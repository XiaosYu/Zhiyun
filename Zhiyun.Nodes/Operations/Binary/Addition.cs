using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Nodes.Operations.Binary
{
    [Name("加法运算")]
    public class Addition : Arithmetic
    {
        protected override bool CanForward()
        {
            if (Number1Dimension == null || Number2Dimension == null)
                return false;
            for (int i = 0; i < Number1Dimension.DimensionsCount; ++i)
            {
                if (Number1Dimension[i] != Number2Dimension[i])
                    return false;
            }
            return true;
        }

        protected override Dimension CalculateDimensions(Dimension first, Dimension second)
            => first.Clone();

    }
}
