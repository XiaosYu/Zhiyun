using System.Drawing;
using System.Threading;

namespace Zhiyun.Nodes.Modules
{
    [STNode("网络模块")]
    public abstract class Module : NodeBase
    {
        public abstract int ParametersNumber { get; }

        protected Dimension InputDim { get; set; } = Dimension.Create();
        protected Dimension OutputDim => InputDim.OnlyBatch ? Dimension.Create() : CalculateOutputDim();

        protected abstract Dimension CalculateOutputDim();

        public Module()
        {
            TitleColor = Color.FromArgb(200, Color.Goldenrod);
        }

        protected override void OnInitializePort()
        {
            AddInPort("IN", true);
            AddOutPort("OUT", false);
        }

        protected virtual STNodeOption InPort => InputOptions[0];
        protected virtual STNodeOption OutPort => OutputOptions[0];

        protected virtual void SetInPortText(string text)
            => SetOptionText(InputOptions[0], text);

        protected virtual void SetOutPortText(string text)
            => SetOptionText(OutputOptions[0], text);

        protected virtual void OnReceivedMessagePart(ConnectionData data) { }

        public override void OnReceivedMessage(ConnectionData data)
        {
            InputDim = data.Dimension;
            OnReceivedMessagePart(data);
        }

        public override ConnectionData OnSendMessage() => new()
        {
            Dimension = OutputDim.Clone()
        };


    }
}
