using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Nodes.Operations.Binary
{
    [STNode("运算符号/二元运算")]
    public abstract class BinaryOperation : Operation
    {
        public BinaryOperation()
        {
            TitleColor = Color.FromArgb(200, Color.YellowGreen);
        }

        protected override List<Dimension> GetInputDimensions() => [Number1Dimension, Number2Dimension];
        protected override List<Dimension> GetOutputDimensions() => [CalculateDimensions(Number1Dimension, Number2Dimension)];

        protected override void OnInitializePort()
        {
            AddInPort("IN1", true);
            AddInPort("IN2", true);
            AddOutPort("OUT", false);
        }

        protected STNodeOption Number1Option => InputOptions[0];
        protected STNodeOption Number2Option => InputOptions[1];
        protected STNodeOption OutPortOptiont => OutputOptions[0];

        public Dimension? Number1Dimension { set; get; }
        public Dimension? Number2Dimension { set; get; }

        protected virtual void OnFlushComponentPart() { }

        protected abstract Dimension CalculateDimensions(Dimension first, Dimension second);


        public override void OnFlushComponent()
        {
            if (Number1Dimension != null)
                SetOptionText(Number1Option, Number1Dimension);

            if (Number2Dimension != null)
                SetOptionText(Number2Option, Number2Dimension);

            if (Number1Dimension != null && Number2Dimension != null)
            {
                var resultDim = CalculateDimensions(Number1Dimension, Number2Dimension);
                SetOptionText(OutPortOptiont, resultDim);
            }

            OnFlushComponentPart();
        }

        public override ConnectionData OnSendMessage()
        {
            if (Number1Dimension == null || Number2Dimension == null)
                return new ConnectionData();
            else
            {
                var resulDim = CalculateDimensions(Number1Dimension, Number2Dimension);
                return new ConnectionData() { Dimension = resulDim };
            }
        }

        public override void OnReceivedMessage(ConnectionData data)
        {
            var optionIndex = data.OptionIndex;
            switch (optionIndex)
            {
                case 0: Number1Dimension = data.Dimension; break;
                case 1: Number2Dimension = data.Dimension; break;
            }
            Flush();
        }
    }
}
