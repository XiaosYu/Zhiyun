using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Nodes.Structures
{
    public abstract class Input: Structure
    {
        [Property]
        public abstract Dimension FeaturesDim { get; }

        protected virtual STNodeOption OutPort => OutputOptions[0];
        protected virtual void SetOutPortText(string text)
            => SetOptionText(OutputOptions[0], text);

        public abstract void Modify(Dimension dimension);

        protected override void OnInitializePort()
        {
            AddOutPort("OUT", false);
        }
    }
}
