
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Nodes.Structures
{
    [Name("图片输入")]
    public class ImageInput: Input
    {
        private int channelNumber;
        [STNodeProperty("通道数量", "")]
        [Property(Default = false)]
        public int ChannelNumber
        {
            get => channelNumber;
            set { channelNumber = value; Flush(); }
        }

        private int imageWidth;
        [STNodeProperty("图片宽度", "")]
        [Property(Default = false)]
        public int ImageWidth
        {
            get => imageWidth;
            set { imageWidth = value; Flush(); }
        }

        private int imageHeight;
        [STNodeProperty("图片高度", "")]
        [Property(Default = false)]
        public int ImageHeight
        {
            get => imageHeight;
            set { imageHeight = value; Flush(); }
        }

        public override Dimension FeaturesDim => Dimension.Create(ChannelNumber, ImageWidth, ImageHeight);

        public override void Modify(Dimension dimension)
        {
            if(dimension.IsImage)
            {
                ChannelNumber = dimension[1];
                ImageWidth = dimension[2];
                ImageHeight = dimension[3];
            }
            
        }

        public override void OnFlushComponent()
        {
            UpdateText("Feature", $"输入图片尺寸:\n[batch×{FeaturesDim.ToString()}]");
            SetOutPortText(FeaturesDim.ToString(','));
        }

        public override ConnectionData OnSendMessage() => new() { Dimension = Dimension.Create(ChannelNumber, ImageWidth, ImageHeight) };

        protected override void OnInitializeProperty()
        {
            base.OnInitializeProperty();

            AutoSize = false;
            AddTextBlockControl("Feature", "");
        }
    }
}
