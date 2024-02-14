using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Nodes.Operations.Binary
{
    [Name("张量连接")]
    public class Concatenate: BinaryOperation
    {

        private int connectedDim = 1;

        [STNodeProperty("连接维度", "")]
        [Property]
        public int ConnectedDim
        {
            get => connectedDim;
            set { connectedDim = value; Flush(); }
        }

        protected override bool CanForward()
        {
            if (Number1Dimension == null || Number2Dimension == null)
                return false;
            for (int i = 0; i < Number1Dimension.DimensionsCount; ++i)
            {
                if (i == ConnectedDim) continue;
                if (Number1Dimension[i] != Number2Dimension[i])
                    return false;
            }
            return true;
        }

        protected override void OnFlushComponentPart()
        {
            UpdateText("Feature", $"连接维度:\n{ConnectedDim}");
        }

        protected override Dimension CalculateDimensions(Dimension first, Dimension second)
        {
            var resulDim = first.Clone();
            resulDim[ConnectedDim] += second[ConnectedDim];
            return resulDim;
        }

        public static List<Dimension> Compute(List<Dimension> inputDimensions, Dictionary<string, int> parameters)
        {
            var first = inputDimensions[0];
            var second = inputDimensions[1];
            var resulDim = first.Clone();
            resulDim[parameters["ConnectedDim"]] += second[parameters["ConnectedDim"]];
            return [resulDim];
        }

        protected override void OnInitializeProperty()
        {
            base.OnInitializeProperty();

            AutoSize = false;
            AddTextBlockControl("Feature", "");
        }
    }
}
