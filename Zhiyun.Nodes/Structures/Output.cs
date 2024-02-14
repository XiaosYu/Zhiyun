using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Nodes.Structures
{
    public abstract class Output : Structure
    {
        public abstract Dimension OutDim { get; }

        protected override List<Dimension> GetInputDimensions() => [OutDim];

        protected virtual STNodeOption InPort => InputOptions[0];
        protected virtual void SetInPortText(string text)
            => SetOptionText(InputOptions[0], text);

        protected override void OnInitializePort()
        {
            AddInPort("IN", true);
        }
    }
}
