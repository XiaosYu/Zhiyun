using System.Drawing;
using System.Threading;

namespace Zhiyun.Nodes.Modules
{
    [Name("线性层")]
    public class Linear : Module
    {

        private int inFeatures;
        private int outFeatures;

        public override int ParametersNumber => InFeatures * OutFeatures + (Bias ? OutFeatures : 0);

        [STNodeProperty("输入特征数", "")]
        [Property]
        public int InFeatures
        {
            get => inFeatures;
            set { inFeatures = value; Flush(); }
        }

        [STNodeProperty("输出特征数", "")]
        [Property]
        public int OutFeatures
        {
            get => outFeatures;
            set { outFeatures = value; Flush(); }
        }

        [STNodeProperty("是否采用偏置", "")]
        [Property]
        public bool Bias { set; get; } = true;


        protected override Dimension CalculateOutputDim() => InputDim.Clone().SetDimension(1, OutFeatures);

        public override void OnFlushComponent()
        {
            UpdateText("Feature", $"输入特征数:{InFeatures}\n输出特征数:{OutFeatures}");
            SetOutPortText(OutFeatures.ToString());
            SetInPortText(InFeatures.ToString());
        }

        protected override void OnReceivedMessagePart(ConnectionData data)
        {
            if (data.Dimension.IsVector)
                InFeatures = data.Dimension[1];
            //else throw new Exception("Dimension Error");
            
        }

        protected override void OnInitializeProperty()
        {
            base.OnInitializeProperty();

            AutoSize = false;
            AddTextBlockControl("Feature", "");

          
        }
    }
}
