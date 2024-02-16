using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Nodes.Structures
{
    [Name("向量输出")]
    public class VectorOutput: Output
    {
        private int outNumber;
        [STNodeProperty("输出数量", "")]
        [Property(Default = false)]
        public int OutNumber
        {
            get => outNumber;
            set { outNumber = value; Flush(); }
        }

        public override Dimension OutDim => Dimension.Create(OutNumber);

        public override void OnFlushComponent()
        {
            UpdateText("Feature", $"输出尺寸:\n[batch×{outNumber}]");
            SetInPortText(OutNumber.ToString());
        }

        public override void OnReceivedMessage(ConnectionData data)
        {
            if(data.Dimension.IsVector)
                OutNumber = data.Dimension[1];
            else throw new Exception("Dimension Error");
        }

        protected override void OnInitializeProperty()
        {
            base.OnInitializeProperty();

            AutoSize = false;
            AddTextBlockControl("Feature", "");
        }
    }
}
