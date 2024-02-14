
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Nodes.Structures
{
    [Name("向量输入")]
    public class VectorInput: Input
    {
        private int featureNumber;
        [STNodeProperty("特征数量", "")]
        [Property]
        public int FeatureNumber
        {
            get => featureNumber;
            set { featureNumber = value; Flush(); }
        }

        public override Dimension FeaturesDim => Dimension.Create(FeatureNumber);

        public override void OnFlushComponent()
        {
            UpdateText("Feature", $"输入特征尺寸:\n[batch×{FeatureNumber}]");
            SetOutPortText(FeatureNumber.ToString());
        }

        public override ConnectionData OnSendMessage() => new() { Dimension = Dimension.Create(FeatureNumber) };

        protected override void OnInitializeProperty()
        {
            base.OnInitializeProperty();

            AutoSize = false;
            AddTextBlockControl("Feature", "");
        }
    }
}
