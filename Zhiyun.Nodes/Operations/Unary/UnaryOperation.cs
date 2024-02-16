using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Nodes.Operations.Unary
{
    [STNode("运算符号/一元运算")]
    public abstract class UnaryOperation: Operation
    {
        public UnaryOperation()
        {
            TitleColor = Color.FromArgb(200, Color.LightSeaGreen);
        }

        protected override void OnInitializePort()
        {
            AddInPort("IN", true);
            AddOutPort("OUT", false);
        }

     
        protected STNodeOption NumberOption => InputOptions[0];
        protected STNodeOption OutPortOptiont => OutputOptions[0];

        public Dimension? NumberDimension { set; get; }

        protected virtual void OnFlushComponentPart() { }

        protected abstract Dimension CalculateDimensions(Dimension first);


        public override void OnFlushComponent()
        {
            if (NumberDimension != null)
            {
                SetOptionText(NumberOption, NumberDimension);
                var resultDim = CalculateDimensions(NumberDimension);
                SetOptionText(OutPortOptiont, resultDim);
            }

            OnFlushComponentPart();
        }

        public override ConnectionData OnSendMessage()
        {
            if (NumberDimension == null)
                return new ConnectionData();
            else
            {
                var resulDim = CalculateDimensions(NumberDimension);
                return new ConnectionData() { Dimension = resulDim };
            }
        }

        public override void OnReceivedMessage(ConnectionData data)
        {
            NumberDimension = data.Dimension;
            Flush();
        }
    }
}
