﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Nodes.Modules.Activate
{
    [STNode("网络模块/激活函数")]
    public abstract class Activate: Module
    {
        public static List<Dimension> Compute(List<Dimension> inputDimensions, Dictionary<string, int> parameters)
            => inputDimensions.Select(s => s.Clone()).ToList();

        public override int ParametersNumber => 0;
        public Activate() => TitleColor = Color.FromArgb(200, Color.BlueViolet);

        protected override Dimension OutputDim => InputDim;

        protected override void OnReceivedMessagePart(ConnectionData data)
        {
            Flush();
        }

        protected virtual void OnFlushComponentPart()
        {

        }

        public override void OnFlushComponent()
        {
            OnFlushComponentPart();
            SetInPortText(InputDim);
            SetOutPortText(OutputDim);
        }

        public override ConnectionData OnSendMessage() => new() { Dimension = OutputDim.Clone() };
    }
}