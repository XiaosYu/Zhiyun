﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Nodes.Modules.Activate
{
    [Name("批规范化")]
    public class BatchNormalization2D: Activate
    {
        [STNodeProperty("特征通道数", "")]
        [Property]
        public int FeatureNumber { get; set; }

        protected override void OnReceivedMessagePart(ConnectionData data)
        {
            if(data.Dimension.IsImage)
            {
                FeatureNumber = data.Dimension[1];
            }

            base.OnReceivedMessagePart(data);
        }
    }
}
