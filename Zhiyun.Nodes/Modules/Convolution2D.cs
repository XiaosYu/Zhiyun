using System.Drawing;
using System.Threading;
using Zhiyun.Nodes.Structures;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Zhiyun.Nodes.Modules
{
    [Name("2维卷积层")]
    public class Convolution2D : Module
    {
        private int inChannels;
        private int outChannels;
        private int kernelWidth = 3;
        private int kernelHeight = 3;
        private int stride = 1;
        private int padding = 0;

        public override int ParametersNumber => InChannels * OutChannels * KernelHeight * KernelWidth + (Bias ? OutChannels : 0);

        [STNodeProperty("输入通道数", "")]
        [Property]
        public int InChannels
        {
            get => inChannels;
            set { inChannels = value; Flush(); }
        }

        [STNodeProperty("输出通道数", "")]
        [Property]
        public int OutChannels
        {
            get => outChannels;
            set { outChannels = value; Flush(); }
        }

        [STNodeProperty("卷积核宽度", "")]
        [Property]
        public int KernelWidth
        {
            get => kernelWidth;
            set { kernelWidth = value; Flush(); }
        }

        [STNodeProperty("卷积核高度", "")]
        [Property]
        public int KernelHeight
        {
            get => kernelHeight;
            set { kernelHeight = value; Flush(); }
        }


        [STNodeProperty("卷积步长", "")]
        [Property]
        public int Stride
        {
            get => stride;
            set { stride = value; Flush(); }
        }

        [STNodeProperty("填充", "")]
        [Property]
        public int Padding
        {
            get => padding;
            set { padding = value; Flush(); }
        }

        [STNodeProperty("是否采用偏置", "")]
        [Property]
        public bool Bias { set; get; } = true;

        public static List<Dimension> Compute(List<Dimension> inputDimensions, Dictionary<string, int> parameters)
        {
            var inputImage = inputDimensions[0];
            var imageChannels = inputImage[1];
            var imageWidth = inputImage[2];
            var imageHeight = inputImage[3];

            var outWidth = (imageWidth - parameters["KernelWidth"] + 2 * parameters["Padding"]) / parameters["Stride"] + 1;
            var outHeight = (imageHeight - parameters["KernelHeight"] + 2 * parameters["Padding"]) / parameters["Stride"] + 1;
            var outDim = Dimension.Create(parameters["OutChannels"], outWidth, outHeight);
            return [outDim];
        }


        protected override Dimension OutputDim => CalculateDimension(InputDim);

        public override void OnFlushComponent()
        {
            if(InputDim != null && !InputDim.OnlyBatch)
            {
                SetOutPortText(CalculateDimension(OutputDim).ToString(','));
                SetInPortText(InputDim.ToString(','));
            }
            
            UpdateText("Feature", $"卷积通道数:{InChannels},{OutChannels}\n卷积核尺寸:{KernelWidth}×{KernelHeight}");
        }

        private Dimension CalculateDimension(Dimension input)
        {
            var inputImage = input;
            var imageChannels = inputImage[1];
            var imageWidth = inputImage[2];
            var imageHeight = inputImage[3];

            var outWidth = (imageWidth - KernelWidth + 2 * Padding) / Stride + 1;
            var outHeight = (imageHeight - KernelHeight + 2 * Padding) / Stride + 1;
            var outDim = Dimension.Create(OutChannels, outWidth, outHeight);
            return outDim;
        }

        protected override void OnReceivedMessagePart(ConnectionData data)
        {
            if (data.Dimension.IsImage)
            {
                var inputImage = data.Dimension;
                var imageChannels = inputImage[1];
                var imageWidth = inputImage[2];
                var imageHeight = inputImage[3];
                InChannels = imageChannels;
                SetInPortText(inputImage.ToString(','));
                var outWidth = imageWidth - KernelWidth + 1;
                var outHeight = imageHeight - KernelHeight + 1;
                var outDim = Dimension.Create(OutChannels, outWidth, outHeight);
                SetOutPortText(outDim.ToString(','));
            }
            else
                throw new Exception("Dimension Error");
            
        }


        public override ConnectionData OnSendMessage() => InputDim.OnlyBatch ? new() { Dimension = Dimension.Create() }: new () { Dimension = OutputDim.Clone() };

        protected override void OnInitializeProperty()
        {
            base.OnInitializeProperty();

            AutoSize = false;
            AddTextBlockControl("Feature", "");
        }
    }
}
