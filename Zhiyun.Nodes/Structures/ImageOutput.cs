
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Nodes.Structures
{
    [Name("图片输出")]
    public class ImageOutput: Output
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

        public override Dimension OutDim => Dimension.Create(ChannelNumber, ImageWidth, ImageHeight);

        public override void OnFlushComponent()
        {
            UpdateText("Feature", $"输出图片尺寸:\n[batch×{OutDim.ToString()}]");
            SetInPortText(OutDim.ToString(','));
        }

        public override void OnReceivedMessage(ConnectionData data)
        {
            if (data.Dimension.IsImage)
            {
                channelNumber = data.Dimension[1];
                imageWidth = data.Dimension[2];
                imageHeight = data.Dimension[3];
                Flush();
            }
            // else throw new Exception("Dimension Error");
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
